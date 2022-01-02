using Microsoft.AspNetCore.Mvc;
using RealState.Common.Entities;
using RealState.Common.Interfaces.Services;

namespace RealState.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OwnerController : BaseController<Owner>
    {
        public OwnerController(IGenericService<Owner> service) : base(service)
        {
        }
    }
}
