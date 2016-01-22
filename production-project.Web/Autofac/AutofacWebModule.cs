namespace production_project.Web.Autofac
{
    using System.Web;

    using global::Autofac;

    using production_project.Domain.Organisation;
    using production_project.Domain.Users;
    using production_project.Web.Providers;

    public class AutofacWebModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // used in CurrentUserProvider
            builder.Register(c => new HttpContextWrapper(HttpContext.Current)).As<HttpContextBase>().InstancePerLifetimeScope();
            builder.Register(ctx => HttpContext.Current).As<HttpContext>().InstancePerLifetimeScope();

            builder.RegisterType<OrganisationRepository>().As<IOrganisationRepository>().InstancePerRequest();
            builder.RegisterType<UserRepository>().As<IUserRepository>().InstancePerRequest();
            builder.RegisterType<CurrentUserProvider>().As<ICurrentUserProvider>().InstancePerRequest();

            base.Load(builder);
        }
    }
}