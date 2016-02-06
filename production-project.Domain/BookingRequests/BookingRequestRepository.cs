namespace production_project.Domain.BookingRequests
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using production_project.Domain.Data;
    using System.Linq;
    using production_project.Domain.BookingRequests.Dto;
    using production_project.Domain.BookingRequests.Models;
    using production_project.Domain.BookingRequests.Models.Enums;
    using production_project.Domain.Patients.Models;

    public class BookingRequestRepository : IBookingRequestRepository
    {
        private readonly SparebedsContext _context;

        public BookingRequestRepository(SparebedsContext context)
        {
            _context = context;
        }

        public BookingRequestDto Create(BookingRequestDto bookingRequestDto, string currentUserId)
        {
            var user = _context.UserDetails.Single(x => x.AspNetUser.Id == currentUserId);

            var bookingRequest = new BookingRequest
            {
                OrganisationId = bookingRequestDto.OrganisationId,
                UserDetailId = user.Id,
                Notes = bookingRequestDto.Notes,
                BedId = bookingRequestDto.BedId,
                DateRequested = DateTime.Now,
                CurrentStatus = CurrentStatus.Requested,
                Patient = new Patient
                {
                    PasId = bookingRequestDto.Patient.PasId,
                    Age = bookingRequestDto.Patient.Age,
                    Gender = bookingRequestDto.Patient.Gender
                },
                ClinicalInformation = bookingRequestDto.ClinicalInformation
            };

            _context.BookingRequests.Add(bookingRequest);
            _context.SaveChanges();

            return bookingRequestDto;
        }

        public ICollection<BookingRequestDto> GetAll(string currentUserId)
        {
            var userOrganisation = _context.UserDetails
            .Include(o => o.Organisation)
            .Single(o => o.AspNetUser.Id == currentUserId).Organisation;

            var currentUser = _context.UserDetails.First(x => x.AspNetUser.Id == currentUserId);

            var bookingRequests = _context.BookingRequests.Where(x => x.UserDetail.Organisation.Id == userOrganisation.Id || x.OrganisationId == currentUser.Organisation.Id).ToList()
                .Select(br => new BookingRequestDto()
                {
                    BedId = br.BedId,
                    Notes = br.Notes,
                    OrganisationId = br.OrganisationId,
                    UserDetailId = br.UserDetailId,
                    DateRequested = br.DateRequested,
                    OrganisationFrom = br.UserDetailId == currentUser.Id ? _context.Organisations.Find(currentUser.Organisation.Id).Name : br.UserDetail.Organisation.Name,
                    OrganisationTo = br.UserDetailId == currentUser.Id ? _context.Organisations.Find(br.OrganisationId).Name : _context.Organisations.Find(currentUser.Organisation.Id).Name,
                    PasId = _context.Patients.Find(br.PatientId).PasId,
                    CurrentStatus = br.CurrentStatus,
                    Incoming = br.UserDetailId != currentUser.Id,
                    Tier = br.Bed.Tier,
                    Patient = new PatientDto()
                    {
                        Age = br.Patient.Age,
                        Gender = br.Patient.Gender,
                        PasId = br.Patient.PasId
                    }
                }).ToList();

            return new List<BookingRequestDto>(bookingRequests);
        }
    }
}