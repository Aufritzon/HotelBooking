namespace HotelBooking.Test;
using FluentAssertions;
using HotelBooking.BLL;
using HotelBooking.DAL.Entities;
using HotelBooking.DAL.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

public class BookingServiceTests
{

    [Fact]
    public void AddBooking_ThrowsException_WhenRoomIsAlreadyBooked()
    {
        var roomRepoMock = new Mock<IRoomRepository>();
        var bookingService = new BookingService(new BookingRepository(), roomRepoMock.Object);

        var existingBooking = new Booking
        {
            RoomNumber = 1,
            StartDate = DateOnly.FromDateTime(DateTime.Now.Date.AddDays(5)),
            EndDate = DateOnly.FromDateTime(DateTime.Now.Date.AddDays(10))
        };

        bookingService.AddBooking(existingBooking);

        var newBooking = new Booking
        {
            RoomNumber = 1,
            StartDate = existingBooking.StartDate.AddDays(1),
            EndDate = existingBooking.EndDate.AddDays(1)
        };

        Action act = () => bookingService.AddBooking(newBooking);

        act.Should().Throw<ArgumentException>().WithMessage("Room is already booked.");
    }

    [Fact]
    public void AddBooking_ThrowsException_WhenStartDateIsAfterEndDate()
    {
        var roomRepoMock = new Mock<IRoomRepository>();
        var bookingService = new BookingService(new BookingRepository(), roomRepoMock.Object);

        var booking = new Booking
        {
            RoomNumber = 1,
            StartDate = DateOnly.FromDateTime(DateTime.Now.Date.AddDays(10)),
            EndDate = DateOnly.FromDateTime(DateTime.Now.Date.AddDays(5))
        };

        Action act = () => bookingService.AddBooking(booking);

        act.Should().Throw<ArgumentException>().WithMessage("Start date must be before the end date.");
    }

    [Fact]
    public void AddBooking_ThrowsException_WhenStartDateIsInPast()
    {
        var roomRepoMock = new Mock<IRoomRepository>();
        var bookingService = new BookingService(new BookingRepository(), roomRepoMock.Object);

        var booking = new Booking
        {
            RoomNumber = 1,
            StartDate = DateOnly.FromDateTime(DateTime.Now.Date.AddDays(-1)),
            EndDate = DateOnly.FromDateTime(DateTime.Now.Date.AddDays(5))
        };

        Action act = () => bookingService.AddBooking(booking);

        act.Should().Throw<ArgumentException>().WithMessage("Start date cannot be in the past.");
    }

    [Fact]
    public void GetAvailableRoomsByType_ReturnsAvailableRooms()
    {
        var bookingRepoMock = new Mock<IBookingRepository>();
        var roomRepoMock = new Mock<IRoomRepository>();
        var bookingService = new BookingService(bookingRepoMock.Object, roomRepoMock.Object);

        var startDate = DateOnly.FromDateTime(DateTime.Now.Date.AddDays(5));
        var endDate = DateOnly.FromDateTime(DateTime.Now.Date.AddDays(10));

        var availableRoom = new Room { Number = 1, Type = RoomType.Single };
        var bookedRoom = new Room { Number = 2, Type = RoomType.Double };

        roomRepoMock.Setup(repo => repo.GetAllRooms()).Returns(new List<Room> { availableRoom, bookedRoom });

        bookingRepoMock.Setup(repo => repo.GetAllBookings()).Returns(new List<Booking>
        {
            new Booking { RoomNumber = 2, StartDate = startDate, EndDate = endDate }
        });

        var result = bookingService.GetAvailableRoomsByType(RoomType.Single, startDate, endDate);

        result.Should().ContainSingle().Which.Should().Be(availableRoom);
    }

    [Fact]
    public void AddBooking_ThrowsException_WhenOverlappingBookingExists()
    {
        var bookingRepoMock = new Mock<IBookingRepository>();
        var roomRepoMock = new Mock<IRoomRepository>();
        var bookingService = new BookingService(bookingRepoMock.Object, roomRepoMock.Object);

        var existingBooking = new Booking
        {
            RoomNumber = 1,
            StartDate = DateOnly.FromDateTime(DateTime.Now.Date.AddDays(5)),
            EndDate = DateOnly.FromDateTime(DateTime.Now.Date.AddDays(10))
        };

        bookingRepoMock.Setup(repo => repo.GetAllBookings()).Returns(new List<Booking> { existingBooking });

        var overlappingBooking = new Booking
        {
            RoomNumber = 1,
            StartDate = existingBooking.StartDate.AddDays(2), 
            EndDate = existingBooking.EndDate.AddDays(-2)
        };

        Action act = () => bookingService.AddBooking(overlappingBooking);

        act.Should().Throw<ArgumentException>().WithMessage("Room is already booked.");
    }

}

