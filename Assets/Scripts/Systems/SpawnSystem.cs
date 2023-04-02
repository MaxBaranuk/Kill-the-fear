using System.Collections.Generic;
using System.Linq;
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
        [SerializeField] private GameObject _biomesRoot;
        [SerializeField] private GameObject _interBiomeRoot;
        [SerializeField] private GameObject _enemyRoot;

        [SerializeField] private float _cellSize = 1f;
        [SerializeField] private Bounds _terrainBounds;

        List<BiomeModel> _biomes;
        ColorPoint[] _biomesPositionsAndColors;

        public void SaveBiomes() =>
            _biomes = _worldSystem.GetBiomes().BiomModels;

        public void SpawnBiomes()
        {
            _biomesPositionsAndColors = new ColorPoint[_biomes.Count];

            for (int i = 0; i < _biomes.Count; i++)
            {
                var biomeModel = _biomes[i];
                int biomeIndex = (int)biomeModel.Name;

                var biomeRoot = new GameObject(biomeModel.Name.ToString());
                biomeRoot.transform.SetParent(_biomesRoot.transform);

                // biomeModel.BiomeObjPositions.Reverse();

                bool init = false;
                foreach (var biomePosition in biomeModel.BiomeObjPositions)
                {
                    var obj = Instantiate(_biomsPrefabs[biomeIndex], biomePosition, Quaternion.identity, biomeRoot.transform);

                    if (!init)
                    {
                        init = true;
                        var color = obj.GetComponentInChildren<MeshRenderer>().material.color;
                        _biomesPositionsAndColors[i] = new ColorPoint { point = biomePosition, color = color };
                    }
                }
            }
        }

        public void SmoothBiomesTransitions()
        {
            CreateColorPoints();
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

        void CreateColorPoints()
        {
            Vector3 origin = _terrainBounds.min + new Vector3(0.5f, 0f, 0.5f) * _cellSize;
            Vector3 current = origin;

            for (int z = 0; z < _terrainBounds.size.z; z++)
            {
                for (int x = 0; x < _terrainBounds.size.x; x++)
                {
                    var overlap = Physics.OverlapSphere(current, 0.25f);

                    if (overlap.Length == 0)
                    {
                        var color = GetWeightedColor(current);

                        var plane = GameObject.CreatePrimitive(PrimitiveType.Plane);

                        plane.transform.SetParent(_interBiomeRoot.transform);
                        plane.transform.position = current;
                        plane.transform.localScale *= 0.1f;

                        plane.GetComponent<MeshRenderer>().material.color = color;

                        current.x += _cellSize;
                    }
                    else
                    {
                        var objTransform = overlap[0].transform;
                        current.x += objTransform.localScale.x;

                        x += ((int)objTransform.localScale.x - 1);
                    }
                }

                current.x = origin.x;
                current.z += _cellSize;
            }
        }

        Color GetWeightedColor(Vector3 position)
        {
            var closestBiomes = GetClosestBiomes(position, 2);

            float[] weights = new float[closestBiomes.Length];
            float sum = 0f;

            for (int i = 0; i < weights.Length; i++)
            {
                weights[i] = (position - _biomesPositionsAndColors[closestBiomes[i]].point).sqrMagnitude;
                sum += weights[i];
            }

            float rnd = UnityEngine.Random.Range(0f, sum);
            
            for (int i = 0; i < weights.Length; i++)
            {
                if (rnd < weights[i])
                    return _biomesPositionsAndColors[closestBiomes[i]].color;

                rnd -= weights[i];
            }

            throw new System.Exception("GetWeightedColor");
        }

        int[] GetClosestBiomes(Vector3 position, int count)
        {
            var distances = _biomesPositionsAndColors.Select(pos => (position - pos.point).sqrMagnitude).ToList();
            return distances.OrderBy(x => x).Take(count).Select(dst => distances.IndexOf(dst)).ToArray();
        }
    }

    struct ColorPoint
    {
        public Vector3 point;
        public Color color;
    }
}