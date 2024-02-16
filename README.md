Som hotellchef:

AddBooking_ThrowsException_WhenRoomIsAlreadyBooked:

Vill jag se till att en bokning inte kan läggas till för ett rum som redan är bokat, så att konflikter inte uppstår.

AddBooking_ThrowsException_WhenStartDateIsAfterEndDate:

Vill jag förhindra bokningar där startdatumet är efter slutdatumet.

AddBooking_ThrowsException_WhenStartDateIsInPast:

Vill jag inte tillåta bokningar med ett startdatum i det förflutna, så att jag kan förhindra ogiltiga bokningar.

GetAvailableRoomsByType_ReturnsAvailableRooms:

Vill jag hämta en lista över tillgängliga rum av en specifik typ inom ett givet datumintervall, så att jag effektivt kan boka rum till gäster baserat på deras preferenser och tillgänglighet.

AddBooking_ThrowsException_WhenOverlappingBookingExists:

Vill jag förhindra överlappande bokningar för samma rum, så att jag kan undvika konflikter och säkerställa en smidig bokningsprocess för alla gäster.
