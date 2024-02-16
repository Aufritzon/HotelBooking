using HotelBooking.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBooking.DAL.Repositories
{
    static public class StaticDataStorage
    {
        public static List<Room> rooms = GenerateRooms(20);
        public static List<Booking> bookings = new();



        public static void AddBooking(Booking booking)
        {
            bookings.Add(booking);
        }

        public static List<Room> GenerateRooms(int nRooms)
        {
            List<Room> rooms = new List<Room>();
            List<RoomType> roomTypes = new List<RoomType> { RoomType.Single, RoomType.Double };

            for (int i = 0; i < nRooms; i++)
            {
                rooms.Add(new Room() { Number = i + 1, Type = roomTypes[new Random().Next(0, 2)] });
            }

            return rooms;
        }
    }
}
