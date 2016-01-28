namespace production_project.Domain.Organisations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Spatial;
    using System.Linq;
    using production_project.Domain.Beds.Models.Enums;
    using production_project.Domain.Data;
    using production_project.Domain.Organisations.Dto;

    public class OrganisationRepository : IOrganisationRepository
    {
        private readonly SparebedsContext _context;

        public OrganisationRepository(SparebedsContext context)
        {
            _context = context;
        }
       
        public OrganisationDto GetByUserId(string currentUserId)
        {
             var userOrganisation = _context.UserDetails
            .Include(o => o.Organisation)
            .Single(o => o.AspNetUser.Id == currentUserId).Organisation;

             var organisationDto = new OrganisationDto(
                           userOrganisation.Id,
                           userOrganisation.Name,
                           userOrganisation.OrganisationType.Type,
                           userOrganisation.OrganisationType.Id,
                           userOrganisation.GeoLocation.Latitude,
                           userOrganisation.GeoLocation.Longitude);

             return organisationDto;
        }

        public ICollection<OrganisationLocationDto> Search(OrganisationSearchDto searchDto, string userId)
        {
            var userOrganisation = _context.UserDetails
               .Include(o => o.Organisation)
               .Single(o => o.AspNetUser.Id == userId).Organisation;

            const double MetresToMiles = 0.00062137;

            var bedLocations = _context.Beds
                            .Where(b => b.Availability &&
                            b.Ward.OrganisationId != userOrganisation.Id &&
                            (b.Ward.Organisation.GeoLocation.Distance(DbGeography.FromText("POINT (" + searchDto.Longitude + " " + searchDto.Latitude + ")")) * MetresToMiles) < searchDto.Distance);

            if (!searchDto.AllAges)
            {
                bedLocations = bedLocations.Where(b => b.MinAge < searchDto.Age && b.MaxAge > searchDto.Age);
            }

            if (searchDto.Gender != Gender.Both)
            {
                bedLocations = bedLocations.Where(b => b.Gender == searchDto.Gender);
            }

            if (searchDto.Tier != Tier.Any)
            {
                bedLocations = bedLocations.Where(b => b.Tier == searchDto.Tier);
            }

            var organisationLocationDtos = bedLocations.Select(b => new OrganisationLocationDto()
            {
                OrganisationId = b.Ward.OrganisationId,
                OrganisationName = b.Ward.Organisation.Name,
                OrganisationType = b.Ward.Organisation.OrganisationType.Type,
                BedPrice = b.Price,
                BedType = b.Name,
                Tier = b.Tier,
                MinAge = b.MinAge,
                MaxAge = b.MaxAge,
                Gender = b.Gender,
                BedId = b.Id,
                Latitude = b.Ward.Organisation.GeoLocation.Latitude,
                Longitude = b.Ward.Organisation.GeoLocation.Longitude,
                Distance = Math.Truncate(100 * (b.Ward.Organisation.GeoLocation.Distance(DbGeography.FromText("POINT (" + searchDto.Longitude + " " + searchDto.Latitude + ")")) * MetresToMiles).Value) / 100
            })
                .OrderBy(b => b.Distance).Take(4);

            return organisationLocationDtos.ToList();
        }
    }
}


