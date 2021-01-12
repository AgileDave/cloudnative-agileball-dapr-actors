using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace agileball.service.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HealthController : ControllerBase
    {
        private readonly ILogger<HealthController> _logger;
        private readonly IConfiguration _config;

        public HealthController(ILogger<HealthController> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;
        }

        [HttpGet("ready")]
        public async Task<IActionResult> Ready()
        {
            return Ok("ready and willing");
        }

        [HttpGet("liveness")]
        public async Task<IActionResult> Alive()
        {
            return Ok("okie dokie");
        }
    }
}