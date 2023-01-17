using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingVault156.Models
{
    public class BookingHistory
    {
        public int Id { get; set; }
        public int BookedWeek { get; set; }
        public int BookedDay{ get; set; }
        public int RoomId { get; set; }
        public int DwellerId { get; set; }
        public bool? MutationCheck { get; set; }
        public int? NukaColaAmount { get; set; }
    }
}
