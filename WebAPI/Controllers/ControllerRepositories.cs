using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]


    public class ControlllerRepositories : ControllerBase
    {
        private readonly ILogger<ControlllerRepositories> _logger;
        private readonly IGithubRequestsService _service;

        public ControlllerRepositories(ILogger<ControlllerRepositories> logger,IGithubRequestsService service)
        {
            _logger = logger;
            _service = service;
        }

        [SwaggerOperation(Summary = "Get User Repositories")]
        [HttpGet(Name = "GetRepositories")]
        public ActionResult<IEnumerable<result1>> Get(string username)
        {
            result1[]? finalResult;
            try
            {
                finalResult = _service.GetRepositories(username).ToArray();
            }
            catch (Exception ex)
            {
                return StatusCode(int.Parse(ex.Message));
            }

            return finalResult;

        }
    }
}
