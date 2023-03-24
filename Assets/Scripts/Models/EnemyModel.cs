using System;
using System.Collections.Generic;
using Enums;
using UnityEngine;

namespace Models
{
    [Serializable]
    public class EnemyModel
    {
        public EnemysNames EnemyName;
        public List<Vector3> EnemyPosition;
    }
}