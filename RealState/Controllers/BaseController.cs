using Microsoft.AspNetCore.Mvc;
using RealState.Common.Interfaces;
using RealState.Common.Interfaces.Services;
using System;
using System.Threading.Tasks;

namespace RealState.Controllers
{
    [ApiController]
    public class BaseController<T> : ControllerBase where T : class
    {
        private readonly IGenericService<T> _service;

        public BaseController(IGenericService<T> service) => _service = service;

        [HttpPost]
        public async Task<IResponse> Create(T obj) => await _service.Insert(obj);

        [HttpGet]
        public async Task<IResponse> Read(Guid id) => await _service.GetById(id);

        [HttpPut]
        public async Task<IResponse> Update(T obj) => await _service.Update(obj);

        [HttpDelete]
        public async Task<IResponse> Delete(Guid id) => await _service.Delete(id);

        [HttpGet("list")]
        public async Task<IResponse> List(int page, int size) => await _service.GetAll(page, size);
    }
}
