using UnityEngine;

/// <summary>
/// Node for ending a path in the dialog graph
/// </summary>
[CreateNodeMenu("Control/End")]
public class EndNode : SpailpinNode
{
    [Input(connectionType = ConnectionType.Multiple)] public bool entry;

	// Use this for initialization
	protected override void Init() {
		base.Init();
	} 
}

