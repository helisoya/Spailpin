using UnityEngine;
using UnityEditor;
using System.Collections;
using UnityEditor.SceneManagement;

public class SpailpinMapWindowEditor : EditorWindow
{
    [SerializeField] private string globalMap = "Assets/Scenes/Tests/GlobalMap.unity";
    [SerializeField] private SpailpinMapCollection mapCollection;
    private SerializedObject serializedObject;

    [MenuItem("Spailpin/Map Viewer")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(SpailpinMapWindowEditor));
    }

    private void OnEnable()
    {
        serializedObject = new SerializedObject(this);
    }

    void OnGUI()
    {
        globalMap = EditorGUILayout.TextField("Global map", globalMap);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("mapCollection"));
        serializedObject.ApplyModifiedProperties();

        if (GUILayout.Button("Load"))
        {
            if (mapCollection == null)
            {
                Debug.LogError("No map collection selected");
            }
            else
            {
                EditorSceneManager.OpenScene(globalMap, OpenSceneMode.Single);

                foreach (string map in mapCollection.maps)
                {
                    EditorSceneManager.OpenScene(map, OpenSceneMode.Additive);
                }
            }
        }

        if (GUILayout.Button("Cleanup"))
        {
            EditorSceneManager.OpenScene(globalMap);
        }
    }
}
