namespace production_project.Domain.Beds
{
    using System.Collections.Generic;
    using System.Linq;
    using production_project.Domain.Beds.Dto;
    using production_project.Domain.Data;

    public class BedRepository : IBedRepository
    {
        private readonly SparebedsContext _context;

        public BedRepository(SparebedsContext context)
        {
            _context = context;
        }

        public BedRequestDto GetBed(int id)
        {
            var bed = _context.Beds.Find(id);
            var organisation = bed.Ward.Organisation;
            var organisationContactList = new List<string>();

            var organisationContacts = _context.UserDetails
                .Where(x => x.Organisation.Id == organisation.Id &&
                 x.EmailNotification).Select(x => x.AspNetUser.Email).Where(x => x != null)
                .Select(email => email).ToList();

            organisationContactList.AddRange(organisationContacts);

            var bedDto = new BedRequestDto
            {
                Id = bed.Id,
                Price = bed.Price,
                Tier = bed.Tier,
                OrganisationId = organisation.Id,
                OrganisationName = organisation.Name,
                OrganisationType = organisation.OrganisationType.Type,
                OrganisationContact = organisationContactList,
            };

            return bedDto;
        }
    }
}
