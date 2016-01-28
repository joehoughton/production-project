namespace production_project.Web.Controllers.Token
{
    using System.Net;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using System.Web.Http;
    using Microsoft.AspNet.Identity;
    using Microsoft.Owin.Security;
    using production_project.Web.Identity;
    using production_project.Web.Models;

    [RoutePrefix("api/tokens")] 
    public class TokensController : ApiController
    {
        private readonly ApplicationUserManager _userManager;
        private readonly IAuthenticationManager _authentication;

        public TokensController(ApplicationUserManager userManager, IAuthenticationManager authenticationManager)
        {
            _userManager = userManager;
            _authentication = authenticationManager;
        }

        [HttpPost, Route, AllowAnonymous]
        public async Task<IHttpActionResult> CreateToken(Credentials model)
        {
            var user = await _userManager.FindAsync(model.Username, model.Password);
            if (user == null)
            {
                return Unauthorized();
            }

            var identity = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.Id)
            }, DefaultAuthenticationTypes.ApplicationCookie);

            _authentication.SignIn(identity);

            return StatusCode(HttpStatusCode.NoContent);
        }

        [HttpDelete, Route]
        public IHttpActionResult DeleteToken()
        {
            _authentication.SignOut();

            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}