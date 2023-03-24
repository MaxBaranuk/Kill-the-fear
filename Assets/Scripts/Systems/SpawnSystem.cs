using System.Collections.Generic;
using Core;
using Models;
using Systems.Interfaces;
using UnityEngine;
using Zenject;

namespace Systems
{
    public class SpawnSystem: BaseSystem,ISpawnSystem
    {
        [Inject] IWorldSystem _worldSystem;
        
        [SerializeField] private List<GameObject> _biomsPrefabs;
        [SerializeField] private List<GameObject> _enemysPrefabs;
        [SerializeField] private GameObject _biomRoot;
        [SerializeField] private GameObject _enemyRoot;

        List<BiomModel> _bioms;
        public void SaveBiom() => 
            _bioms=_worldSystem.GetBioms().BiomModels;

        public void SpawnBioms()
        {
            foreach (var biomModel in _bioms)
            {
                int biomIndex = (int)biomModel.Name;
                foreach (var positionBiom in biomModel.BiomsPosition)
                {
                    Instantiate(_biomsPrefabs[biomIndex],positionBiom,Quaternion.identity,_biomRoot.transform);
                }
            }
        }
        public void SpawnEnemys()
        {
            foreach (var biomModel in _bioms)
            {
                foreach (var enemyModel in biomModel.EnemyModels)
                {
                    int enemyIndex = (int)enemyModel.EnemyName;
                    foreach (var positionEnemy in enemyModel.EnemyPosition)
                    {
                        Instantiate(_enemysPrefabs[enemyIndex],positionEnemy,Quaternion.identity,_enemyRoot.transform);
                    }
                }
            }
        }
    }
}