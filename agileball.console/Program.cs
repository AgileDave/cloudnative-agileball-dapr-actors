using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using agileball.interfaces.baseball_actors;
using agileball.interfaces.models;
using CsvHelper;
using Dapr.Actors;
using Dapr.Actors.Client;

namespace agileball.console
{
    class Program
    {
        static async Task Main(string[] args)
        {
            using (var file = File.OpenRead(@"C:\data\retrosheet\2010s\2019full.csv"))
            using (var reader = new StreamReader(file))
            using (var csv = new CsvReader(reader, System.Threading.Thread.CurrentThread.CurrentCulture))
            {
                var rows = csv.GetRecordsAsync<gameEvent>();

                await foreach (var row in rows)
                {
                    var proxy = ActorProxy.Create<IGameSupervisor>(new ActorId("SUPER"), "GameSupervisor");

                    Console.WriteLine($"Sending game {row.gameid} with score of {row.home_team} {row.home_score} to {row.visiting_team} {row.visitor_score}");

                    await proxy.TellGameEventAsync(row);

                    //var json = JsonSerializer.Serialize<gameEvent>(row);

                }
            }
        }
    }
}
