using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Represents something Pinpin can shoot (finally !)
/// </summary>
public class PinpinMobileShootableThing : MonoBehaviour
{
    [SerializeField] private Collider[] colliders;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private UnityEvent onDamage;

    /// <summary>
    /// Sends Pinpin's ennemy flying (important)
    /// </summary>
    /// <param name="force">Pinpin's strength (Over 999999999999 I guess ?)</param>
    public void ApplyForce(Vector3 force)
    {
        onDamage.Invoke();
        rb.isKinematic = false;
        rb.AddForce(force, ForceMode.Impulse);
    }
}
