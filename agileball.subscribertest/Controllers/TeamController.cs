using System;
using System.IO;
using System.Threading.Tasks;
using agileball.subscribertest.Models;
using Dapr.Client;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace agileball.subscribertest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TeamController : ControllerBase
    {
        private readonly ILogger<TeamController> _logger;
        public TeamController(ILogger<TeamController> logger, DaprClient client)
        {
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] DeathStarStatus msg)
        {
            Console.WriteLine($"{Request.ContentType} :: {Request.ContentLength} :: {msg.status} :: {msg.message}");
            var sr = new StreamReader(Request.BodyReader.AsStream());
            var body = sr.ReadToEnd();

            Console.WriteLine(body);
            //Console.WriteLine(msg);

            return Ok();
        }
    }
}