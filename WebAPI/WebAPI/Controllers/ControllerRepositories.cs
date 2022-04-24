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
            var finalResult = _service.GetRepositories(username).ToArray();
            if (finalResult.Length == 1)
            {
                var tmp = finalResult[0];
                if (tmp.repositorylanguagesAndBytes == "-1")
                {
                    return StatusCode(int.Parse(tmp.repositoryName));
                }
            }

            return finalResult;

        }
    }
}
