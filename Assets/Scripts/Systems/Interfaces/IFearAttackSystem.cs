using Enums;
using UnityEngine;

namespace Systems.Interfaces
{
    public interface IFearAttackSystem
    {
        public void FollowOnAttackPlayer(BiomsNames biomName, Vector3 position);
    }
}