using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ToDoList.Models.Interfaces;

namespace ToDoList.BL.RepositoryInterfaces
{
    public interface IRepository<TEntity>
        where TEntity : IEntity
    {
        Guid Add(TEntity entity);
        Task<TEntity> GetByIdAsync(Guid id);
        TEntity GetById(Guid id);
        void Update(TEntity entity);
        void Delete(Guid id);
        List<TEntity> GetAll();
    }
}