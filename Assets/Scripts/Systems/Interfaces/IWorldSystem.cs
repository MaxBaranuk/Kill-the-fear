using Models;

namespace Systems.Interfaces
{
    public interface IWorldSystem
    {
        public void SetBioms(WorldModel worldModel);
        public WorldModel GetBioms();
    }
}