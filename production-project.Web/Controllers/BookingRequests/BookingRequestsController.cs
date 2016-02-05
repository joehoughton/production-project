namespace production_project.Web.Controllers.BookingRequests
{
    using System.Web.Http;
    using production_project.Domain.BookingRequests;
    using production_project.Domain.BookingRequests.Dto;
    using production_project.Domain.Users;

    [RoutePrefix("api")]
    public class BookingRequestsController : ApiController
    {
        private readonly IBookingRequestRepository _bookingRequestRepository;
        private readonly ICurrentUserProvider _currentUserProvider;

        public BookingRequestsController(IBookingRequestRepository bookingRequestRepository, ICurrentUserProvider currentUserProvider)
        {
            _bookingRequestRepository = bookingRequestRepository;
            _currentUserProvider = currentUserProvider;
        }

        [Route("bookings"), HttpPost]
        public IHttpActionResult CreateBookingRequest(BookingRequestDto bookingRequestDto)
        {
            var currentUserId = _currentUserProvider.CurrentUserDetail.UserId;
            var result = _bookingRequestRepository.Create(bookingRequestDto, currentUserId);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }
    }
}