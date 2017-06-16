using Autofac;
using Autofac.Integration.WebApi;
using bot.ait.codes.Commands;
using bot.ait.codes.Dialogs;
using bot.ait.codes.Services;

namespace bot.ait.codes
{
    public class AutofacBootstrap
    {
        public static void Init(ContainerBuilder builder)
        {
            builder.RegisterInstance(new at_botEntities());
            builder.RegisterType<DataService>();
            builder.RegisterType<MainService>();
            builder.RegisterType<FacebookService>();

            builder.RegisterAssemblyTypes(typeof(StartCommand).Assembly)
                .Where(t => t.IsAssignableTo<BaseCommand>())
                .As<BaseCommand>();

            builder.RegisterType<RootDialog>().InstancePerRequest();
        }
    }
}