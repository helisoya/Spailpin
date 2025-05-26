using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

/// <summary>
/// Represents the Spailpin player
/// </summary>
public class Player : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private PlayerController controller;
    [SerializeField] private PlayerInteraction interactions;
    [SerializeField] private GameObject playerModelRoot;

    [Header("Shaking")]
    [SerializeField] private bool activateShaking = false;
    [SerializeField] private float shakingStrength = 0.1f;
    [SerializeField] private float shakingLength = 0.5f;
    private Room currentRoom = null;
    public int CurrentRoom { get { return currentRoom != null ? currentRoom.GetID() : -1; } }
    public Vector3 position { get { return controller.position; } }
    public Vector3 rotation { get { return controller.rotation; } }

    public static Player instance { get; private set; }
    private Puzzle currentPuzzle;
    public bool inPuzzle { get { return currentPuzzle != null; } }

    private string currentScheme;
    private float currentShakingLength;
    private float currentShakingStrength;
    private bool colliding;

    [Header("Events")]
    public UnityEvent<string> onDeviceChange;
    public UnityEvent<bool> onCollision;

    [Header("Very Important Stuff (DO NOT TOUCH)")]
    [SerializeField] private OurLordAndSaviourTheGreatCaribou ourGloriousSaviour;
    private bool ourGloriousSaviourHasArrived = false;


    void Awake()
    {
        instance = this;
        SetPlayerModelActive(true);
        if(activateShaking) onCollision.AddListener(OnCollision);
        if (Gamepad.current != null) Gamepad.current.SetMotorSpeeds(0.0f, 0.0f);
        currentShakingLength = 0;
    }

    void Oestroy()
    {
        if (Gamepad.current != null) Gamepad.current.SetMotorSpeeds(0.0f, 0.0f);    
    }

    void Update()
    {
        if (colliding)
        {
            if (controller.moving)
            {
                currentShakingStrength = Mathf.Clamp(currentShakingStrength + Time.deltaTime * 0.05f, 0.0f, shakingStrength);
            }
            else
            {
                currentShakingStrength = 0;
            }
            Gamepad.current.SetMotorSpeeds(currentShakingStrength, currentShakingStrength);
        }

        if (currentShakingLength > 0)
        {
            currentShakingLength -= Time.deltaTime;
            if (currentShakingLength <= 0 && Gamepad.current != null) Gamepad.current.SetMotorSpeeds(0.0f, 0.0f);
        }
    }

    /// <summary>
    /// Changes if the player model is active or not
    /// </summary>
    /// <param name="value">True if the player model is active</param>
    public void SetPlayerModelActive(bool value)
    {
        playerModelRoot.SetActive(value);
    }

    /// <summary>
    /// Refreshs the game's bindings (calls onDeviceChange to the current device)
    /// </summary>
    public void RefreshBindings()
    {
        onDeviceChange.Invoke(currentScheme);
    }

    /// <summary>
    /// Sets the current puzzle
    /// </summary>
    /// <param name="puzzle">The current puzzle</param>
    public void SetCurrentPuzzle(Puzzle puzzle)
    {
        currentPuzzle = puzzle;
    }

    /// <summary>
    /// Stops the current puzzle
    /// </summary>
    public void StopCurrentPuzzle()
    {
        if (currentPuzzle != null)
        {
            currentPuzzle.EndPuzzle(true);
        }
    }

    /// <summary>
    /// Changes the current room the player is in
    /// </summary>
    /// <param name="room">The new room</param>
    public void ChangeRoom(Room room)
    {
        if (currentRoom != null && currentRoom.GetID() == room.GetID()) return;

        if (currentRoom != null)
        {
            currentRoom.GetCamera().Priority = 0;
        }

        currentRoom = room;
        room.GetCamera().Priority = 1;

        float targetFOV = room.GetCamera().Lens.FieldOfView;

        foreach (Camera camera in Camera.main.GetUniversalAdditionalCameraData().cameraStack)
        {
            camera.fieldOfView = targetFOV;
        }

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
    public void SetPosition(Vector3 position, Quaternion rotation)
    {
        controller.SetPosition(position, rotation);
    }

    /// <summary>
    /// Sets the movement vector of the player
    /// </summary>
    /// <param name="vector">The new movement vector</param>
    public void SetMovementVector(Vector2 vector)
    {
        controller.SetMovementVector(vector);
    }

    /// <summary>
    /// OnMove callback
    /// </summary>
    /// <param name="value">The movement value</param>
    void OnMove(InputValue value)
    {
        if (CutsceneManager.instance.inCutscene && !CutsceneManager.instance.inParrallelCutscene) return;
        if (GameGUI.instance.isPauseOpen) return;

        if (inPuzzle && currentPuzzle.absorbMovements)
        {
            currentPuzzle.FowardInput(Puzzle.InputType.MOVEMENT, value);
        }
        else if(!inPuzzle || !currentPuzzle.inHintMenu)
        {
            controller.SetMovementVector(value.Get<Vector2>());
        }
    }

    /// <summary>
    /// OnHint callback
    /// </summary>
    /// <param name="value">The hint value</param>
    void OnHint(InputValue value)
    {
        if (CutsceneManager.instance.inCutscene && !CutsceneManager.instance.inParrallelCutscene) return;
        if (GameGUI.instance.isPauseOpen) return;

        if (inPuzzle && currentPuzzle.absorbHint)
        {
            currentPuzzle.FowardInput(Puzzle.InputType.HINT, value);
        }
    }


    /// <summary>
    /// OnPrevious callback
    /// </summary>
    /// <param name="value">The movement value</param>
    void OnPrevious(InputValue value)
    {
        if (CutsceneManager.instance.inCutscene && !CutsceneManager.instance.inParrallelCutscene) return;
        if (GameGUI.instance.isPauseOpen) return;

        if (inPuzzle && currentPuzzle.absorbPrevious)
        {
            currentPuzzle.FowardInput(Puzzle.InputType.PREVIOUS, value);
        }
    }

    /// <summary>
    /// OnEasterEgg callback
    /// </summary>
    /// <param name="value">The movement value</param>
    void OnEasterEgg(InputValue value)
    {
        if (CutsceneManager.instance.inCutscene && !CutsceneManager.instance.inParrallelCutscene) return;
        if (GameGUI.instance.isPauseOpen) return;

        if (!ourGloriousSaviourHasArrived)
        {
            ourGloriousSaviourHasArrived = true;
            Instantiate(ourGloriousSaviour, controller.position + Vector3.up * 30f, Quaternion.identity).PrayThatOurLordLandsHere(controller.position.y + 1.5f);
        }
    }

    /// <summary>
    /// OnSprint callback
    /// </summary>
    /// <param name="value">The sprinting value</param>
    void OnSprint(InputValue value)
    {
        if (GameGUI.instance.isPauseOpen || (CutsceneManager.instance.inCutscene && !CutsceneManager.instance.inParrallelCutscene) || inPuzzle)
        {
            controller.SetSprinting(false);
            return;
        }

        controller.SetSprinting(value.isPressed);
    }

    /// <summary>
    /// On Controls changed callback
    /// </summary>
    /// <param name="input">The player input</param>
    void OnControlsChanged(PlayerInput input)
    {
        print(input.currentControlScheme);
        if (Gamepad.current != null) Gamepad.current.SetMotorSpeeds(0.0f, 0.0f);    
        currentScheme = input.currentControlScheme;
        onDeviceChange.Invoke(currentScheme);
    }

    /// <summary>
    /// Changes if the player is sprinting or not
    /// </summary>
    /// <param name="sprinting">True if the player is sprinting</param>
    public void SetSprinting(bool sprinting)
    {
        controller.SetSprinting(sprinting);
    }

    /// <summary>
    /// OnPause callback
    /// </summary>
    /// <param name="value">The pause value (unused)</param>
    void OnPause(InputValue value)
    {
        if (inPuzzle && currentPuzzle.absorbPause) currentPuzzle.FowardInput(Puzzle.InputType.PAUSE, value);
        else if (inPuzzle && currentPuzzle.inHintMenu) return;
        else if (GameGUI.instance.isPauseOpen) GameGUI.instance.ClosePause();
        else
        {
            //controller.SetMovementVector(Vector2.zero);
            GameGUI.instance.OpenPause();
        }
    }

    /// <summary>
    /// OnInteract callback
    /// </summary>
    /// <param name="value">The interaction value (unused)</param>
    void OnInteract(InputValue value)
    {
        if (GameGUI.instance.isPauseOpen) return;
        if (CutsceneManager.instance.inCutscene) CutsceneManager.instance.UserSubmit();
        else if (inPuzzle && currentPuzzle.absorbInteract) currentPuzzle.FowardInput(Puzzle.InputType.ACCEPT, value);
        else if (inPuzzle && currentPuzzle.inHintMenu) return;
        else interactions.TryInterract();
    }


    /// <summary>
    /// Resets hints for the player
    /// </summary>
    public void ResetHints()
    {
        controller.ResetHints();
    }

    /// <summary>
    /// OnCollisionEnter Callback
    /// </summary>
    /// <param name="collision">The collision</param>
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            onCollision.Invoke(true);
        }
    }

    /// <summary>
    /// OnCollisionExit Callback
    /// </summary>
    /// <param name="collision">The collision</param>
    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            onCollision.Invoke(false);
        }
    }

    /// <summary>
    /// OnCollision Callback
    /// </summary>
    /// <param name="value">True if the collision has started, False if it has ended</param>
    private void OnCollision(bool value)
    {
        if (value && Gamepad.current != null)
        {
            colliding = true;
            currentShakingStrength = 0;
        }
        else if (!value)
        {
            colliding = false;
            currentShakingLength = shakingLength;
        }
    }
}
