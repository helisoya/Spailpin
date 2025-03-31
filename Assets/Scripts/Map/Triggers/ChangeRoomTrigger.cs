using Unity.Cinemachine;
using UnityEngine;

/// <summary>
/// Represents a trigger that can change the current room
/// </summary>
public class ChangeRoomTrigger : TriggerZone
{
    [SerializeField] private Room room;
    [SerializeField] private CinemachineBlendDefinition blend;

    protected override void OnEnter()
    {
        if(Player.instance.CurrentRoom != room.GetID()){
            CinemachineBrain.GetActiveBrain(0).DefaultBlend = blend;
            room.Apply();
        }
    }
}
