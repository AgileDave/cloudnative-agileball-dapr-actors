
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using agileball.interfaces.baseball_actors;
using agileball.interfaces.models;
using CsvHelper;
using Dapr.Actors;
using Dapr.Actors.Client;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace agileball.runner.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AtBatController : ControllerBase
    {
        private readonly ILogger<AtBatController> _logger;

        public AtBatController(ILogger<AtBatController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {

            var client = new HttpClient();

            using (var reader = new StringReader(await client.GetStringAsync("https://nalbfiles.blob.core.windows.net/seasons/2019full.csv")))
            using (var csv = new CsvReader(reader, System.Threading.Thread.CurrentThread.CurrentCulture))
            {
                var rows = csv.GetRecordsAsync<gameEvent>();

                var x = 0;
                await foreach (var row in rows)
                {
                    var proxy = ActorProxy.Create<IGameSupervisor>(new ActorId("SUPER"), "GameSupervisor");
                    Console.WriteLine($"Sending game {row.gameid} with score of {row.home_team} {row.home_score} to {row.visiting_team} {row.visitor_score}");
                    await proxy.TellGameEventAsync(row);
                    if (x > 20000)
                    {
                        break;
                    }
                    x++;
                }
            }

            return Ok();
        }
    }
}