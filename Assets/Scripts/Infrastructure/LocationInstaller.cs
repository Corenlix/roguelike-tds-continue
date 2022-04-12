using GameState;
using Infrastructure.AssetProvider;
using Infrastructure.Factory;
using Infrastructure.Popup;
using Infrastructure.StaticData;
using Zenject;

namespace Infrastructure
{
    public class LocationInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IAssetProvider>().To<AssetProvider.AssetProvider>().AsSingle();
            Container.Bind<IStaticDataService>().To<StaticDataService>().AsSingle();
            Container.Bind<ILevelFactory>().To<LevelFactory>().AsSingle();
            Container.Bind<IGameFactory>().To<GameFactory>().AsSingle();
            Container.Bind<PopupSpawner>().AsSingle();

            Container.Bind<GameStateMachine>().AsSingle().NonLazy();
        }
    }
}