using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// Represents a trigger zone for the player
/// </summary>
public class TriggerZone : MonoBehaviour
{
    protected bool playerIn;

    void OnTriggerEnter(Collider other)
    {
        if(!playerIn && other.tag == "Player"){
            playerIn = true;
            OnEnter();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(playerIn && other.tag == "Player"){
            playerIn = false;
            OnExit();
        }
    }

    void Update()
    {
        if(playerIn) OnStay();
    }

    /// <summary>
    /// Triggered when the player enters the trigger
    /// </summary>
    protected virtual void OnEnter(){}

    /// <summary>
    /// Triggered when the player exits the trigger
    /// </summary>
    protected virtual void OnExit(){}

    /// <summary>
    /// Triggered when the player stays inside the trigger
    /// </summary>
    protected virtual void OnStay(){}
}
