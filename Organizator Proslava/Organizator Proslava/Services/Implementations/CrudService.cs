using Microsoft.EntityFrameworkCore;
using Organizator_Proslava.Data;
using Organizator_Proslava.Model;
using Organizator_Proslava.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Organizator_Proslava.Services.Implementations
{
    public class CrudService<T> : ICrudService<T> where T : BaseObservableEntity
    {
        protected DatabaseContext _context;
        protected DbSet<T> _entities;

        public CrudService(DatabaseContext context)
        {
            _context = context;
            _entities = context.Set<T>();
        }

        public virtual T Create(T entity)
        {
            _entities.Add(entity);
            _context.SaveChanges();

            return entity;
        }

        public IEnumerable<T> CreateRange(IEnumerable<T> entities)
        {
            _entities.AddRange(entities);
            _context.SaveChanges();

            return entities;
        }

        public virtual IEnumerable<T> Read()
        {
            return _entities.ToList();
        }

        public virtual T Read(Guid id)
        {
            return _entities.FirstOrDefault(e => e.Id == id);
        }

        public virtual T Update(T entity)
        {
            var entityForUpdate = Read(entity.Id);
            if (entityForUpdate != null)
            {
                _context.Entry(entityForUpdate).CurrentValues.SetValues(entity);
                _context.SaveChanges();
            }

            return entityForUpdate;
        }

        public virtual T Delete(Guid id)
        {
            var entityForDeletion = Read(id);
            if (entityForDeletion != null)
            {
                _context.Remove(entityForDeletion);
                _context.SaveChanges();
            }

            return entityForDeletion;
        }
    }
}