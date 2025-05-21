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
    [SerializeField] private float accelerationSpeed = 5f;
    [SerializeField] private float maxSlopeAngle = 45f;
    [SerializeField] private float dragNormal = 0.9f;
    [SerializeField] private float dragSlope = 2.5f;

    [Header("Hints")]
    [SerializeField] private bool canShowMovementHint = true;
    [SerializeField] private float waitTimeForShowingMovementHint = 3;
    private float currentWaitTimeForShowingMovementHint;

    [Header("Components")]
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private Transform soundListenerTransform;
    [SerializeField] private Rigidbody controller;
    [SerializeField] private Transform slopeRaycast;
    private bool running = false;
    private Vector2 moveVector;

    private Vector3 currentForward;
    private Vector3 currentRight;

    private bool cachedDirections = false;
    private Vector3 cachedForward;
    private Vector3 cachedRight;

    private RaycastHit slopeHit;


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
            //float maxSpeed = running ? playerRunSpeed : playerWalkSpeed;
            //controller.AddForce(currentForward * maxSpeed * moveVector.y + currentRight * maxSpeed * moveVector.x, ForceMode.Acceleration);
        }

        playerAnimator.SetBool("Moving", moveVector != Vector2.zero);
        soundListenerTransform.position = playerTransform.position;
        soundListenerTransform.rotation = Camera.main.transform.rotation;

        if (canShowMovementHint && !GameGUI.instance.isPauseOpen && (!CutsceneManager.instance.inCutscene || CutsceneManager.instance.inParrallelCutscene) && !Player.instance.inPuzzle)
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
    /// Checks if the player is on a slope or not
    /// </summary>
    /// <returns>True if on a slope</returns>
    private bool OnSlope()
    {
        if (Physics.Raycast(slopeRaycast.position, Vector3.down, out slopeHit, 0.15f))
        {
            float angle = Vector3.Angle(Vector3.up, slopeHit.normal);
            return angle < maxSlopeAngle && angle != 0;
        }
        return false;
    }

    void FixedUpdate()
    {
        float maxSpeed = running ? playerRunSpeed : playerWalkSpeed;
        Vector3 force = currentForward * maxSpeed * accelerationSpeed * moveVector.y + currentRight * maxSpeed * accelerationSpeed * moveVector.x;


        bool onSlope = OnSlope();
        controller.useGravity = !onSlope;
        if (onSlope)
        {
            force = Vector3.ProjectOnPlane(force, slopeHit.normal);
        }

        controller.AddForce(force, ForceMode.Acceleration);
        controller.linearDamping = onSlope ? dragSlope : dragNormal;
        controller.maxLinearVelocity = maxSpeed;
    }

    /// <summary>
    /// Resets hints for the controller
    /// </summary>
    public void ResetHints()
    {
        currentWaitTimeForShowingMovementHint = waitTimeForShowingMovementHint;
        GameGUI.instance.SetMovementHintAlpha(0);
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
        controller.position = position;
        controller.rotation = rotation;
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
