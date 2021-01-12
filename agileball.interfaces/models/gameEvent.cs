
using System;

namespace agileball.interfaces.models
{

    public class gameEvent
    {
        public gameEvent() => id = Guid.NewGuid();

        public Guid id { get; }
        public string gameid { get; set; }
        public string home_team { get; set; }
        public int year { get; set; }
        public int month { get; set; }
        public int day { get; set; }
        public int daygamenum { get; set; }
        public string visiting_team { get; set; }
        public int inning { get; set; }
        public int is_home_at_bat { get; set; }
        public int outs { get; set; }
        public int balls { get; set; }
        public int strikes { get; set; }
        public string pitch_seq { get; set; }
        public int visitor_score { get; set; }
        public int home_score { get; set; }
        public string batter { get; set; }
        public string batter_hand { get; set; }
        public string pitcher { get; set; }
        public string pitcher_hand { get; set; }
        public string catcher { get; set; }
        public string firstbase { get; set; }
        public string secondbase { get; set; }
        public string thirdbase { get; set; }
        public string shortstop { get; set; }
        public string leftfield { get; set; }
        public string centerfield { get; set; }
        public string rightfield { get; set; }
        public string runner_on_first { get; set; }
        public string runner_on_second { get; set; }
        public string runner_on_third { get; set; }
        public string event_text { get; set; }
        public string is_leadoff { get; set; }
        public string is_pinchhit { get; set; }
        public int lineup_pos { get; set; }
        public int event_type { get; set; }
        public string is_batter_event { get; set; }
        public string is_atbat { get; set; }
        public int hit_value { get; set; }
        public string is_sac_hit { get; set; }
        public string is_sac_fly { get; set; }
        public int outs_on_play { get; set; }
        public string is_double_play { get; set; }
        public string is_triple_play { get; set; }
        public int rbi_on_play { get; set; }
        public string is_wild_pitch { get; set; }
        public string is_passed_ball { get; set; }
        public int fielded_by { get; set; }
        public string batted_ball_type { get; set; }
        public string is_bunt { get; set; }
        public string is_foul { get; set; }
        public string hit_location { get; set; }
        public int errors_on_play { get; set; }
        public int batter_dest { get; set; }
        public int runner_on_first_dest { get; set; }
        public int runner_on_second_dest { get; set; }
        public int runner_on_third_dest { get; set; }
        public string is_new_game { get; set; }
        public string is_end_game { get; set; }
        public int event_number { get; set; }
    }
}