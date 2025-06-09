using System.Collections;
using UnityEngine;

/// <summary>
/// Represents a node that can show / hide the player
/// </summary>
[CreateNodeMenu("Event/Hide player")]
public class HidePlayerNode : SpailpinNode
{
    [Input(connectionType = ConnectionType.Multiple)] public bool entry;
    [SerializeField] private bool playerVisible;
    [Output(connectionType = ConnectionType.Override)] public bool exit;

    // Use this for initialization
    protected override void Init()
    {
        base.Init();
    }

    public override IEnumerator Apply()
    {
        Player.instance.SetPlayerModelActive(playerVisible);
        yield return 0;
    }
}
