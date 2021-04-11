
using System.Threading.Tasks;
using agileball.interfaces.baseball_actors;
using agileball.interfaces.models;
using Dapr.Actors;
using Dapr.Actors.Client;
using Dapr.Actors.Runtime;

namespace agileball.service.actors
{
    public class GameSupervisor : Actor, IGameSupervisor
    {

        public GameSupervisor(ActorHost host)
            : base(host)
        {
        }


        public async Task TellGameEventAsync(gameEvent evt)
        {
            var proxy = ActorProxy.Create<IGame>(new ActorId(evt.gameid), "GameEvent");
            var batterProxy = ActorProxy.Create<IBatter>(new ActorId(evt.batter), "Batter");

            var batterTeam = evt.is_home_at_bat == 0 ? evt.visiting_team : evt.home_team;
            var teamProxy = ActorProxy.Create<ITeam>(new ActorId(batterTeam), "Team");

            var gameMsg = new gameInfo
            {
                balls = evt.balls,
                gameid = evt.gameid,
                home_score = evt.home_score,
                home_team = evt.home_team,
                inning = evt.inning,
                is_end_game = evt.is_end_game,
                is_new_game = evt.is_new_game,
                is_top_of_inning = evt.is_home_at_bat == 0,
                outs = evt.outs,
                strikes = evt.strikes,
                visiting_team = evt.visiting_team,
                visitor_score = evt.visitor_score
            };

            var batterMsg = new batterEvent
            {
                batter = evt.batter,
                event_text = evt.event_text,
                event_type = evt.event_type,
                hit_value = evt.hit_value,
                is_atbat = evt.is_atbat,
                is_bunt = evt.is_bunt,
                is_sac_fly = evt.is_sac_fly,
                is_sac_hit = evt.is_sac_hit,
                rbi_on_play = evt.rbi_on_play
            };

            var teamAtBatMsg = new atBatStat
            {
                batter = evt.batter,
                hit_value = evt.hit_value,
                hits = evt.hit_value > 0 ? 1 : 0,
                home_runs = evt.hit_value == 4 ? 1 : 0,
                is_atbat = evt.is_atbat == "T",
                rbis = evt.rbi_on_play,
                team = batterTeam,
                walks = evt.event_number == 14 ? 1 : 0
            };

            await proxy.TellGameEventAsync(gameMsg);
            await batterProxy.TellHandleBatterPlateAppearanceAsync(batterMsg);
            await teamProxy.TellHandleTeamStatsForAtBatAsync(teamAtBatMsg);
        }
    }
}