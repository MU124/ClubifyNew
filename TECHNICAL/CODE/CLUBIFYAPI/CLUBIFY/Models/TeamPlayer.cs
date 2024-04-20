using System;

namespace Clubify.Models
{
    public class TeamPlayer
    {
        public int TeamPlayerID { get; set; }
        public int? TeamID { get; set; }
        public int? PlayerID { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? ModifiedBy { get; set; }
    }
}
