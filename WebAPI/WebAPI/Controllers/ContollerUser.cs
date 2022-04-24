using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]


    public class ControlllerUser : ControllerBase
    {
        private readonly ILogger<ControlllerUser> _logger;
        private readonly IGithubRequestsService _service;

        public ControlllerUser(ILogger<ControlllerUser> logger, IGithubRequestsService service)
        {
            _logger = logger;
            _service = service;
        }

        [SwaggerOperation(Summary = "Get User Informations")]
        [HttpGet(Name = "GetUser")]
        public ActionResult<IEnumerable<result2>> Get(string username)
        {
            result2[]? finalResult;
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
