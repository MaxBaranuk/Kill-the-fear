using Enums;
using UnityEngine;

namespace Systems.Interfaces
{
    public interface IFearAttackSystem
    {
        public void FollowOnAttackPlayer(BiomesNames biomName, Vector3 playerPosition);
    }
}