namespace production_project.Web.Autofac
{
    using System.Web;
    using System.Web.Http.Validation;
    using FluentValidation;
    using FluentValidation.WebApi;
    using global::Autofac;
    using Microsoft.AspNet.Identity.Owin;
    using production_project.Domain.Organisations;
    using production_project.Domain.Users;
    using production_project.Web.Identity;
    using production_project.Web.Providers;

    public class AutofacWebModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(x => x.Resolve<HttpContextBase>()
                .Request.GetOwinContext()
                .GetUserManager<ApplicationUserManager>())
                .As<ApplicationUserManager>();

            builder.Register(c => new HttpContextWrapper(HttpContext.Current)).As<HttpContextBase>().InstancePerLifetimeScope();

            builder.RegisterType<CurrentUserProvider>().As<ICurrentUserProvider>().InstancePerRequest();
            builder.RegisterType<OrganisationRepository>().As<IOrganisationRepository>().InstancePerRequest();
            builder.RegisterType<UserRepository>().As<IUserRepository>().InstancePerRequest();

            builder.Register(ctx => HttpContext.Current).As<HttpContext>().InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(ThisAssembly)
                               .Where(t => t.Name.EndsWith("Validator"))
                               .AsImplementedInterfaces()
                               .InstancePerLifetimeScope();

            builder.RegisterType<FluentValidationModelValidatorProvider>().As<ModelValidatorProvider>();
            builder.RegisterType<AutofacValidatorFactory>().As<IValidatorFactory>().SingleInstance();
            builder.Register(c => HttpContext.Current.GetOwinContext()
                .Authentication)
                .InstancePerRequest();

            base.Load(builder);
        }
    }
}