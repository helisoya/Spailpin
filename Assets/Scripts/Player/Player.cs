using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

/// <summary>
/// Represents the Spailpin player
/// </summary>
public class Player : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private PlayerController controller;
    [SerializeField] private PlayerInteraction interactions;
    private Room currentRoom = null;
    public int CurrentRoom { get {return currentRoom != null ? currentRoom.GetID() : -1;}}
    public Vector3 position {get{return controller.position;}}
    public Vector3 rotation {get{return controller.rotation;}}

    public static Player instance  { get; private set;}
    private Puzzle currentPuzzle;
    public bool inPuzzle {get{return currentPuzzle != null;}}

    [Header("Events")]
    public UnityEvent<string> onDeviceChange;



    void Awake()
    {
        instance = this;
    }

    /// <summary>
    /// Sets the current puzzle
    /// </summary>
    /// <param name="puzzle">The current puzzle</param>
    public void SetCurrentPuzzle(Puzzle puzzle){
        currentPuzzle = puzzle;
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
    /// Sets the movement vector of the player
    /// </summary>
    /// <param name="vector">The new movement vector</param>
    public void SetMovementVector(Vector2 vector){
        controller.SetMovementVector(vector);
    }

    /// <summary>
    /// OnMove callback
    /// </summary>
    /// <param name="value">The movement value</param>
    void OnMove(InputValue value){
        if(CutsceneManager.instance.inCutscene) return;
        // GameGUI.instance.isPauseOpen || 
        if(inPuzzle){
            currentPuzzle.FowardInput(Puzzle.InputType.MOVEMENT,value);
        }else{
            controller.SetMovementVector(value.Get<Vector2>());
        }
        
    }

    /// <summary>
    /// OnSprint callback
    /// </summary>
    /// <param name="value">The sprinting value</param>
    void OnSprint(InputValue value){
        if(GameGUI.instance.isPauseOpen || CutsceneManager.instance.inCutscene || inPuzzle){
            controller.SetSprinting(false);
            return;
        }

        controller.SetSprinting(value.isPressed);
    }

    /// <summary>
    /// On Controls changed callback
    /// </summary>
    /// <param name="input">The player input</param>
    void OnControlsChanged(PlayerInput input){
        print(input.currentControlScheme);
        onDeviceChange.Invoke(input.currentControlScheme);
    }

    /// <summary>
    /// Changes if the player is sprinting or not
    /// </summary>
    /// <param name="sprinting">True if the player is sprinting</param>
    public void SetSprinting(bool sprinting){
        controller.SetSprinting(sprinting);
    }

    /// <summary>
    /// OnPause callback
    /// </summary>
    /// <param name="value">The pause value (unused)</param>
    void OnPause(InputValue value){
        if(inPuzzle) currentPuzzle.FowardInput(Puzzle.InputType.CANCEL,value);
        else if(GameGUI.instance.isPauseOpen) GameGUI.instance.ClosePause();
        else{
            //controller.SetMovementVector(Vector2.zero);
            GameGUI.instance.OpenPause();
        } 
    }

    /// <summary>
    /// OnInteract callback
    /// </summary>
    /// <param name="value">The interaction value (unused)</param>
    void OnInteract(InputValue value){
        if(GameGUI.instance.isPauseOpen) return;
        if(CutsceneManager.instance.inCutscene) CutsceneManager.instance.UserSubmit();
        else if(inPuzzle) currentPuzzle.FowardInput(Puzzle.InputType.ACCEPT,value);
        else interactions.TryInterract();
    }
}
