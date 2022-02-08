using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Text;
using ToDoList.BL.Helpers;
using ToDoList.BL.Logic;
using ToDoList.BL.LogicInterfaces;
using ToDoList.BL.Repository;
using ToDoList.BL.RepositoryInterfaces;
using ToDoList.BL.ServiceInterfaces;
using ToDoList.BL.Services;
using ToDoList.BL.Services.EmailProviders;
using ToDoList.Models;

namespace ToDoList.API
{
    public class Startup
    {
        private IConfiguration _configuration { get; }

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ToDoListContext>(
                options => { options.UseSqlServer(_configuration.GetConnectionString("default")); });

            services.AddCors();
            services.AddControllers();
            services.AddHttpContextAccessor();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "ToDoList API"
                });
            });

            var appSettingsSection = _configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);
            services.Configure<SmtpSettings>(_configuration.GetSection("SmtpSettings"));
            services.Configure<FileSettings>(_configuration.GetSection("FileSettings"));
            var appSettings = appSettingsSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            services.AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(x =>
                {
                    x.Events = new JwtBearerEvents
                    {
                        OnTokenValidated = async context =>
                        {
                            var userLogic = context.HttpContext.RequestServices.GetRequiredService<IUserLogic>();
                            var userId = Guid.Parse(context.Principal.Identity.Name);
                            var user = await userLogic.GetByIdAsync(userId);
                            if (user == null)
                            {
                                context.Fail("Unauthorized");
                            }
                        }
                    };
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });

            services.AddScoped(typeof(IRepository<>), typeof(RepositoryBaseGeneric<>));
            services.AddScoped<IUserRepository, UserRepository>();
            // services.AddScoped<IProjectRepository, ProjectRepository>();
            // services.AddScoped<INotificationRepository, NotificationRepository>();
            // services.AddScoped<ISharedProjectRepository, SharedProjectRepository>();
            // services.AddScoped<IToDoTaskRepository, ToDoTaskRepository>();
            services.AddScoped<IUserLogic, UserLogic>();
            services.AddScoped<IProjectLogic, ProjectLogic>();
            services.AddScoped<INotificationLogic, NotificationLogic>();
            services.AddScoped<ISharedProjectLogic, SharedProjectLogic>();
            services.AddScoped<IToDoTaskLogic, ToDoTaskLogic>();
            services.AddScoped<ITokenLogic, TokenLogic>();
            services.AddScoped<IEmailProviderFabric, EmailProviderFabric>();
            services.AddScoped<IFileService, LocalFileService>();
            services.AddScoped<IPdfService, PdfService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "ToDoList API"); });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}