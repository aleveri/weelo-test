using Microsoft.AspNetCore.Mvc;
using RealState.Common.Entities;
using RealState.Common.Interfaces.Services;

namespace RealState.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropertyController : BaseController<Property>
    {
        public PropertyController(IGenericService<Property> service) : base(service)
        {
        }  
    }
}
