using Infrastructure.AssetProvider;
using Infrastructure.Input;
using Infrastructure.SaveLoad;
using UnityEngine;
using Zenject;

namespace Infrastructure
{
    public class BootstrapInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindInput();
            BindSaveLoadService();
        }

        private void BindInput()
        {
            Container.
                Bind<IInput>().
                To<StandaloneInput>().
                AsSingle();
        }

        private void BindSaveLoadService()
        {
            Container.Bind<ISaveLoadService>().To<SaveLoadService>().AsSingle();
            Container.Resolve<ISaveLoadService>().Clear();
        }

    }
}
