using System;
using System.Threading.Tasks;

namespace RealState.Common.Interfaces.Services
{
    public interface IGenericService<in T> where T : class
    {
        Task<IResponse> Insert(T obj);
        Task<IResponse> Update(T obj);
        Task<IResponse> Delete(Guid id);
        Task<IResponse> GetAll(int page, int size);
        Task<IResponse> GetById(Guid id);
    }
}
