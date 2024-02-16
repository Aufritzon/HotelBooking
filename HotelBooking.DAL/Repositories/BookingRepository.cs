using HotelBooking.DAL.Entities;
using HotelBooking.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBooking.DAL.Interfaces
{
    public class BookingRepository : IBookingRepository
    {
        public void AddBooking(Booking booking)
        {
            StaticDataStorage.bookings.Add(booking);
        }

        public List<Booking> GetAllBookings()
        {
            return StaticDataStorage.bookings;
        }
    }
}
