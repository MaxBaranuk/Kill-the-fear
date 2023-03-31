using System;
using System.Collections.Generic;
using Enums;
using UnityEngine;

namespace Models
{
    [Serializable]
    public class BiomModel
    {
        public BiomsNames Name;
        public List<Vector3> BiomsPosition;
        public List<EnemyModel> EnemyModels;
    }
}