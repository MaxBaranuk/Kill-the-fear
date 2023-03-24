using System;
using System.Collections.Generic;
using System.Linq;
using Enums;
using Models;
using UnityEngine;

namespace WorldEditor.Scripts
{
    public class WorldSetting : MonoBehaviour
    {
        private WorldModel _saveData;
        private List<GameObject> _allGameObjects;

        public void Save()
        {
            _allGameObjects = FindObjectsOfType<GameObject>().ToList();
            _saveData = new WorldModel
            {
                BiomModels = CreateBiomModels()
            };
            SaveIntoJson();
            Debug.LogError("Save");
        }

        void SaveIntoJson()
        {
            string potion = JsonUtility.ToJson(_saveData);
            System.IO.File.WriteAllText(Application.dataPath + "/SaveWorldData.json", potion);
        }

        List<BiomModel> CreateBiomModels()
        {
            List<BiomModel> biomModels = new List<BiomModel>();
            List<GameObject> items = _allGameObjects.Where(element => element.layer == 3).ToList();
            var itemsNames = Enum.GetValues(typeof(BiomsNames)).Cast<BiomsNames>().ToList();
            int index = 0;
            foreach (var itemName in itemsNames)
            {
                var sortItems = items.Where(biom => biom.CompareTag(itemName.ToString())).ToList();
                List<Vector3> itemPositions = sortItems.Select(item => item.transform.position).ToList();
                var enemyModels = CreateEnemyModels(index);
                biomModels.Add(new BiomModel
                {
                    Name = itemName,
                    BiomsPosition = itemPositions,
                    EnemyModels = enemyModels
                });
                index++;
            }
            return biomModels;
        }

        List<EnemyModel> CreateEnemyModels(int enumIndex)
        {
            List<EnemyModel> enemyModels = new List<EnemyModel>();
            List<GameObject> items = _allGameObjects.Where(element => element.layer == 6).ToList();

            EnemysNames enemyName = (EnemysNames)enumIndex;
            var sortEnemys = items.Where(enemy => enemy.CompareTag(enemyName.ToString())).ToList();
            List<Vector3> itemPositions = sortEnemys.Select(item => item.transform.position).ToList();

            enemyModels.Add(new EnemyModel
            {
                EnemyName = enemyName,
                EnemyPosition = itemPositions
            });
            return enemyModels;
        }

    }
}