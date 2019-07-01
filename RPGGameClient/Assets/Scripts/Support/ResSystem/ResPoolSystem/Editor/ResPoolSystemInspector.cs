using System.Collections.Generic;
using UnityEditor;

namespace RPGGame.ResSystem.Editor
{
    [CustomEditor(typeof(ResSystem.ResPoolSystem))]
    public class ResPoolSystemInspector : UnityEditor.Editor
    {
        private ResSystem.ResPoolSystem Target;

        private List<Res> poolList;
        void OnEnable()
        {
            Target = target as ResSystem.ResPoolSystem;
            poolList = Target.GetPoolList();
        }

        public override void OnInspectorGUI()
        {
            EditorGUILayout.BeginVertical();

            for (int i = 0; i < poolList.Count; i++)
            {
                var res = poolList[i];

                EditorGUILayout.BeginVertical("box");
                EditorGUILayout.LabelField("assetName: " + res.assetName);
                EditorGUILayout.LabelField("bundleName: " + res.bundleName);
                EditorGUILayout.LabelField("refCount: " + res.refCount.ToString());
                EditorGUILayout.EndVertical();

                EditorGUILayout.Space();
            }

            EditorGUILayout.EndVertical();
        }
    }
}