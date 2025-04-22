using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// If Node for the dialog system
/// </summary>
[CreateNodeMenu("Control/If")]
public class IfNode : SpailpinNode
{
    [Input(connectionType = ConnectionType.Override)] public bool entry;
    [SerializeField] private IfType type;
    [SerializeField] private List<Check> checks;
    [Output(connectionType = ConnectionType.Override)] public bool checkTrue;
    [Output(connectionType = ConnectionType.Override)] public bool checkFalse;

	// Use this for initialization
	protected override void Init() {
		base.Init();
	}

    public override IEnumerator Apply()
    {
        bool value = false;
        bool tempValue = false;
        int variableValue;
        foreach(Check check in checks){
            variableValue = GameManager.instance.GetSaveManager().GetVariable(check.variableID);
            switch(check.check){
                case CheckType.EQUALS:
                    tempValue = variableValue == check.value;
                    break;
                case CheckType.LESS:
                    tempValue = variableValue < check.value;
                    break;
                case CheckType.LESSEQUALS:
                    tempValue = variableValue <= check.value;
                    break;
                case CheckType.GREATER:
                    tempValue = variableValue > check.value;
                    break;
                case CheckType.GREATEREQUALS:
                    tempValue = variableValue >= check.value;
                    break;
            }

            if(type == IfType.AND){
                if(!tempValue){
                    value = false;
                    break;
                }else{
                    value = true;
                }
            }else{
                value = tempValue || value;
            }
        }

        yield return value ? 0 : 1;
    }







    public enum IfType {AND, OR};
    public enum CheckType {EQUALS,LESS,GREATER,LESSEQUALS,GREATEREQUALS};

    /// <summary>
    /// Represents a check in the if node
    /// </summary>
    [System.Serializable]
    public class Check{
        public string variableID;
        public CheckType check;
        public int value;
    }

}
