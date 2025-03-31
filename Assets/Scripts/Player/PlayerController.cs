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

    [Header("Components")]
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Rigidbody playerRigidbody;
    private bool running = false;
    private Vector2 moveVector;
    
    private Vector3 currentForward;
    private Vector3 currentRight;

    private bool cachedDirections = false;
    private Vector3 cachedForward;
    private Vector3 cachedRight;


    void Update()
    {  
        if(moveVector != Vector2.zero){
            Vector3 rotVector = moveVector.y * currentForward + moveVector.x * currentRight;
            rotVector.y = 0;
            rotVector.Normalize();

            Quaternion toQuat = Quaternion.LookRotation(rotVector,Vector3.up);
            playerTransform.rotation = Quaternion.RotateTowards(playerTransform.rotation,toQuat,rotationSpeed * Time.deltaTime);
        }
    }

    void FixedUpdate()
    {
        // Player Movements
        float actualSpeed = running ? playerRunSpeed : playerWalkSpeed;
        playerRigidbody.linearVelocity = currentForward * actualSpeed * moveVector.y + currentRight * actualSpeed * moveVector.x;
    }

    /// <summary>
    /// Change the direction vectors
    /// </summary>
    /// <param name="forward">The new forward vector</param>
    /// <param name="right">The new right vector</param>
    /// <param name="force">Should the change be forced ?</param>
    public void ChangeDirectionVectors(Vector3 forward,Vector3 right, bool force){
        if(force || moveVector == Vector2.zero){
            cachedDirections = false;
            currentForward = forward;
            currentRight = right;
        }else{
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
    public void SetPosition(Vector3 position, Quaternion rotation){
        playerRigidbody.position = position;
        playerRigidbody.rotation = rotation;
    }


    /// <summary>
    /// Sets the controller's move vector
    /// </summary>
    /// <param name="moveVector">The new move vector</param>
    public void SetMovementVector(Vector2 moveVector){
        this.moveVector = moveVector;

        if(moveVector == Vector2.zero && cachedDirections){
            cachedDirections = false;
            currentForward = cachedForward;
            currentRight = cachedRight;
        }
    }

    /// <summary>
    /// Sets if the player is running or not
    /// </summary>
    /// <param name="running">True if the player is running</param>
    public void SetSprinting(bool running){
        this.running = running;
    }
}
