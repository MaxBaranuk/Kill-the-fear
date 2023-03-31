using System.Collections;
using System.Collections.Generic;
using Core;
using Enums;
using Models;
using Systems.Interfaces;
using UnityEngine;
using Zenject;

namespace Systems
{
    public class UserSystem : BaseSystem,IUserSystem
    {
        [Inject] IFearAttackSystem _fearAttackSystem;
        [Inject] IWorldSystem _worldSystem;
        [Inject] ISpawnSystem _spawnSystem;
        
        [SerializeField] GameObject player;
        
        private BiomsNames _testBiomName = BiomsNames.FirstBiom;
        private int _playerHP = 100;
        private float _testTime = 1f;
        private Coroutine _attackUser;

        private void Start()
        {
            var test= System.IO.File.ReadAllText(Application.dataPath + "/SaveWorldData.json");
            WorldModel worldModel = JsonUtility.FromJson<WorldModel>(test);
            CrateAndSaveWorld(worldModel);
            StartCoroutine(FollowPlayerOnFear());
        }

        public int GetUserHP() => 
            _playerHP;

        public void AttackUser()
        {
            _attackUser ??= StartCoroutine(AttackUserInZone());
        }

        public void StopAttackUser()
        {
            if (_attackUser == null) return;
            StopCoroutine(_attackUser);
            _attackUser = null;
        }

        private void CrateAndSaveWorld(WorldModel worldModel)
        {
            _worldSystem.SetBioms(worldModel);
            _spawnSystem.SaveBiom();
            _spawnSystem.SpawnBioms();
            _spawnSystem.SpawnEnemys();
        }
        
        private IEnumerator FollowPlayerOnFear()
        {
            while (true)
            {
                yield return new WaitForSeconds(_testTime);
                _fearAttackSystem.FollowOnAttackPlayer(_testBiomName,player.transform.position);
            }
        }

        private IEnumerator AttackUserInZone()
        {
            while (true)
            {
                yield return new WaitForSeconds(_testTime);
                _playerHP -= 1;
            }
        }
    }
}