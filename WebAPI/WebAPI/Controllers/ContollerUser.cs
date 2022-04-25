using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]


    public class ControlllerUser : ControllerBase
    {
        private readonly ILogger<ControlllerUser> _logger;
        private readonly IRequestsService _service;

        public ControlllerUser(ILogger<ControlllerUser> logger, IRequestsService service)
        {
            _logger = logger;
            _service = service;
        }

        [SwaggerOperation(Summary = "Get User Informations")]
        [HttpGet(Name = "GetUser")]
        public ActionResult<IEnumerable<UserData>> Get(string username)
        {
            UserData[]? finalResult;
            try
            {
                finalResult = _service.GetUserData(username).ToArray();
            }
            catch (Exception ex)
            {
                return StatusCode(int.Parse(ex.Message));
            }

            return finalResult;
        }
    }
}
