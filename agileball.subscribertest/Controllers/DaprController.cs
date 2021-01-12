using System.Threading.Tasks;
using agileball.subscribertest.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace agileball.subscribertest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DaprController : ControllerBase
    {
        private readonly ILogger<DaprController> _logger;
        public DaprController(ILogger<DaprController> logger)
        {
            _logger = logger;
        }

        [HttpGet("subscribe")]
        public async Task<IActionResult> Subscribe()
        {
            var result = new JsonResult(new Subscription[] {
                new Subscription { pubsubname = "pubsub", topic = "deathStarStatus", route = "team" }
            });
            return result;
        }
    }
}