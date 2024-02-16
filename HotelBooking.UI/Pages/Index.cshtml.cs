using HotelBooking.BLL;
using HotelBooking.DAL.Entities;
using HotelBooking.DAL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HotelBooking.UI.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IBookingService _bookingService;
        public List<Room> Rooms { get; set; } = new List<Room>();
        [BindProperty]
        public int SelectedRoomNumber { get; set; }
        [BindProperty(SupportsGet = true)]

        public DateOnly StartDate { get; set; }
        [BindProperty(SupportsGet = true)]
        public DateOnly EndDate { get; set; }
        [BindProperty(SupportsGet = true)]
        public RoomType Type { get; set; }

        public IndexModel(IBookingService bookingService) 
        { 
            _bookingService = bookingService;
        }

        public void OnGet()
        {
            if (StartDate != DateOnly.MinValue && EndDate != DateOnly.MinValue && Type != RoomType.None)
            {
                Rooms = _bookingService.GetAvailableRoomsByType(Type, StartDate, EndDate);
            }
        }
       
        public void OnPost()
        {
            _bookingService.AddBooking(new Booking() { RoomNumber = SelectedRoomNumber, EndDate = EndDate, StartDate = StartDate });
        }
    }
}