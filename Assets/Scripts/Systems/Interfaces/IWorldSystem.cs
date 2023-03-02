using System.Collections.Generic;
using Models;

namespace Systems.Interfaces
{
    public interface IWorldSystem
    {
        public void SetBioms(List<BiomModel> bioms);
        public List<BiomModel> GetBioms();
    }
}