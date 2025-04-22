using System.Collections;
using UnityEngine;

/// <summary>
/// Variable set node for the dialog system
/// </summary>
[CreateNodeMenu("Event/Set Variable")]
public class VariableSetNode : SpailpinNode
{
    [Input(connectionType = ConnectionType.Override)] public bool entry;
    [SerializeField] private string variableID;
    [SerializeField] private SetType type;
    [SerializeField] private int value;
    [Output(connectionType = ConnectionType.Override)] public bool exit;

	// Use this for initialization
	protected override void Init() {
		base.Init();
	}

    public override IEnumerator Apply()
    {
        int correctValue = type == SetType.SET ? value : GameManager.instance.GetSaveManager().GetVariable(variableID);
        if(type == SetType.ADD) correctValue += value;
        else if(type == SetType.SUBSTRACT) correctValue -= value;

        GameManager.instance.GetSaveManager().SetVariable(variableID, correctValue);

        yield return 0;
    }

    public enum SetType{SET,ADD,SUBSTRACT};
}
