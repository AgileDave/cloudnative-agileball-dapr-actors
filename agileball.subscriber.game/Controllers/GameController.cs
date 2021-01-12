using System;
using System.IO;
using System.Threading.Tasks;
using agileball.interfaces.models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.SignalR.Management;
using Microsoft.Extensions.Logging;

namespace agileball.subscriber.game.Controllers
{
    [ApiController]
    //[Route("[controller]")]
    public class GameController : ControllerBase
    {
        private readonly ILogger<GameController> _logger;
        private IServiceHubContext _hub;
        public GameController(ILogger<GameController> logger, IServiceHubContext hub)
        {
            _logger = logger;
            _hub = hub;
        }

        [HttpPost("game")]
        [Dapr.Topic("pubsub", "gameEvent")]
        public async Task<IActionResult> Game([FromBody] gameInfo msg)
        {
            var resp = $"For {msg.gameid} :: INN {msg.inning}  COUNT {msg.balls}-{msg.strikes}-{msg.outs}  {msg.home_team} {msg.home_score} - {msg.visiting_team} {msg.visitor_score}";
            Console.WriteLine(resp);

            await _hub.Clients.All.SendCoreAsync("Receive", new object[] { "Game Actor", resp });

            return Ok();
        }
    }
}