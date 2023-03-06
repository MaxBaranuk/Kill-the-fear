using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace WorldEditor.Scripts
{
    public class WorldSetting : MonoBehaviour
    {

        private SaveEditorModel _saveData;
        public void Save()
        {
            Debug.LogError("Save");
            var allGameObjList = FindObjectsOfType<GameObject>().ToList();
            
            
            List<BiomEditorModel> biomModels = new List<BiomEditorModel>();
            List<GameObject> bioms = allGameObjList.Where(element => element.layer == 3).ToList();
            foreach (var biom in bioms)
            {
                var biomPosition = biom.transform.position;
                biomModels.Add(new BiomEditorModel
                {
                    name = biom.tag,
                    positionX = biomPosition.x,
                    positionY = biomPosition.y,
                    positionZ = biomPosition.z,
                });
            }
            
            List<EnemyEditorModel> enemyModels = new List<EnemyEditorModel>();
            List<GameObject> enemyes = allGameObjList.Where(element => element.layer == 6).ToList();
            foreach (var enemy in enemyes)
            {
                var enemyPosition = enemy.transform.position;
                enemyModels.Add(new EnemyEditorModel
                {
                    name = enemy.tag,
                    positionX = enemyPosition.x,
                    positionY = enemyPosition.y,
                    positionZ = enemyPosition.z,
                });
            }

            _saveData = new SaveEditorModel
            {
                bioms = biomModels,
                enemyes = enemyModels
            };
            SaveIntoJson();
        }

        public void SaveIntoJson()
        {
            string potion = JsonUtility.ToJson(_saveData);
            System.IO.File.WriteAllText(Application.dataPath + "/SaveWorldData.json", potion);
        }

        [Serializable]
        public class BiomEditorModel
        {
            public string name;
            public float positionX;
            public float positionY;
            public float positionZ;
        }
        
        [Serializable]
        public class EnemyEditorModel
        {
            public string name;
            public float positionX;
            public float positionY;
            public float positionZ;
        }
        
        [Serializable]
        public class SaveEditorModel
        {
            public List<BiomEditorModel> bioms;
            public List<EnemyEditorModel> enemyes;
        }
    }
}