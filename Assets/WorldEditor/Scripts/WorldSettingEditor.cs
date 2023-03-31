using UnityEditor;
using UnityEngine;

namespace WorldEditor.Scripts
{
        [CustomEditor(typeof(WorldSetting))]
        public class WorldSettingEditor : Editor
        {
            public override void OnInspectorGUI()
            {
                serializedObject.Update ();
                DrawDefaultInspector();
                WorldSetting myScript = (WorldSetting)target;
                if(GUILayout.Button("Save"))
                {
                    myScript.Save();
                }
                serializedObject.ApplyModifiedProperties ();
            }
        }
}