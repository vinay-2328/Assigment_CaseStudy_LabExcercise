﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketBookingSystem.Exception
{
    public class InvalidBookingIDException : System.Exception
    {
        public InvalidBookingIDException(int bookingId)
            : base($"Booking ID {bookingId} is invalid.")
        {
        }
    }

}
