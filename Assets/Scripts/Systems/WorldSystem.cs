using System.Collections.Generic;
using Core;
using Models;
using Systems.Interfaces;

namespace Systems
{
    public class WorldSystem: BaseSystem,IWorldSystem
    {
        private List<BiomModel> _biomModels;

        public void SetBioms(List<BiomModel> bioms) => 
            _biomModels = bioms;
        public List<BiomModel> GetBioms() => 
            _biomModels;
    }
}