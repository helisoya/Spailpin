using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

/// <summary>
/// Represents a node that can change the current room
/// </summary>
[CreateNodeMenu("Event/Change room")]
public class ChangeRoomNode : SpailpinNode
{
    [Input(connectionType = ConnectionType.Multiple)] public bool entry;
    [SerializeField] private int roomID;
    [SerializeField] private CinemachineBlendDefinition blend;
    [Output(connectionType = ConnectionType.Override)] public bool exit;

    // Use this for initialization
    protected override void Init()
    {
        base.Init();
    }

    public override IEnumerator Apply()
    {
        CinemachineBrain.GetActiveBrain(0).DefaultBlend = blend;
        Map.instance.GetRoom(roomID).Apply();
        yield return 0;
    }
}
