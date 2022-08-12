using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EGeladinho.Src.Repository
{
    public interface ICrud<T>
    {
        Task Create(T entity);
        Task<T> Read(object param);
        Task Update(T entity);
        Task Delete(object param);
    }
}