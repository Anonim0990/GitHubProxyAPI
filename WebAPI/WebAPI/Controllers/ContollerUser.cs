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
            var finalResult = _service.GetUserData(username).ToArray();
            if (finalResult.Length == 1)
            {
                var tmp = finalResult[0];
                if (tmp.userName == "-1")
                {
                    return StatusCode(int.Parse(tmp.userLogin));
                }
            }

            return finalResult;

        }
    }
}
