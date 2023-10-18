using UnityEditor;
using UnityEngine;

namespace Tools
{
    public class ScriptableObjectVariableEditor : EditorWindow
    {
        private ScriptableObject _scriptableObject;
        private string _variableName;
        
        [MenuItem("Tools/Scriptable Object Variable Editor")]
        public static void ShowWindow()
        {
            GetWindow(typeof(ScriptableObjectVariableEditor));
        }

        private void OnGUI()
        {
            DisplayScriptableObjectField();
            EditorGUILayout.Space();
            
            DisplayVariableCreation();
            
            Repaint();
        }

        private void DisplayScriptableObjectField()
        {
            _scriptableObject = (ScriptableObject)EditorGUILayout.ObjectField("To Edit:", _scriptableObject, typeof(ScriptableObject), false);
        }

        private void DisplayVariableCreation()
        {
            GUILayout.Label("Variable Creation");
            _variableName = EditorGUILayout.TextField("New Variable Name:", _variableName);
        }
    }
}