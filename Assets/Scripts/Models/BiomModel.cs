using System;
using System.Collections.Generic;
using Enums;
using UnityEngine;

namespace Models
{
    [Serializable]
    public class BiomeModel
    {
        public BiomesNames Name;
        public List<Vector3> BiomeObjPositions;
        public List<EnemyModel> EnemyModels;
    }
}