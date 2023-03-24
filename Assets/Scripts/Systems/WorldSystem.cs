using Core;
using Models;
using Systems.Interfaces;

namespace Systems
{
    public class WorldSystem: BaseSystem,IWorldSystem
    {
        private WorldModel _worldModel;

        public void SetBioms(WorldModel worldModel) => 
            _worldModel = worldModel;
        public WorldModel GetBioms() => 
            _worldModel;
    }
}