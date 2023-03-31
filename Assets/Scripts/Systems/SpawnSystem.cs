using System.Collections.Generic;
using Core;
using Models;
using Systems.Interfaces;
using UnityEngine;
using Zenject;

namespace Systems
{
    public class SpawnSystem : BaseSystem, ISpawnSystem
    {
        [Inject] IWorldSystem _worldSystem;

        [SerializeField] private List<GameObject> _biomsPrefabs;
        [SerializeField] private List<GameObject> _enemysPrefabs;
        [SerializeField] private GameObject _biomRoot;
        [SerializeField] private GameObject _enemyRoot;

        List<BiomeModel> _biomes;

        public void SaveBiomes() =>
            _biomes = _worldSystem.GetBiomes().BiomModels;

        public void SpawnBiomes()
        {
            foreach (var biomeModel in _biomes)
            {
                int biomeIndex = (int)biomeModel.Name;

                var biomeRoot = new GameObject(biomeModel.Name.ToString());
                biomeRoot.transform.SetParent(_biomRoot.transform);

                // biomeModel.BiomeObjPositions.Reverse();

                foreach (var biomePosition in biomeModel.BiomeObjPositions)
                    Instantiate(_biomsPrefabs[biomeIndex], biomePosition, Quaternion.identity, biomeRoot.transform);
            }
        }

        public void SmoothBiomesTransitions()
        {

        }

        public void SpawnEnemies()
        {
            foreach (var biomModel in _biomes)
            {
                foreach (var enemyModel in biomModel.EnemyModels)
                {
                    int enemyIndex = (int)enemyModel.EnemyName;

                    foreach (var positionEnemy in enemyModel.EnemyPosition)
                    {
                        Instantiate(_enemysPrefabs[enemyIndex], positionEnemy, Quaternion.identity, _enemyRoot.transform);
                    }
                }
            }
        }
    }
}