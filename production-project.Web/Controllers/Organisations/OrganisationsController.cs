namespace production_project.Web.Controllers.Organisations
{
    using System.Linq;
    using System.Web.Http;
    using production_project.Domain.Organisations;
    using production_project.Domain.Organisations.Dto;
    using production_project.Domain.Users;

    [Authorize]
    [RoutePrefix("api")]
    public class OrganisationsController : ApiController
    {
        private readonly IOrganisationRepository _organisationRepository;
        private readonly ICurrentUserProvider _currentUserProvider;

        public OrganisationsController(IOrganisationRepository organisationRepository, ICurrentUserProvider currentUserProvider)
        {
            _organisationRepository = organisationRepository;
            _currentUserProvider = currentUserProvider;
        }

        [Route("users/current/organisation")]
        public IHttpActionResult GetOrganisation()
        {
            var currentUserId = _currentUserProvider.CurrentUserDetail.UserId;
            var organisation = _organisationRepository.GetByUserId(currentUserId);
         
            return Ok(organisation);
        }

        [Route("organisations/search"), HttpPost]
        public IHttpActionResult GetOrganisationSearch(OrganisationSearchDto searchDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var currentUserId = _currentUserProvider.CurrentUserDetail.UserId;
            var organisationSearchDtos = _organisationRepository.Search(searchDto, currentUserId);

            if (!organisationSearchDtos.Any())
            {
                return NotFound();
            }

            return Ok(organisationSearchDtos);
        }

    }
}
