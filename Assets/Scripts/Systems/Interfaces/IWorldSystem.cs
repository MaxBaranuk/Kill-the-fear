using Models;

namespace Systems.Interfaces
{
    public interface IWorldSystem
    {
        public void SetBiomes(WorldModel worldModel);
        public WorldModel GetBiomes();
    }
}