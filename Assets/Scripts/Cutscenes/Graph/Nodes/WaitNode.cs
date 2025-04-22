using System.Collections;
using UnityEngine;

/// <summary>
/// Represents a node that waits for X seconds
/// </summary>
[CreateNodeMenu("Control/Wait")]
public class WaitNode : SpailpinNode
{
    [Input(connectionType = ConnectionType.Override)] public bool entry;
    [Input(connectionType = ConnectionType.Override)] public float waitTime;
    [Output(connectionType = ConnectionType.Override)] public bool exit;

	// Use this for initialization
	protected override void Init() {
		base.Init();
	}

    public override IEnumerator Apply()
    {
        yield return new WaitForSeconds(waitTime);
        yield return 0;
    }
}
