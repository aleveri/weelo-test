using Microsoft.AspNetCore.Mvc;
using RealState.Common.Entities;
using RealState.Common.Interfaces.Services;

namespace RealState.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropertyTraceController : BaseController<PropertyTrace>
    {
        public PropertyTraceController(IGenericService<PropertyTrace> service) : base(service)
        {
        }
    }
}
