using UnityEngine;
using XNodeEditor;

/// <summary>
/// Represents the editor for a dialog node
/// </summary>
[CustomNodeEditor(typeof(DialogNode))]
public class DialogNodeEditor : NodeEditor
{
    private DialogNode node;

    public override void OnBodyGUI() {
        base.OnBodyGUI();

        if (node == null) node = target as DialogNode;

        // Update serialized object's representation
        serializedObject.Update();
        UnityEditor.EditorGUILayout.LabelField("Local : " + Locals.GetLocal(serializedObject.FindProperty("dialogID").stringValue));

        // Apply property modifications
        serializedObject.ApplyModifiedProperties();
    }
}
