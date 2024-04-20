using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clubify.Models
{
    public class UserConnect
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public int ConnectedUserID { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
