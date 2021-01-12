
namespace agileball.interfaces.models
{
    public class atBatStat
    {
        public string team { get; set; }
        public string batter { get; set; }
        public int home_runs { get; set; }
        public int hits { get; set; }
        public int rbis { get; set; }
        public bool is_atbat { get; set; }
        public int hit_value { get; set; }
        public int walks { get; set; }
    }
}