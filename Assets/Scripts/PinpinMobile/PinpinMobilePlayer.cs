using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.InputSystem;

/// <summary>
/// Represents something. I guess they can move too ?
/// </summary>
public class PinpinMobilePlayer : MonoBehaviour
{
    [Header("Player Infos")]
    [SerializeField] private float playerWalkSpeed = 2f;
    [SerializeField] private float playerRunSpeed = 4f;
    [SerializeField] private float lookSensitivityMouse = 40f;
    [SerializeField] private float lookSensitivityGamepad = 70f;
    [SerializeField] private float cooldownFire = 0.25f;
    [SerializeField] private float shootForce = 100f;

    [Header("Player Components")]
    [SerializeField] private Transform firstPersonCamTransform;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Rigidbody playerRigidbody;
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private ParticleSystem fireParticles;
    [SerializeField] private Animator gunAnimator;
    [SerializeField] private Collider playerCollider;

    [Header("Car Infos")]
    [SerializeField] private float drivespeed;
    [SerializeField] private float steerspeed;
    [SerializeField] private float interactionDistance = 5f;

    [Header("Car Components")]
    [SerializeField] private Rigidbody carRigidbody;
    [SerializeField] private Transform playerHolder;
    [SerializeField] private Transform carCenter;
    [SerializeField] private WheelCollider wheelFL;
    [SerializeField] private WheelCollider wheelFR;
    [SerializeField] private WheelCollider wheelBL;
    [SerializeField] private WheelCollider wheelBR;

    [Header("Audio Event")]
    [SerializeField] private UnityEvent onFire;
    [SerializeField] private UnityEvent oneEnterCar;
    [SerializeField] private UnityEvent onExitCar;
    [SerializeField] private UnityEvent<bool> onMove;
    [SerializeField] private UnityEvent<bool> onCarMove;

    private bool moving = false;

    private bool usingCar = false;
    private bool running = false;
    private Vector2 moveVector;
    private Vector2 lookVector;
    private float xRotation;
    private float yRotation;
    private float lastFire;


    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if (moveVector != Vector2.zero && !moving)
        {
            moving = true;
            if (usingCar) onCarMove.Invoke(true);
            else onMove.Invoke(true);
        }
        else if (moveVector == Vector2.zero && moving)
        {
            moving = false;
            if (usingCar) onCarMove.Invoke(false);
            else onMove.Invoke(false);
        }

        if (!usingCar)
        {
            // Player Update
            float sensitivity = playerInput.currentControlScheme.Equals("Gamepad") ? lookSensitivityGamepad : lookSensitivityMouse;
            yRotation += lookVector.x * sensitivity * Time.deltaTime;
            if (yRotation > 360.0f) yRotation -= 360.0f;
            else if (yRotation < -360.0f) yRotation += 360.0f;

            xRotation -= lookVector.y * sensitivity * Time.deltaTime;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        }
        else
        {
            // Car Update
        }
    }

    void FixedUpdate()
    {
        if (usingCar)
        {
            // Car Movements
            float motor = moveVector.y * drivespeed;
            wheelFL.motorTorque = motor;
            wheelFR.motorTorque = motor;
            wheelBL.motorTorque = motor;
            wheelBR.motorTorque = motor;
            wheelFL.steerAngle = steerspeed * moveVector.x;
            wheelFR.steerAngle = steerspeed * moveVector.x;
        }
        else
        {
            // Player Movements
            float actualSpeed = running ? playerRunSpeed : playerWalkSpeed;
            playerRigidbody.linearVelocity = playerTransform.forward * actualSpeed * moveVector.y + playerTransform.right * actualSpeed * moveVector.x;


            firstPersonCamTransform.localRotation = Quaternion.Euler(xRotation,0,0);
            playerTransform.rotation = Quaternion.Euler(0,yRotation,0);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(carCenter.position, interactionDistance);
    }

    /// <summary>
    /// OnMove callback (Right, they can move too)
    /// </summary>
    /// <param name="value">The movement value</param>
    void OnMove(InputValue value)
    {
        moveVector = value.Get<Vector2>();
    }


    /// <summary>
    /// OnInteract callback (SHOOT)
    /// </summary>
    /// <param name="value">The interaction value (unused, SHOOT INSTEAD)</param>
    void OnInteract(InputValue value)
    {
        if (usingCar)
        {
            onExitCar.Invoke();
            usingCar = false;
            playerCollider.enabled = true;
            playerRigidbody.isKinematic = false;
            playerTransform.parent = null;
            playerTransform.position += playerTransform.right * 3f;
            onCarMove.Invoke(false);
            moving = false;

            wheelFL.motorTorque = 0;
            wheelFR.motorTorque = 0;
            wheelBL.motorTorque = 0;
            wheelBR.motorTorque = 0;
            wheelFL.steerAngle = 0;
            wheelFR.steerAngle = 0;
        }
        else
        {
            if (Vector3.Distance(playerRigidbody.transform.position, carCenter.position) <= interactionDistance)
            {
                oneEnterCar.Invoke();
                usingCar = true;
                playerCollider.enabled = false;
                playerRigidbody.isKinematic = true;
                playerTransform.position = playerHolder.position;
                playerTransform.rotation = playerHolder.rotation;
                playerTransform.parent = playerHolder;
                onMove.Invoke(false);
                moving = false;
            }
            else if (Time.time - lastFire > cooldownFire)
            {
                lastFire = Time.time;
                fireParticles.Play();
                onFire.Invoke();
                gunAnimator.SetTrigger("Shoot");

                if (Physics.Raycast(firstPersonCamTransform.position, firstPersonCamTransform.forward, out RaycastHit hit, 20f))
                {
                    if (hit.transform.TryGetComponent<PinpinMobileShootableThing>(out PinpinMobileShootableThing thing))
                    {
                        thing.ApplyForce(firstPersonCamTransform.forward * shootForce);
                    }
                }
            }
        }

    }

    /// <summary>
    /// OnLook callback (Useless to be honest)
    /// </summary>
    /// <param name="value">The look value (Don't look, SHOOT)</param>
    void OnLook(InputValue value){
        lookVector = value.Get<Vector2>();
    }

    /// <summary>
    /// OnSprint callback (Makes you shoot faster by running harder)
    /// </summary>
    /// <param name="value">The sprinting value</param>
    void OnSprint(InputValue value)
    {
        running = value.isPressed;
    }
}
