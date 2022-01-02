using RealState.Common.Entities;
using RealState.Common.Enumerations;
using RealState.Common.Interfaces;
using RealState.Common.Interfaces.Data;
using RealState.Common.Interfaces.Services;
using System;
using System.Threading.Tasks;

namespace RealState.Services
{
    public class GenericService<T> : IGenericService<T> where T : BaseEntity
    {
        public readonly IResponse _response;

        public readonly IGenericSqlServerRepository<T> _repository;

        public GenericService(IGenericSqlServerRepository<T> repository, IResponse response)
        {
            _repository = repository;
            _response = response;
        }

        public async Task<IResponse> Delete(Guid id)
        {
            try
            {
                if (id.Equals(Guid.Empty))
                {
                    throw new ArgumentException(MessagesEnum.ValidateEmptyGuid);
                }

                await _repository.Delete(id);
                
                SetSuccessResponse();
                
                return _response;
            }
            catch (Exception e)
            {
                SetExceptionResponse(e.Message);

                return _response;
            }
        }

        public async Task<IResponse> GetAll(int page, int size)
        {
            try
            {
                if (page < 1 || size < 1 || size > 100)
                {
                    throw new ArgumentException(MessagesEnum.ValidatePagination);
                }

                SetSuccessResponse(await _repository.Get(page, size, null, x => !x.Id.Equals(Guid.Empty), null));

                return _response;
            }
            catch (Exception e)
            {
                SetExceptionResponse(e.Message);

                return _response;
            }
        }

        public async Task<IResponse> GetById(Guid id)
        {
            try
            {
                if (id.Equals(Guid.Empty))
                {
                    throw new ArgumentException(MessagesEnum.ValidateEmptyGuid);
                }

                SetSuccessResponse(await _repository.Get(1, 1, null, x => x.Id.Equals(id), null));

                return _response;
            }
            catch (Exception e)
            {
                SetExceptionResponse(e.Message);

                return _response;
            }
        }

        public async Task<IResponse> Insert(T obj)
        {
            try
            {
                if (obj is null)
                {
                    throw new ArgumentException(string.Format(MessagesEnum.ValidateNullEntity, typeof(T)));
                }

                await _repository.Insert(obj);

                SetSuccessResponse();

                return _response;
            }
            catch (Exception e)
            {
                SetExceptionResponse(e.Message);

                return _response;
            }
        }

        public async Task<IResponse> Update(T obj)
        {
            try
            {
                if (obj is null)
                {
                    throw new ArgumentException(string.Format(MessagesEnum.ValidateNullEntity, typeof(T)));
                }

                await _repository.Update(obj);
                
                SetSuccessResponse();

                return _response;
            }
            catch (Exception e)
            {
                SetExceptionResponse(e.Message);

                return _response;
            }
        }

        public void SetExceptionResponse(string message)
        {
            _response.Status = false;
            _response.Content = null;
            _response.Errors.Add(message);
        }

        public void SetSuccessResponse(dynamic content = null)
        {
            _response.Status = true;
            _response.Content = content;
        }
    }
}
