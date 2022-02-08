using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoList.BL.RepositoryInterfaces;
using ToDoList.Models;
using ToDoList.Models.Interfaces;

namespace ToDoList.BL.Repository
{
    public class RepositoryBaseGeneric<TEntity> : IRepository<TEntity>
        where TEntity : class, IEntity
    {
        protected readonly ToDoListContext DbContext;

        public RepositoryBaseGeneric(ToDoListContext dbContext)
        {
            DbContext = dbContext;
        }

        public virtual async Task<TEntity> GetByIdAsync(Guid id)
        {
            var query = DbContext.Set<TEntity>();
            var result = await query.FindAsync(id);
            if (result != null)
            {
                var isDeletedProperty = result.GetType().GetProperty("IsDeleted");
                if (isDeletedProperty != null && (bool) isDeletedProperty.GetValue(result))
                {
                    result = null;
                }
            }

            return result;
        }

        public virtual TEntity GetById(Guid id)
        {
            var query = DbContext.Set<TEntity>();
            var result = query.Find(id);
            if (result != null)
            {
                var isDeletedProperty = result.GetType().GetProperty("IsDeleted");
                if (isDeletedProperty != null && (bool) isDeletedProperty.GetValue(result))
                {
                    result = null;
                }
            }

            return result;
        }

        public virtual void Delete(Guid id)
        {
            var query = DbContext.Set<TEntity>();
            var result = query.Find(id);
            if (result != null)
            {
                var isDeletedProperty = result.GetType().GetProperty("IsDeleted");
                if (isDeletedProperty != null)
                {
                    // TODO
                    DbContext.Remove(result);
                    DbContext.SaveChanges();
                }
            }
        }

        public List<TEntity> GetAll()
        {
            IQueryable<TEntity> result = DbContext.Set<TEntity>();

            return result.ToList();
        }

        public Guid Add(TEntity entity)
        {
            DbContext.Add(entity);
            DbContext.SaveChanges();

            return entity.Id;
        }

        public void Update(TEntity entity)
        {
            DbContext.SaveChangesAsync();
        }
    }
}