using GameState;
using Popup;
using Zenject;

namespace Infrastructure
{
    public class LocationInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IStaticDataService>().To<StaticDataService>().AsSingle();
            Container.Bind<ILevelFactory>().To<LevelFactory>().AsSingle();
            Container.Bind<IGameFactory>().To<GameFactory>().AsSingle();
            Container.Bind<PopupSpawner>().AsSingle();

            Container.Bind<GameStateMachine>().AsSingle().NonLazy();
        }
    }
}