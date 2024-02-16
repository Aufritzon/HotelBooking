using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBooking.DAL.Entities
{
    public class Room
    {
        public int Number { get; set; }
        public RoomType Type { get; set; }

    }
    public enum RoomType
    {
        None,
        Single,
        Double
    }
}
