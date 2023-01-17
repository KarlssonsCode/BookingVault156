﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingVault156.Models
{
    internal class BookingHistory
    {
        public int Id { get; set; }
        public DateOnly BookedDate { get; set; }
        public int RoomId { get; set; }
        public int DwellerId { get; set; }
        public bool? MutationCheck { get; set; }
        public int? NukaColaAmount { get; set; }
    }
}
