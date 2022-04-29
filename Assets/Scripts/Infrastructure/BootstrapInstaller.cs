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
            var saveLoadService = new SaveLoadService();
            Container.BindInstance<ISaveLoadService>(saveLoadService).AsSingle();
            saveLoadService.Clear();
        }

    }
}
