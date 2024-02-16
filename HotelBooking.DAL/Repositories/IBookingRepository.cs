using HotelBooking.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBooking.DAL.Interfaces
{
    public interface IBookingRepository
    {
        public List<Booking> GetAllBookings();
        public void AddBooking(Booking booking);
    }
}
