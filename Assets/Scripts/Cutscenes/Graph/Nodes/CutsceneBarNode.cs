using System.Collections;
using UnityEngine;

/// <summary>
/// Represents a node that can show / hide the cutscene's bar
/// </summary>
[CreateNodeMenu("Event/Cutscene bar")]
public class CutsceneBarNode : SpailpinNode
{
    [Input(connectionType = ConnectionType.Override)] public bool entry;
    [SerializeField] private bool cutsceneBarActive;
    [Output(connectionType = ConnectionType.Override)] public bool exit;

	// Use this for initialization
	protected override void Init() {
		base.Init();
	}

    public override IEnumerator Apply()
    {
        GameGUI.instance.SetCutsceneBarActive(cutsceneBarActive);
        yield return 0;
    }
}
