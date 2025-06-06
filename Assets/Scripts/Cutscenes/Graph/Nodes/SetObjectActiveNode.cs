using System.Collections;
using UnityEngine;

/// <summary>
/// Represents a node that can manipulate the active status of an object
/// </summary>
[CreateNodeMenu("Event/SetObjectActive")]
public class SetObjectActiveNode : SpailpinNode
{
    [Input(connectionType = ConnectionType.Multiple)] public bool entry;
    [SerializeField] private string objectID;
    [SerializeField] private bool objectActive;
    [Output(connectionType = ConnectionType.Override)] public bool exit;

	// Use this for initialization
	protected override void Init() {
		base.Init();
	}

    public override IEnumerator Apply()
    {
        CutsceneManager.instance.SetObjectActive(objectID, objectActive);
        yield return 0;
    }
}
