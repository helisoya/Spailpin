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
    [SerializeField] private float lookSensitivityMouse = 40f;
    [SerializeField] private float lookSensitivityGamepad = 70f;

    [Header("Components")]
    [SerializeField] private Transform firstPersonCamTransform;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Rigidbody playerRigidbody;
    [SerializeField] private PlayerInput playerInput;
    private bool running = false;
    private Vector2 moveVector;
    private Vector2 lookVector;
    private float xRotation;
    private float yRotation;


    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        float sensitivity = playerInput.currentControlScheme.Equals("Gamepad") ? lookSensitivityGamepad : lookSensitivityMouse;

        yRotation += lookVector.x * sensitivity * Time.deltaTime;
        if(yRotation > 360.0f) yRotation -= 360.0f;
        else if(yRotation < -360.0f) yRotation += 360.0f;

        xRotation -= lookVector.y * sensitivity * Time.deltaTime;
        xRotation = Mathf.Clamp(xRotation,-90f,90f);
    }

    void FixedUpdate()
    {
        // Player Movements
        float actualSpeed = running ? playerRunSpeed : playerWalkSpeed;
        playerRigidbody.linearVelocity = playerTransform.forward * actualSpeed * moveVector.y + playerTransform.right * actualSpeed * moveVector.x;


        firstPersonCamTransform.localRotation = Quaternion.Euler(xRotation,0,0);
        playerTransform.rotation = Quaternion.Euler(0,yRotation,0);
    }



    void OnMove(InputValue value){
        moveVector = value.Get<Vector2>();
    }

    void OnLook(InputValue value){
        lookVector = value.Get<Vector2>();
    }

    void OnSprint(InputValue value){
        running = value.isPressed;
    }
}
