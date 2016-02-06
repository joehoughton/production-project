namespace production_project.Domain.BookingRequests
{
    using System.Collections.Generic;
    using production_project.Domain.BookingRequests.Dto;

    public interface IBookingRequestRepository
    {
        BookingRequestDto Create(BookingRequestDto bookingRequestDto, string currentUserId);
        ICollection<BookingRequestDto> GetAll(string currentUserId);
    }
}
