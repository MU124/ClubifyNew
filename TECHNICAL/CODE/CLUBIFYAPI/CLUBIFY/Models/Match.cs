using System;

namespace Clubify.Models
{
    public class Match
    {
        public int MatchID { get; set; }
        public DateTime? MatchDate { get; set; }
        public string Location { get; set; }
        public decimal? Fees { get; set; }
        public int? Team1ID { get; set; }
        public int? Team2ID { get; set; }
        public int? WinnerTeamID { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? ModifiedBy { get; set; }
    }
}
