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
        public void FollowOnAttackPlayer(BiomsNames biomName,Vector3 position)
        {
            foreach (var biomModel in _worldSystem.GetBioms().Where(biomModel => biomModel.name == biomName))
                FindPlayerInBiom(biomModel, position);
        }

        private void FindPlayerInBiom(BiomModel biomModel,Vector3 position)
        {
            if (biomModel.EnemyPosition.x + 5 < position.x || biomModel.EnemyPosition.x - 5 > position.x ||
                biomModel.EnemyPosition.y + 5 < position.y || biomModel.EnemyPosition.y - 5 > position.y)
                _userSystem.AttackUser();
            else
                _userSystem.StopAttackUser();
        }
    }
}