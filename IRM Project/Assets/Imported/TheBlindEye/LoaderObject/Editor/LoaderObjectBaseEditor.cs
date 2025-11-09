using UnityEditor;
using UnityEngine;

namespace LoaderObject.EditorExtensions
{
    [CustomEditor(typeof(LoaderObjectBase), true)]
    public class LoaderObjectBaseEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            
            if (GUILayout.Button(new GUIContent ("Delete Data", "Delete the Scriptable Object Data, as well as the Json(if exist).")))
                EditorEvent.Invoke();
        }
    }
}