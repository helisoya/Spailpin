using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Represents the Spailpin player
/// </summary>
public class Player : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private PlayerController controller;
    private Room currentRoom = null;

    public static Player instance  { get; private set;}

    void Awake()
    {
        instance = this;
    }

    /// <summary>
    /// Changes the current room the player is in
    /// </summary>
    /// <param name="room">The new room</param>
    public void ChangeRoom(Room room){
        if(currentRoom != null && currentRoom.GetID() == room.GetID()) return;
        
        if(currentRoom != null){
            currentRoom.GetCamera().Priority = 0;
        }

        currentRoom = room;
        room.GetCamera().Priority = 1;

        // Do things with player controller
        controller.ChangeDirectionVectors(
            room.GetRoomForward(),
            room.GetRoomRight(),
            false    
        );

    }

    /// <summary>
    /// Force change the player's position and rotation
    /// </summary>
    /// <param name="position">The new rotation</param>
    /// <param name="rotation">The new rotation</param>
    public void SetPosition(Vector3 position, Quaternion rotation){
        controller.SetPosition(position,rotation);
    }

    /// <summary>
    /// OnMove callback
    /// </summary>
    /// <param name="value">The movement value</param>
    void OnMove(InputValue value){
        controller.SetMovementVector(value.Get<Vector2>());
    }

    /// <summary>
    /// OnSprint callback
    /// </summary>
    /// <param name="value">The sprinting value</param>
    void OnSprint(InputValue value){
        controller.SetSprinting(value.isPressed);
    }
}
