using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]


    public class ControlllerUser : ControllerBase
    {
        private readonly ILogger<ControlllerUser> _logger;

        public ControlllerUser(ILogger<ControlllerUser> logger)
        {
            _logger = logger;
        }


        [SwaggerOperation(Summary = "Get User Informations")]
        [HttpGet(Name = "GetUser")]
        public IEnumerable<result2> Get(string username)
        {

            var R = new GitHubRequests(username);
            return R.GetUserData().ToArray();

        }
    }
}
