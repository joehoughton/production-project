namespace production_project.Domain.BookingRequests
{
    using production_project.Domain.BookingRequests.Dto;

    public interface IBookingRequestRepository
    {
        BookingRequestDto Create(BookingRequestDto bookingRequestDto, string currentUserId);
    }
}
