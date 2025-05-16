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
        Gizmos.DrawWireSphere(transform.position, interactionRadius);
    }

    void Update()
    {
        if (GameGUI.instance.isPauseOpen || (CutsceneManager.instance.inCutscene && !CutsceneManager.instance.inParrallelCutscene) || Player.instance.inPuzzle){
            if(currentObject != null && ((CutsceneManager.instance.inCutscene && !CutsceneManager.instance.inParrallelCutscene) || Player.instance.inPuzzle)){
                currentObject.SetActive(false);
                currentObject = null;
            }
            return;
        }

        Collider[] colliders = Physics.OverlapSphere(transform.position, interactionRadius, interactionMask);
        if (colliders.Length >= 1)
        {
            float minDist = float.MaxValue;
            float currentDist;
            InteractableObject newObj = null;

            foreach(Collider collider in colliders){
                currentDist = Vector3.Distance(transform.position,collider.bounds.center);
                if(currentDist < minDist){
                    minDist = currentDist;
                    newObj = collider.transform.GetComponent<InteractableObject>();
                }
            }

            if (newObj == currentObject)
            {
                return;
            }

            if (currentObject) currentObject.SetActive(false);
            currentObject = newObj;
            if (currentObject) currentObject.SetActive(true);

        }
        else if (currentObject)
        {
            currentObject.SetActive(false);
            currentObject = null;
        }
    }

    /// <summary>
    /// Starts an interaction with the currently selected interractable object
    /// </summary>
    public void TryInterract()
    {
        if (currentObject != null)
        {
            if(currentObject.stopPlayerOnInterract){
                Player.instance.SetMovementVector(Vector2.zero);
                Player.instance.SetSprinting(false);
            }

            Player.instance.ResetHints();

            currentObject.SetActive(false);
            currentObject.Interract();

            currentObject = null;
        }
    }
}
