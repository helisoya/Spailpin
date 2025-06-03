using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Represents an interactable object in Spailpin
/// </summary>
public class InteractableObject : MonoBehaviour
{
    [Header("Interaction")]
    public bool stopPlayerOnInterract = true;
    [SerializeField] protected Transform interactionObject;
    [SerializeField] protected DialogGraph linkedGraph;
    [SerializeField] protected UnityEvent onInterract;
    private bool playerIsInside = false;

    /// <summary>
    /// Changes if the interaction is "active" or not
    /// </summary>
    /// <param name="value">True if active</param>
    public void SetActive(bool value)
    {
        playerIsInside = value;
        if (value) GameGUI.instance.ShowInteractionIcon(interactionObject.position);
        else GameGUI.instance.HideInteractionIcon();
    }

    /// <summary>
    /// Interacts with the object
    /// </summary>
    public void Interract()
    {
        print("Interaction with : " + this.name);
        onInterract.Invoke();
        OnInterract();
    }

    /// <summary>
    /// Callback on interraction
    /// </summary>
    protected virtual void OnInterract()
    {
        // Do thing with the graph
        CutsceneManager.instance.ProcessCutscene(linkedGraph);
    }


    void Update()
    {
        if(playerIsInside) GameGUI.instance.ShowInteractionIcon(interactionObject.position);
        OnUpdate();
    }

    /// <summary>
    /// Called on Update
    /// </summary>
    public virtual void OnUpdate()
    {

    }

}
