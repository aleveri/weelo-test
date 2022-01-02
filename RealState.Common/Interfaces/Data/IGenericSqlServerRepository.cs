using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace RealState.Common.Interfaces.Data
{
    public interface IGenericSqlServerRepository<T> where T : class
    {
        Task Insert(T obj);
        Task Update(T obj);
        Task Delete(Guid id);
        Task<IEnumerable<T>> Get(int page, int size, string includeProperties = "",
            Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null);
    }
}
