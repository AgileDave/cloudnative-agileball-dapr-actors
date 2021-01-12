
using System.Threading.Tasks;
using agileball.interfaces.models;
using Dapr.Actors;

namespace agileball.interfaces.baseball_actors
{
    public interface IBatter : IActor
    {
        Task TellHandleBatterPlateAppearanceAsync(batterEvent msg);
    }
}