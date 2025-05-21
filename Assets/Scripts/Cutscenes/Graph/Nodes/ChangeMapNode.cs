using System.Collections;
using UnityEngine;

/// <summary>
/// Represents a node that can change the map
/// </summary>
[CreateNodeMenu("Event/Change Map")]
public class ChangeMapNode : SpailpinNode
{
    [Input(connectionType = ConnectionType.Multiple)] public bool entry;
    [SerializeField] private string mapID;

	// Use this for initialization
	protected override void Init() {
		base.Init();
	}

    public override IEnumerator Apply()
    {
        GameManager.instance.ChangeScene(mapID);
        yield return 0;
    }
}
