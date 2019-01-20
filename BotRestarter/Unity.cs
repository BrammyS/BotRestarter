using BotRestarter.Commands;
using BotRestarter.Interfaces;
using BotRestarter.Logger.Commands;
using BotRestarter.Logger.Interfaces;
using BotRestarter.Timers;
using Unity;
using Unity.Lifetime;
using Unity.Resolution;

namespace BotRestarter
{
    public static class Unity
    {
        private static UnityContainer container;

        private static UnityContainer Container
        {
            get
            {
                if (container == null)
                    RegisterTypes();
                return container;
            }
        }

        /// <summary>
        /// Registers objects to the <see cref="container"/>
        /// </summary>
        public static void RegisterTypes()
        {
            container = new UnityContainer();
            container.RegisterType<ILogger, Logger.Logger>(new PerThreadLifetimeManager());
            container.RegisterSingleton<IBotRestarter, BotRestarter>();
            container.RegisterSingleton<IBotReader, BotReader>();
            container.RegisterSingleton<IConsoleCommands, ConsoleCommands>();
            container.RegisterSingleton<ConsoleReSetterTimer>();
        }


        /// <summary>
        /// Resolve a objects that is registered in the <see cref="container"/>.
        /// </summary>
        /// <typeparam name="T">The object you want to resolve</typeparam>
        /// <returns>The resolved object.</returns>
        public static T Resolve<T>()
        {
            return (T)Container.Resolve(typeof(T), string.Empty, new CompositeResolverOverride());
        }
    }
}
