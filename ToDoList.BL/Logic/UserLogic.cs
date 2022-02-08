using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using ToDoList.BL.LogicInterfaces;
using ToDoList.BL.RepositoryInterfaces;
using ToDoList.Models.Entity;
using ToDoList.Models.Model.User;

namespace ToDoList.BL.Logic
{
    public class UserLogic : IUserLogic
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly HttpContext _httpContext;

        public UserLogic(IUserRepository userRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _httpContext = httpContextAccessor.HttpContext;
        }

        public User Authenticate(string email, string password)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                return null;
            }

            var user = _userRepository.GetByEmail(email);

            if (user == null)
            {
                return null;
            }

            if (!VerifyPasswordHash(password, Convert.FromBase64String(user.PasswordEncrypted),
                Convert.FromBase64String(user.PasswordSalt)))
            {
                return null;
            }

            return user;
        }

        public async Task<User> Create(RegisterUserModel model)
        {
            if (string.IsNullOrWhiteSpace(model.Password))
                return null;

            var isEmailExist = await _userRepository.IsEmailExistAsync(model.Email);

            if (isEmailExist)
            {
                return null;
            }

            CreatePasswordHash(model.Password, out var passwordHash, out var passwordSalt);

            var user = _mapper.Map<User>(model);

            user.CreationDate = DateTime.UtcNow;
            user.ModificationDate = DateTime.UtcNow;
            user.PasswordEncrypted = passwordHash;
            user.PasswordSalt = passwordSalt;

            _userRepository.Add(user);

            return user;
        }

        public void Update(Guid id, UpdateModel model)
        {
            var user = _userRepository.GetById(id);

            if (user == null)
            {
                throw new Exception("User not found");
            }

            if (!string.IsNullOrWhiteSpace(model.FirstName))
                user.FirstName = model.FirstName;

            if (!string.IsNullOrWhiteSpace(model.LastName))
                user.LastName = model.LastName;

            if (!string.IsNullOrWhiteSpace(model.Password))
            {
                string passwordHash;
                string passwordSalt;
                CreatePasswordHash(model.Password, out passwordHash, out passwordSalt);

                user.PasswordEncrypted = passwordHash;
                user.PasswordSalt = passwordSalt;
            }

            _userRepository.Update(user);
        }

        public async Task<UserModel> GetByIdAsync(Guid id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            var model = _mapper.Map<UserModel>(user);

            return model;
        }

        public List<User> GetAll()
        {
            return _userRepository.GetAll();
        }

        public Guid GetCurrentUserId()
        {
            return Guid.Parse(_httpContext.User.Identity.Name);
        }

        public void Delete(Guid id)
        {
            _userRepository.Delete(id);
        }

        private void CreatePasswordHash(string password, out string passwordHash,
            out string passwordSalt)
        {
            if (password == null || string.IsNullOrWhiteSpace(password))
            {
                passwordHash = null;
                passwordSalt = null;
            }

            using (var hmac = new HMACSHA512())
            {
                passwordSalt = Convert.ToBase64String(hmac.Key);
                passwordHash = Convert.ToBase64String(hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password)));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (password == null || string.IsNullOrWhiteSpace(password) || storedHash.Length != 64 ||
                storedSalt.Length != 128)
            {
                return false;
            }

            using (var hmac = new HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }

            return true;
        }
    }
}