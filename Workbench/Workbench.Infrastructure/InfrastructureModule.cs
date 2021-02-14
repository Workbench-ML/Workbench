using Ninject;
using Ninject.Modules;

using Workbench.Infrastructure.Services;

namespace Workbench.Infrastructure
{
    public class InfrastructureModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ILoggingService>().To<LoggingService>();
        }
    }
}
