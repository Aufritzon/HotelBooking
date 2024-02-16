using HotelBooking.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBooking.DAL.Interfaces
{
    public interface IRoomRepository
    {
        public List<Room> GetAllRooms();
    }
}
