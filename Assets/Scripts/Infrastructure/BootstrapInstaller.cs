using Infrastructure.Input;
using Zenject;

namespace Infrastructure
{
    public class BootstrapInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindInput();
        }

        private void BindInput()
        {
            Container.
                Bind<IInput>().
                To<StandaloneInput>().
                AsSingle();
        }
    }
}
