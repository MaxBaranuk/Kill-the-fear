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
            Container.Bind<IWorldSystem>().To<WorldSystem>().FromComponentOnRoot().AsSingle();
            Container.Bind<ISpawnSystem>().To<SpawnSystem>().FromComponentOnRoot().AsSingle();
            Container.Bind<IBiomSystem>().To<BiomSystem>().FromComponentOnRoot().AsSingle();
            Container.Bind<IUISystem>().To<UISystem>().FromComponentOnRoot().AsSingle();
            Container.Bind<IFearAttackSystem>().To<FearAttackSystem>().FromComponentOnRoot().AsSingle();
        }
    }
}