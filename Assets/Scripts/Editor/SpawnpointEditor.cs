using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Spawnpoint))]
public class SpawnpointEditor : Editor
{
    SerializedProperty m_isDefaultSpawnpoint;
    SerializedProperty m_linkedMap;
    SerializedProperty m_linkedRoom;

    void OnEnable()
    {
        m_isDefaultSpawnpoint = serializedObject.FindProperty("isDefaultSpawnpoint");
        m_linkedMap = serializedObject.FindProperty("linkedMap");
        m_linkedRoom = serializedObject.FindProperty("linkedRoom");
    }


    override public void OnInspectorGUI()
    {
        EditorGUILayout.PropertyField(m_isDefaultSpawnpoint, new GUIContent("Is default Spawnpoint"));

        if(!m_isDefaultSpawnpoint.boolValue){
            EditorGUILayout.PropertyField(m_linkedMap, new GUIContent("Linked Map"));
        }

        EditorGUILayout.PropertyField(m_linkedRoom, new GUIContent("Linked Room"));
        
        serializedObject.ApplyModifiedProperties();
    }
}
