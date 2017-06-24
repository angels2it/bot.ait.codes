using Autofac;
using Autofac.Integration.WebApi;
using bot.ait.codes;
using Hangfire;
using Microsoft.Owin;
using Owin;
using GlobalConfiguration = System.Web.Http.GlobalConfiguration;

[assembly: OwinStartup(typeof(Startup))]
namespace bot.ait.codes
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var builder = new ContainerBuilder();
            builder.RegisterApiControllers(typeof(WebApiApplication).Assembly);
            AutofacBootstrap.Init(builder);
            var container = builder.Build();
            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);

            Hangfire.GlobalConfiguration.Configuration.UseAutofacActivator(container);
            Hangfire.GlobalConfiguration.Configuration.UseSqlServerStorage("hangfire");
            app.UseHangfireServer();
            app.UseHangfireDashboard();

            RecurringJob.AddOrUpdate<BotJob>(job => job.Run(), () => Cron.MinuteInterval(10));
        }
    }
}