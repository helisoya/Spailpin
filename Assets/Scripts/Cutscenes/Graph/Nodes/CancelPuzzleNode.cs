using System.Collections;
using UnityEngine;

/// <summary>
/// Represents a node that cancels the current puzzle
/// </summary>
[CreateNodeMenu("Event/Cancel Current Puzzle")]
public class CancelPuzzleNode : SpailpinNode
{
    [Input(connectionType = ConnectionType.Multiple)] public bool entry;
    [Output(connectionType = ConnectionType.Override)] public bool exit;

	// Use this for initialization
	protected override void Init() {
		base.Init();
	}

    public override IEnumerator Apply()
    {
        Player.instance.StopCurrentPuzzle();
        yield return 0;
    }
}
