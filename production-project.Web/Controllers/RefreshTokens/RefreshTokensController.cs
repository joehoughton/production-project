namespace production_project.Web.Controllers.RefreshTokens
{
    using System.Threading.Tasks;
    using System.Web.Http;

    using production_project.Web.Authorization;

    [RoutePrefix("api/RefreshTokens")]
    public class RefreshTokensController : ApiController
    {
        private readonly AuthRepository _repo;

        public RefreshTokensController()
        {
            _repo = new AuthRepository();
        }

        // [Authorize(Users="Admin")]
        // Todo: Implement roles http://bitoftech.net/2015/03/11/asp-net-identity-2-1-roles-based-authorization-authentication-asp-net-web-api/
        [Route("")]
        public IHttpActionResult Get()
        {
            return Ok(_repo.GetAllRefreshTokens());
        }

        // [Authorize(Users = "Admin")]
        [AllowAnonymous]
        [Route("")]
        public async Task<IHttpActionResult> Delete(string tokenId)
        {
            var result = await _repo.RemoveRefreshToken(tokenId);
            if (result)
            {
                return Ok();
            }
            return BadRequest("Token Id does not exist");
            
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _repo.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}
