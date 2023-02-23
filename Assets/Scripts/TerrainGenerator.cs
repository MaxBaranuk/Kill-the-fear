using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

namespace OvercomeTheFear
{
    public class TerrainGenerator : MonoBehaviour
    {
        [SerializeField] GameObject _grid;
        [SerializeField] TileBase _tileGrass;


        Tilemap _mainTilemap;


        void Start()
        {
            _mainTilemap = _grid.GetComponentInChildren<Tilemap>();
            _mainTilemap.size = Vector3Int.one * 100;
            _mainTilemap.origin = -_mainTilemap.size / 2;

            Fill();
        }

        void Update()
        {

        }

        void Fill()
        {
            _mainTilemap.FloodFill(Vector3Int.zero, _tileGrass);
        }
    }
}