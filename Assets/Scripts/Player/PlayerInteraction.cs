using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// Handles the player interactions
/// </summary>
public class PlayerInteraction : MonoBehaviour
{
    [Header("Interactions")]
    [SerializeField] private LayerMask interactionMask;
    [SerializeField] private float interactionRadius = 2f;
    private InteractableObject currentObject = null;


    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position,interactionRadius);
    }

    void Update()
    {
        if(GameGUI.instance.isPauseOpen || CutsceneManager.instance.inCutscene) return;
        
        Collider[] colliders = Physics.OverlapSphere(transform.position,interactionRadius,interactionMask);
        if(colliders.Length >= 1){
            InteractableObject newObj = colliders[0].transform.GetComponent<InteractableObject>();
            if(newObj == currentObject){
                return;
            }

            if(currentObject) currentObject.SetActive(false);
            currentObject = newObj;
            if(currentObject) currentObject.SetActive(true);

        }else if(currentObject){
            currentObject.SetActive(false);
            currentObject = null;
        }
    }

    /// <summary>
    /// Starts an interaction with the currently selected interractable object
    /// </summary>
    public void TryInterract(){
        if(currentObject != null){
            Player.instance.SetMovementVector(Vector2.zero);

            currentObject.SetActive(false);
            currentObject.Interract();
            
            currentObject = null;
        }
    }
}
