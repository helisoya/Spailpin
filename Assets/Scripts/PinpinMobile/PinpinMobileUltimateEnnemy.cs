using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Pinpin's Arch Nemesis : ....
/// Who actually ?
/// </summary>
public class PinpinMobileUltimateEnnemy : MonoBehaviour
{
    [Header("Infos")]
    [SerializeField] private float speed = 5f;
    [SerializeField] private float distanceToDeath = 1f;
    [SerializeField] private float deathCooldown = 60f;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float screamerLength = 1.0f;
    [SerializeField] private GameObject screamer;
    [SerializeField] private string errorTitle = "Critical Cube Blanc Error";
    [SerializeField] private string errorDesc = "I DO NOT WANT WHITE CUBES !";


    [Header("Audio")]
    [SerializeField] private UnityEvent onDeath;

    private float deathStart;
    private bool dead = false;
    private Transform target;
    private bool playerDied = false;
    private bool turboDead = false;

    void Start()
    {
        target = Camera.main.transform;
    }

    public void Die()
    {
        dead = true;
        deathStart = Time.time;
        deathCooldown--;
        if (deathCooldown <= 0.1f) deathCooldown = 0.1f;
    }

    void Update()
    {
        if (turboDead) return;

        if (playerDied)
        {
            screamerLength -= Time.deltaTime;
            if (screamerLength <= 0)
            {
                turboDead = true;
                NativeWinAlert.Error(errorDesc, errorTitle);
                Application.Quit();
            }
            return;
        }

        if (dead && Time.time - deathStart >= deathCooldown)
        {
            rb.isKinematic = true;
            dead = false;
        }

        if (!dead)
        {
            transform.position = Vector3.MoveTowards(transform.position, Camera.main.transform.position, speed * Time.deltaTime);
            transform.LookAt(target);
            if (Vector3.Distance(transform.position, target.position) <= distanceToDeath)
            {
                // You are DEAD !
                playerDied = true;
                screamer.SetActive(true);
                onDeath.Invoke();
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, distanceToDeath);
    }
}
