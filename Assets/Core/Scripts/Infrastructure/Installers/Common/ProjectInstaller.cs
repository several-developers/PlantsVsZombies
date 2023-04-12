using Core.Utilities;
using Zenject;

namespace Core.Infrastructure.Installers.Common
{
    public class ProjectInstaller : MonoInstaller, ICoroutineRunner
    {
        // PUBLIC METHODS: ------------------------------------------------------------------------

        public override void InstallBindings() => BindCoroutineRunner();

        // PRIVATE METHODS: -----------------------------------------------------------------------

        private void BindCoroutineRunner()
        {
            Container
                .Bind<ICoroutineRunner>()
                .FromInstance(this)
                .AsSingle()
                .NonLazy();
        }
    }
}
