namespace production_project.Web.Providers
{
    using System.Linq;
    using System.Security.Claims;
    using System.Threading;
    using System.Web;

    using Microsoft.AspNet.Identity;

    using production_project.Domain.Users;
    using production_project.Domain.Users.Models;

    public class CurrentUserProvider : ICurrentUserProvider
    {
        public CurrentUserProvider(HttpContextBase context)
        {
            var prinicpal = (ClaimsPrincipal)Thread.CurrentPrincipal;
            var role = prinicpal.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).SingleOrDefault();
            var userId = prinicpal.Claims.Where(c => c.Type == ClaimTypes.Sid).Select(c => c.Value).SingleOrDefault();

            CurrentUserDetail = new CurrentUserDetail
            {
                Username = context.User.Identity.GetUserName(),
                UserId = userId,
                Role = role
            };
        }

        public CurrentUserDetail CurrentUserDetail { get; private set; }
    }
}