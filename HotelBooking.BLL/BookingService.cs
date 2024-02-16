using HotelBooking.DAL.Entities;
using HotelBooking.DAL.Interfaces;
using AutoMapper;

namespace HotelBooking.BLL
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _bookingRepo;
        private readonly IRoomRepository _roomRepo;

        public BookingService(IBookingRepository bookingRepo, IRoomRepository roomRepo) 
        { 
            _bookingRepo = bookingRepo;
            _roomRepo = roomRepo;
        }

        public List<Room> GetAvailableRoomsByType(RoomType roomType, DateOnly startDate, DateOnly endDate)
        {
            ValidateBookingDates(startDate, endDate);
            List<Room> rooms = _roomRepo.GetAllRooms();

            return rooms.Where(r => r.Type == roomType && !IsBookedRoom(r.Number, startDate, endDate)).ToList();
        }

        public void AddBooking(Booking booking)
        {
            if (IsBookedRoom(booking.RoomNumber, booking.StartDate, booking.EndDate))
                throw new ArgumentException("Room is already booked.");

            ValidateBookingDates(booking.StartDate, booking.EndDate);

            _bookingRepo.AddBooking(booking);
        }

        private void ValidateBookingDates(DateOnly startDate, DateOnly endDate)
        {
            if (startDate == default || endDate == default)
            {
                throw new ArgumentException("Start date and end date must be provided.");
            }

            if (startDate >= endDate)
            {
                throw new ArgumentException("Start date must be before the end date.");
            }

            if (startDate < DateOnly.FromDateTime(DateTime.Now))
            {
                throw new ArgumentException("Start date cannot be in the past.");
            }
        }

        private bool IsBookedRoom(int roomNumber, DateOnly startDate, DateOnly endDate) 
        {
            return _bookingRepo.GetAllBookings().Any(b => roomNumber == b.RoomNumber && InterferingDates(b, startDate, endDate));
        }

        private bool InterferingDates(Booking booking, DateOnly startDate, DateOnly endDate)
        {
            return booking.StartDate < endDate && booking.EndDate > startDate;
        }
    }
}