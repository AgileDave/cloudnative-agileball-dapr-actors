
using System;
using System.Threading.Tasks;
using agileball.interfaces.baseball_actors;
using agileball.interfaces.models;
using Dapr.Actors;
using Dapr.Actors.Runtime;

namespace agileball.service.actors
{
    public class Batter : Actor, IBatter
    {
        private string _batterId;
        private int _atBats, _hits, _walks, _homeruns, _rbi, _strikeouts, _stolenBases, _ibb, _hbp, _2b, _3b, _bases;
        public Batter(ActorHost host) : base(host)
        {
            _batterId = host.Id.GetId();
        }

        public async Task TellHandleBatterPlateAppearanceAsync(batterEvent msg)
        {
            _atBats += (msg.is_atbat == "T") ? 1 : 0;

            switch (msg.event_type)
            {
                case 3:
                    _strikeouts += 1;
                    break;
                case 4:
                    _stolenBases += 1;
                    break;
                case 14:
                    _walks += 1;
                    break;
                case 15:
                    _ibb += 1;
                    break;
                case 16:
                    _hbp += 1;
                    break;
                case 20:
                    _hits += 1;
                    break;
                case 21:
                    _hits += 1;
                    _2b += 1;
                    break;
                case 22:
                    _hits += 1;
                    _3b += 1;
                    break;
                case 23:
                    _hits += 1;
                    _homeruns += 1;
                    Console.WriteLine($"Homerun number {_homeruns} for {_batterId}!!!");
                    break;
                default:
                    break;
            }
            _bases += msg.hit_value;
            _rbi += msg.rbi_on_play;

            Console.WriteLine($"{_batterId} currently hitting with {_hits} hits and {_strikeouts} SOs in {_atBats} ABs");
        }
    }
}