using System.Collections;
using UnityEngine;

/// <summary>
/// Node to set the player's movement vector
/// </summary>
[CreateNodeMenu("Event/Player's movement vector")]
public class SetPlayerMovementVectorNode : SpailpinNode
{
    [Input(connectionType = ConnectionType.Multiple)] public bool entry;
    [SerializeField] private Vector2 moveVector;
    [Output(connectionType = ConnectionType.Override)] public bool exit;

	// Use this for initialization
	protected override void Init() {
		base.Init();
	}

    public override IEnumerator Apply()
    {
        Player.instance.SetMovementVector(moveVector);
        yield return 0;
    }
}
