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
        public void FollowOnAttackPlayer(BiomsNames biomName,Vector3 playerPosition)
        {
            foreach (var biomModel in _worldSystem.GetBioms().BiomModels.Where(biomModel => biomModel.Name == biomName))
                FindPlayerInBiom(biomModel, playerPosition);
        }

        private void FindPlayerInBiom(BiomModel biomModel,Vector3 playerPosition)
        {
            foreach (var enemyModel in biomModel.EnemyModels)
            {
                foreach (var enemyPosition in enemyModel.EnemyPosition)
                {
                    if ((enemyPosition.x + AttackDistance > playerPosition.x &&
                        enemyPosition.x - AttackDistance < playerPosition.x) &&
                        (enemyPosition.z + AttackDistance > playerPosition.z &&
                        enemyPosition.z - AttackDistance < playerPosition.z))
                        _userSystem.AttackUser();
                    else
                        _userSystem.StopAttackUser();
                }
            }
        }
    }
}