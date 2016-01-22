namespace production_project.Domain.Organisation
{
    using System.Data.Entity;
    using System.Linq;
    using production_project.Domain.Data;
    using production_project.Domain.Organisation.Dto;

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
                userOrganisation.Name,
                userOrganisation.OrganisationType.Type
             );

            return organisationDto;
        }
    }
}


