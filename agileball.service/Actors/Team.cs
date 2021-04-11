
using System;
using System.Threading.Tasks;
using agileball.interfaces.baseball_actors;
using agileball.interfaces.models;
using Dapr.Actors;
using Dapr.Actors.Runtime;

namespace agileball.service.actors
{
    public class Team : Actor, ITeam
    {
        private string _teamId;
        private int _hits, _atBats, _walks, _homeruns, _rbis, _wins, _losses, _games;
        public Team(ActorHost host)
            : base(host)
        {
            _teamId = host.Id.GetId();
        }

        public async Task TellHandleGameCompleted(completedGame msg)
        {
            _games += 1;
            _wins += msg.is_win ? 1 : 0;
            _losses += msg.is_win ? 0 : 1;

            Console.WriteLine($"{_teamId}: {_wins} - {_losses} out of {_games}.\nThey have {_hits} hits in {_atBats} ABs with {_homeruns} HRs and {_rbis} RBIs");
        }

        public async Task TellHandleTeamStatsForAtBatAsync(atBatStat msg)
        {
            _atBats += msg.is_atbat ? 1 : 0;
            _hits += msg.hits;
            _homeruns += msg.home_runs;
            _rbis += msg.rbis;
            _walks += msg.walks;
        }
    }
}