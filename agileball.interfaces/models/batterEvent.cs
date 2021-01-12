
namespace agileball.interfaces.models
{
    public class batterEvent
    {
        public string batter { get; set; }
        public string is_atbat { get; set; }
        public int hit_value { get; set; }
        public string is_sac_hit { get; set; }
        public string is_sac_fly { get; set; }
        public int rbi_on_play { get; set; }
        public string is_bunt { get; set; }
        public string event_text { get; set; }
        public int event_type { get; set; }

    }
}