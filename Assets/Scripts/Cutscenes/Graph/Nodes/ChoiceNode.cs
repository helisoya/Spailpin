using System.Collections;
using UnityEngine;
using XNode;

/// <summary>
/// Represents a choice node
/// </summary>
[CreateNodeMenu("Control/Choice")]
public class ControlNode : SpailpinNode {

    [Input(connectionType = ConnectionType.Multiple)] public bool entry;
    [Output(connectionType = ConnectionType.Multiple, dynamicPortList = true)] public string[] choiceKeys;

    // Use this for initialization
    protected override void Init()
    {
        base.Init();
    }

    public override IEnumerator Apply()
    {
        GameGUI.instance.OpenChoiceMenu(choiceKeys);
        yield return new WaitForEndOfFrame();
        while (GameGUI.instance.selectedChoiceIndex == -1)
        {
            yield return new WaitForEndOfFrame();
        }
        yield return GameGUI.instance.selectedChoiceIndex + 1;
    }
}