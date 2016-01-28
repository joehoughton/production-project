namespace production_project.Web.Controllers.Beds
{
    using System.Web.Http;

    using production_project.Domain.Beds;

    [RoutePrefix("api")]
    public class BedsController : ApiController
    {
        private readonly IBedRepository _bedRepository;

        public BedsController(IBedRepository bedRepository)
        {
            _bedRepository = bedRepository;
        }

        [Route("beds/{id}")]
        public IHttpActionResult GetBedRequestDto(int id)
        {
            var bed = _bedRepository.GetBed(id);
            return Ok(bed);
        }
    }
}
