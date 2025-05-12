using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Handles the player's input
/// </summary>
public class PlayerController : MonoBehaviour
{
    [Header("Data")]
    [SerializeField] private float playerWalkSpeed = 2f;
    [SerializeField] private float playerRunSpeed = 4f;
    [SerializeField] private float rotationSpeed = 360f;

    private const float GRAVITY = -9.81f;

    [Header("Hints")]
    [SerializeField] private bool canShowMovementHint = true;
    [SerializeField] private float waitTimeForShowingMovementHint = 3;
    private float currentWaitTimeForShowingMovementHint;

    [Header("Components")]
    [SerializeField] private Transform playerTransform;
    //[SerializeField] private Rigidbody playerRigidbody;
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private Transform soundListenerTransform;
    [SerializeField] private CharacterController controller;
    private bool running = false;
    private Vector2 moveVector;

    private Vector3 currentForward;
    private Vector3 currentRight;

    private bool cachedDirections = false;
    private Vector3 cachedForward;
    private Vector3 cachedRight;


    public Vector3 position { get { return playerTransform.transform.position; } }
    public Vector3 rotation { get { return playerTransform.eulerAngles; } }

    void Start()
    {
        currentWaitTimeForShowingMovementHint = waitTimeForShowingMovementHint;
    }


    void Update()
    {
        if (moveVector != Vector2.zero)
        {
            // Rotation
            Vector3 rotVector = moveVector.y * currentForward + moveVector.x * currentRight;
            rotVector.y = 0;
            rotVector.Normalize();

            Quaternion toQuat = Quaternion.LookRotation(rotVector, Vector3.up);
            playerTransform.rotation = Quaternion.RotateTowards(playerTransform.rotation, toQuat, rotationSpeed * Time.deltaTime);


            // Movement
            float actualSpeed = running ? playerRunSpeed : playerWalkSpeed;
            Vector3 moveDirection = currentForward * actualSpeed * moveVector.y + currentRight * actualSpeed * moveVector.x;

            controller.Move(moveDirection * Time.deltaTime);
        }

        controller.Move(Vector3.up * GRAVITY * Time.deltaTime);

        playerAnimator.SetBool("Moving", moveVector != Vector2.zero);
        soundListenerTransform.position = playerTransform.position;
        soundListenerTransform.rotation = Camera.main.transform.rotation;
        
        /*
        float actualSpeed = running ? playerRunSpeed : playerWalkSpeed;
        Vector3 flatVel = new Vector3(playerRigidbody.linearVelocity.x,0f,playerRigidbody.linearVelocity.z);
        if(flatVel.magnitude > actualSpeed){
            Vector3 limitedVel = flatVel.normalized * actualSpeed;
            playerRigidbody.linearVelocity = new Vector3(limitedVel.x,playerRigidbody.linearVelocity.y,limitedVel.z);
        }
        */

        if (canShowMovementHint && !GameGUI.instance.isPauseOpen && !CutsceneManager.instance.inCutscene && !Player.instance.inPuzzle)
        {

            if (moveVector != Vector2.zero)
            {
                // Shown
                currentWaitTimeForShowingMovementHint = waitTimeForShowingMovementHint;
                GameGUI.instance.SetMovementHintAlpha(0);
            }

            if (currentWaitTimeForShowingMovementHint > 0)
            {
                // Not shown
                if (moveVector != Vector2.zero) return;

                currentWaitTimeForShowingMovementHint -= Time.deltaTime;
                if (currentWaitTimeForShowingMovementHint <= 0)
                {
                    // Show
                    GameGUI.instance.SetMovementHintAlpha(1);
                }
            }
        }
    }

    /// <summary>
    /// Resets hints for the controller
    /// </summary>
    public void ResetHints()
    {
        currentWaitTimeForShowingMovementHint = waitTimeForShowingMovementHint;
        GameGUI.instance.SetMovementHintAlpha(0);
    }

    void FixedUpdate()
    {
        // Player Movements
        float actualSpeed = running ? playerRunSpeed : playerWalkSpeed;
        

        /*
        Vector3 moveDirection = currentForward * moveVector.y + currentRight * moveVector.x;
        moveDirection.y = 0;
        
        playerRigidbody.AddForce(moveDirection.normalized * actualSpeed * 10f,ForceMode.Force);
        */

        /*
        float ySpeed = playerRigidbody.linearVelocity.y;
        playerRigidbody.linearVelocity = currentForward * actualSpeed * moveVector.y + currentRight * actualSpeed * moveVector.x;
        playerRigidbody.linearVelocity = new Vector3(playerRigidbody.linearVelocity.x, ySpeed, playerRigidbody.linearVelocity.z);
        */
    }

    /// <summary>
    /// Change the direction vectors
    /// </summary>
    /// <param name="forward">The new forward vector</param>
    /// <param name="right">The new right vector</param>
    /// <param name="force">Should the change be forced ?</param>
    public void ChangeDirectionVectors(Vector3 forward, Vector3 right, bool force)
    {
        if (force || moveVector == Vector2.zero)
        {
            cachedDirections = false;
            currentForward = forward;
            currentRight = right;
        }
        else
        {
            cachedDirections = true;
            cachedForward = forward;
            cachedRight = right;
        }
    }


    /// <summary>
    /// Force change the player's position and rotation
    /// </summary>
    /// <param name="position">The new rotation</param>
    /// <param name="rotation">The new rotation</param>
    public void SetPosition(Vector3 position, Quaternion rotation)
    {
        playerTransform.position = position;
        playerTransform.rotation = rotation;
    }


    /// <summary>
    /// Sets the controller's move vector
    /// </summary>
    /// <param name="moveVector">The new move vector</param>
    public void SetMovementVector(Vector2 moveVector)
    {
        this.moveVector = moveVector;

        currentWaitTimeForShowingMovementHint = waitTimeForShowingMovementHint;

        if (moveVector == Vector2.zero && cachedDirections)
        {
            cachedDirections = false;
            currentForward = cachedForward;
            currentRight = cachedRight;
        }
    }

    /// <summary>
    /// Sets if the player is running or not
    /// </summary>
    /// <param name="running">True if the player is running</param>
    public void SetSprinting(bool running)
    {
        this.running = running;
    }
}
