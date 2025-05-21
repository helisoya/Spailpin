using System.Collections;
using UnityEngine;

/// <summary>
/// Node for starting a puzzle in the dialog system
/// </summary>
[CreateNodeMenu("Event/Start Puzzle")]
public class StartPuzzleNode : SpailpinNode
{
    [Input(connectionType = ConnectionType.Multiple)] public bool entry;
    [SerializeField] private int ID;
    [Output(connectionType = ConnectionType.Override)] public bool exit;

	// Use this for initialization
	protected override void Init() {
		base.Init();
	}

    public override IEnumerator Apply()
    {
        Map.instance.StartPuzzle(ID);
        CutsceneManager.instance.StopProcessing();

        yield return 0;
    }
}
