using System.Linq;
using Core;
using Enums;
using Models;
using Systems.Interfaces;
using UnityEngine;
using Zenject;

namespace Systems
{
    public class FearAttackSystem: BaseSystem,IFearAttackSystem
    {
        [Inject] IWorldSystem _worldSystem;
        [Inject] IUserSystem _userSystem;

        private const int AttackDistance = 5;
        public void FollowOnAttackPlayer(BiomesNames biomName,Vector3 playerPosition)
        {
            foreach (var biomModel in _worldSystem.GetBiomes().BiomModels.Where(biomModel => biomModel.Name == biomName))
                FindPlayerInBiome(biomModel, playerPosition);
        }

        private void FindPlayerInBiome(BiomeModel biomModel,Vector3 playerPosition)
        {
            float sqrDist = AttackDistance * AttackDistance;

            foreach (var enemyModel in biomModel.EnemyModels)
            {
                foreach (var enemyPosition in enemyModel.EnemyPosition)
                {
                    if ((enemyPosition - playerPosition).sqrMagnitude < sqrDist)
                        _userSystem.AttackUser();
                    else
                        _userSystem.StopAttackUser();
                }
            }
        }
    }
}