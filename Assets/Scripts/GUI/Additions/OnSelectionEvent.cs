using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

/// <summary>
/// Handles events on selection
/// </summary>
public class OnSelectionEvent : MonoBehaviour, ISelectHandler
{
    [Header("Events")]
    [SerializeField] private UnityEvent onSelection;


    public void OnSelect(BaseEventData eventData)
    {
        onSelection.Invoke();
    }
}
