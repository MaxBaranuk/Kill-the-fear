using System;
using System.Collections;
using Core;
using Enums;
using Systems.Interfaces;
using UnityEngine;
using Zenject;

namespace Systems
{
    public class UserSystem : BaseSystem,IUserSystem
    {
        [Inject] IFearAttackSystem _fearAttackSystem;
        
        [SerializeField] GameObject player;
        
        private BiomsNames _testBiomName = BiomsNames.FirstBiom;
        private int _playerHP = 100;
        private float _testTime = 1f;
        private Coroutine _attackUser;

        private void Start() => 
            StartCoroutine(FollowPlayerOnFear());

        public int GetUserHP() => 
            _playerHP;

        public void AttackUser() => 
            _attackUser = StartCoroutine(AttackUserInZone());

        public void StopAttackUser() => 
            StopCoroutine(_attackUser);
        
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