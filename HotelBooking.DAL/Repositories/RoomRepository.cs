using HotelBooking.DAL.Entities;
using HotelBooking.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBooking.DAL.Interfaces
{
    public class RoomRepository : IRoomRepository
    {
        public List<Room> GetAllRooms()
        {
            return StaticDataStorage.rooms;
        }
    }
}
