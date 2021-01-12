using System;

namespace agileball.interfaces.models
{
    public class gameInfo
    {
        public gameInfo() => id = Guid.NewGuid();

        public Guid id { get; }
        public string gameid { get; set; }
        public string home_team { get; set; }
        public string visiting_team { get; set; }
        public int inning { get; set; }
        public bool is_top_of_inning { get; set; }
        public int outs { get; set; }
        public int balls { get; set; }
        public int strikes { get; set; }
        public int visitor_score { get; set; }
        public int home_score { get; set; }
        public string is_new_game { get; set; }
        public string is_end_game { get; set; }

    }
}