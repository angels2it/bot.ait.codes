using System.Collections.Generic;
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

            builder.RegisterAssemblyTypes(typeof(StartCommandHandler).Assembly)
                .Where(t => t.IsAssignableTo<BaseCommandHandler>())
                .As<BaseCommandHandler>();

            builder.Register(context =>
            {
                var commandHandlerList = new CommandHandlerList();
                var handlers = context.Resolve<IEnumerable<BaseCommandHandler>>();
                foreach (var handler in handlers)
                {
                    commandHandlerList.Add(handler);
                }
                return commandHandlerList;
            }).SingleInstance();

            builder.RegisterType<RootDialog>().InstancePerRequest();
            builder.RegisterType<BotJob>();
        }
    }
}