
using System.Threading.Tasks;
using agileball.interfaces.models;
using Dapr.Actors;

namespace agileball.interfaces.baseball_actors
{
    public interface IGame : IActor
    {
        Task TellGameEventAsync(gameInfo msg);
    }
}