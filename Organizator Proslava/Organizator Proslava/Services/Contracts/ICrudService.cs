using Organizator_Proslava.Model;
using System;
using System.Collections.Generic;

namespace Organizator_Proslava.Services.Contracts
{
    public interface ICrudService<T> where T : IBaseEntity
    {
        IEnumerable<T> Read();

        T Read(Guid id);

        T Create(T entity);

        IEnumerable<T> CreateRange(IEnumerable<T> entities);

        T Update(T entity);

        T Delete(Guid id);
    }
}