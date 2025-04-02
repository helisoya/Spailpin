using UnityEngine;

/// <summary>
/// Represents an interactable object in Spailpin
/// </summary>
public class InteractableObject : MonoBehaviour
{
    [Header("Interaction")]
    [SerializeField] private GameObject interactionObject;
    [SerializeField] private DialogGraph linkedGraph;
    private bool playerNear; 

    /// <summary>
    /// Changes if the interaction is "active" or not
    /// </summary>
    /// <param name="value">True if active</param>
    public void SetActive(bool value){
        playerNear = value;
        interactionObject.SetActive(value);
    }

    /// <summary>
    /// Interacts with the object
    /// </summary>
    public void Interract(){
        print("Interaction with : "+this.name);
        OnInterract();
    }

    /// <summary>
    /// Callback on interraction
    /// </summary>
    protected virtual void OnInterract(){
        // Do thing with the graph
        CutsceneManager.instance.ProcessCutscene(linkedGraph);
    }


    void Update()
    {
        if(playerNear){
            interactionObject.transform.LookAt(Camera.main.transform);
        }   
    }

}
