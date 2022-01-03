using Microsoft.AspNetCore.Mvc;
using RealState.Common.Entities;
using RealState.Common.Interfaces.Services;

namespace RealState.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropertyImageController : BaseController<PropertyImage>
    {
        public PropertyImageController(IGenericService<PropertyImage> service) : base(service)
        {
        }
    }
}
