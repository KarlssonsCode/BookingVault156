using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingVault156.Models
{
    public class Dweller
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool? MutationStatus { get; set; }
        public bool? MutationId { get; set; }
    }
}
