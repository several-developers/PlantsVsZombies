using Core.Infrastructure.Services.Common;
using Zenject;

namespace Core.Infrastructure.Installers.Common
{
    public class ServicesInstaller : MonoInstaller
    {
        // PUBLIC METHODS: ------------------------------------------------------------------------

        public override void InstallBindings()
        {
            BindGameDataService();
            BindScenesLoaderService();
        }

        // PRIVATE METHODS: -----------------------------------------------------------------------

        private void BindGameDataService()
        {
            Container
                .BindInterfacesTo<GameDataService>()
                .AsSingle()
                .NonLazy();
        }

        private void BindScenesLoaderService()
        {
            Container
                .BindInterfacesTo<ScenesLoaderService>()
                .AsSingle()
                .NonLazy();
        }
    }
}