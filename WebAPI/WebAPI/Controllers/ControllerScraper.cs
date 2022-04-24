using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]


    public class ControlllerScraper : ControllerBase
    {
        private readonly ILogger<ControlllerScraper> _logger;

        public ControlllerScraper(ILogger<ControlllerScraper> logger)
        {
            _logger = logger;
        }


        [SwaggerOperation(Summary = "Get Informations")]
        [HttpGet(Name = "GetScraper")]
        public IEnumerable<result1> Get(string username)
        {

            var R1 = new GitHubScraper(username).GetLanguages();

            return null;

        }
    }
}
