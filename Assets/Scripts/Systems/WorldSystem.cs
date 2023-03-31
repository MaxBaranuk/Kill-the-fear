using Core;
using Models;
using Systems.Interfaces;

namespace Systems
{
    public class WorldSystem: BaseSystem,IWorldSystem
    {
        private WorldModel _worldModel;

        public void SetBiomes(WorldModel worldModel) => 
            _worldModel = worldModel;
        public WorldModel GetBiomes() => 
            _worldModel;
    }
}