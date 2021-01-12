
using System;
using System.Threading.Tasks;
using agileball.interfaces.baseball_actors;
using agileball.interfaces.models;
using Dapr.Actors;
using Dapr.Actors.Client;
using Dapr.Actors.Runtime;
using Dapr.Client;

namespace agileball.service.actors
{
    public class GameEvent : Actor, IGame
    {
        private string _gameId, _homeTeam, _visitingTeam;
        private int _homeScore, _visitorScore;
        public GameEvent(ActorService actorService, ActorId actorId)
            : base(actorService, actorId)
        {
            _gameId = actorId.GetId();
            _homeScore = 0;
            _visitorScore = 0;
        }

        public async Task TellGameEventAsync(gameInfo evt)
        {
            var resp = $"For {_gameId} :: INN {evt.inning}  COUNT {evt.balls}-{evt.strikes}-{evt.outs}  {evt.home_team} {evt.home_score} - {evt.visiting_team} {evt.visitor_score}";
            Console.WriteLine(resp);

            await Helpers.DaprClient.PublishEventAsync<gameInfo>("pubsub", "gameEvent", evt);

            if (evt.is_end_game == "T")
            {
                var homeTeamProxy = ActorProxy.Create<ITeam>(new ActorId(evt.home_team), "Team");
                var visitingTeamProxy = ActorProxy.Create<ITeam>(new ActorId(evt.visiting_team), "Team");

                var homeMsg = new completedGame
                {
                    game_count = 1,
                    team = evt.home_team,
                    is_win = (evt.home_score > evt.visitor_score)
                };

                var visitMsg = new completedGame
                {
                    game_count = 1,
                    team = evt.visiting_team,
                    is_win = evt.visitor_score > evt.home_score
                };

                await homeTeamProxy.TellHandleGameCompleted(homeMsg);
                await visitingTeamProxy.TellHandleGameCompleted(visitMsg);
            }
        }


        /// <summary>
        /// This method is called whenever an actor is activated.
        /// An actor is activated the first time any of its methods are invoked.
        /// </summary>
        protected override Task OnActivateAsync()
        {
            // Provides opportunity to perform some optional setup.
            Console.WriteLine($"Activating actor id: {this.Id}");
            return Task.CompletedTask;
        }

        /// <summary>
        /// This method is called whenever an actor is deactivated after a period of inactivity.
        /// </summary>
        protected override Task OnDeactivateAsync()
        {
            // Provides Opporunity to perform optional cleanup.
            Console.WriteLine($"Deactivating actor id: {this.Id}");
            return Task.CompletedTask;
        }
    }
}