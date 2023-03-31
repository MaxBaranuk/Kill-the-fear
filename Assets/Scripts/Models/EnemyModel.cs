using System;
using System.Collections.Generic;
using Enums;
using UnityEngine;

namespace Models
{
    [Serializable]
    public class EnemyModel
    {
        public EnemiesNames EnemyName;
        public List<Vector3> EnemyPosition;
    }
}