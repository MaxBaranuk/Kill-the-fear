using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

namespace OvercomeTheFear
{
    public class TerrainGenerator : MonoBehaviour
    {
        [SerializeField] Vector2Int _cellsCount;
        [SerializeField] float _cellSize = 10f;

        [SerializeField] GameObject[] _planes;
        [SerializeField] Vector3[] _biomesPositions;

        public int CellsX => _cellsCount.x;
        public int CellsZ => _cellsCount.y;

        public Vector3 Origin { get; private set; }

        void Start()
        {
            GenerateTerrain();
        }

        void Update()
        {

        }


        void GenerateTerrain()
        {
            Origin = GetCenter((1 - CellsX) * _cellSize * 0.5f, (CellsZ - 1) * _cellSize * 0.5f);

            var container = new GameObject("Planes Container").transform;

            for (int i = 0; i < CellsX * CellsZ; i++)
            {
                var pos = IndexToPoint(i);
                var planeObj = DeterminePlane(pos);

                Instantiate(planeObj, IndexToPoint(i), Quaternion.identity, container);
            }

            //int[] arr = new int[_biomesPositions.Length];
            //var pos = GetCenter(0, 0);

            //for (int i = 0; i < 1000000; i++)
            //{
            //    int res = GetWeightedValue(pos);
            //    arr[res]++;
            //}


            int a = 5;
        }

        Vector3 IndexToPoint(int index)
        {
            Vector3 result = Origin;

            int j = index % CellsX;
            int i = index / CellsX;

            result.x += _cellSize * j;
            result.z -= _cellSize * i;

            return result;
        }

        int PointToIndex(Vector3 position)
        {
            var offset = GetCenter(position.x, position.z) - Origin;
            float inverseCS = 1 / _cellSize;

            int i = (int)(-offset.z * inverseCS);
            int j = (int)( offset.x * inverseCS);

            return i * CellsZ + j;
        }

        Vector3 GetCenter(float x, float z)
        {
            // if (x < Origin.x || x > -Origin.x || z < -Origin.z || z > Origin.z)
            //     throw new System.ArgumentException($"x = {x}; z = {z}");

            float cS2 = _cellSize * 0.5f;
            float inverseCS = 1 / _cellSize;

            float sX = Mathf.Sign(x);
            float sZ = Mathf.Sign(z);

            float X = ((int)(x * inverseCS) * _cellSize) + sX * cS2;
            float Z = ((int)(z * inverseCS) * _cellSize) + sZ * cS2;

            return new Vector3(X, 0f, Z);
        }

        GameObject DeterminePlane(Vector3 position)
        {
            int idx = GetWeightedValue(position);
            return _planes[idx];
        }

        int GetWeightedValue(Vector3 position)
        {
            var closestBiomes = GetClosestBiomes(position);
            float[] weights = new float[closestBiomes.Length];

            float minDist = CellsX * CellsZ * _cellSize;
            float refValue = 1f / (minDist * _cellSize);
            float sum = 0f;

            for (int i = 0; i < weights.Length; i++)
            {
                int biome = closestBiomes[i];

                var dist = (position - _biomesPositions[biome]).sqrMagnitude;
                weights[i] = dist < minDist ? 1000000f : 1f - dist * refValue;

                sum += weights[i];
            }

            float rnd = UnityEngine.Random.Range(0f, sum);
            int resBiome = -1;

            for (int i = 0; i < weights.Length; i++)
            {
                if (rnd < weights[i])
                {
                    resBiome = closestBiomes[i];
                    break;
                }

                rnd -= weights[i];
            }

            return resBiome;
        }

        int[] GetClosestBiomes(Vector3 position)
        {
            var distances = _biomesPositions.Select(pos => (position - pos).sqrMagnitude).ToList();
            return distances.OrderBy(x => x).Take(2).Select(dst => distances.IndexOf(dst)).ToArray();
        }
    }
}