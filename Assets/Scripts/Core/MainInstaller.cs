using Systems;
using Systems.Interfaces;
using Zenject;

namespace Core
{
    public class MainInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IUserSystem>().To<UserSystem>().FromComponentOnRoot().AsSingle();
        }
    }
}