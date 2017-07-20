using Microsoft.Owin;
using Owin;
using Autofac;
using Autofac.Integration.WebApi;
using System.Reflection;
using MusicStore.DAL;
using Microsoft.AspNet.Identity.EntityFramework;
using MusicStore.Entities;
using System.Web;
using Microsoft.Owin.Security;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security.DataProtection;
using Microsoft.Owin.Security.DataHandler;
using Microsoft.Owin.Security.DataHandler.Serializer;
using MusicStore.DAL.Infrastructure;
using MusicStore.DAL.Repositories;
using System.Web.Http;
using MusicStore.WEB.Providers;

[assembly: OwinStartup(typeof(MusicStore.WEB.Startup))]
namespace MusicStore.WEB
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var container = AutofacConfig.RegisterServices(new ContainerBuilder());

            var config = GlobalConfiguration.Configuration;

            var resolver = new AutofacWebApiDependencyResolver(container);

            config.DependencyResolver = resolver;

            ConfigureAuth(app, resolver);

            app.UseAutofacMiddleware(container);
            app.UseAutofacWebApi(config);
            app.UseWebApi(config);
        }
    }

    public class AutofacConfig
    {
        public static IContainer Container;

        internal static IContainer RegisterServices(ContainerBuilder builder)
        {
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            builder.RegisterType<ApplicationDbContext>().AsSelf().SingleInstance();

            //Factory Expsoing
            //DI injection to Repository, UnitOfWork and ApplicationUserStore
            builder.RegisterType<DbFactory>().As<IDbFactory>().SingleInstance();

            //Register UnitOfWork and Repository
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().SingleInstance();
            builder.RegisterGeneric(typeof(GenericRepository<>)).As(typeof(IGenericRepository<>)).SingleInstance();

            //ApplicationUserManager DI Injection
            builder.RegisterType<ApplicationUserStore>().As<IApplicationUserStore>().SingleInstance();
            builder.RegisterType<IdentityFactoryOptions<ApplicationUserManager>>().AsSelf().SingleInstance();
            

            //DI injection to Account Controller
            builder.RegisterType<ApplicationUserManager>().AsSelf().SingleInstance();
            builder.Register(c => HttpContext.Current.GetOwinContext().Authentication).As<IAuthenticationManager>();
            builder.RegisterType<TicketDataFormat>().As<ISecureDataFormat<AuthenticationTicket>>();
            builder.RegisterType<TicketSerializer>().As<IDataSerializer<AuthenticationTicket>>();
            builder.Register(c => new DpapiDataProtectionProvider().Create("ASP.NET Identity")).As<IDataProtector>();


            Container = builder.Build();

            return Container;
        }
    }
}
