using Autofac;
using Miners.Presentation.Views;

namespace Miners.Presentation.ContainerDI
{
    public class ContainerConfig
    {
        public static IContainer Configure()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<GameForm>().AsSelf();
            builder.RegisterType<Game>().AsSelf();
            //builder.Register(c => new Game()).SingleInstance();

            //builder.RegisterType<GameForm>()
            //          .WithParameter((pi, c) => pi.ParameterType == typeof(Game),
            //                         (pi, c) => c.Resolve<Game>());

            //builder.RegisterType<MainViewModel>();



            return builder.Build();
        }
    }
}
