using HotelBooking.DAL.Entities;

namespace HotelBooking.BLL
{
    public interface IBookingService
    {
        public List<Room> GetAvailableRoomsByType(RoomType roomType, DateOnly startDate, DateOnly endDate);
        public void AddBooking(Booking booking);
    }
}
