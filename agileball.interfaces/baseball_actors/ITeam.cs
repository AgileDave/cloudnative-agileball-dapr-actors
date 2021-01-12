
using System.Threading.Tasks;
using agileball.interfaces.models;
using Dapr.Actors;

namespace agileball.interfaces.baseball_actors
{
    public interface ITeam : IActor
    {
        Task TellHandleTeamStatsForAtBatAsync(atBatStat msg);
        Task TellHandleGameCompleted(completedGame msg);
    }
}