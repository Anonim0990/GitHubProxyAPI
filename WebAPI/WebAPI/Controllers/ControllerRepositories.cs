using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]


    public class ControlllerRepositories : ControllerBase
    {
        private readonly ILogger<ControlllerRepositories> _logger;

        public ControlllerRepositories(ILogger<ControlllerRepositories> logger)
        {
            _logger = logger;
        }

        [SwaggerOperation(Summary = "Get User Repositories")]
        [HttpGet(Name = "GetRepositories")]
        public IEnumerable<result1> Get(string username)
        {

            var R = new GithubRequestsService(username);
            return R.GetRepositories().ToArray();

        }
    }
}
