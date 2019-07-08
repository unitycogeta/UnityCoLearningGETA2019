using UnityEditor;
using UnityEngine;
using UnityEngine.UI; 
using System.Collections;

[CustomEditor(typeof(SceneLoader))]
public class SceneGUI : Editor
{
    [SerializeField]
    private string SceneName;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if ((target as SceneLoader).sceneToLoad != null)
        {
            SceneName =  GUILayout.TextField((target as SceneLoader).sceneToLoad.name, GUILayout.Width(0), GUILayout.Height(0));
            (target as SceneLoader).sceneString = SceneName;
        }
    }
}
