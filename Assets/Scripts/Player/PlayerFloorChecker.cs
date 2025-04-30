using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Checks the floor for any type change
/// </summary>
public class PlayerFloorChecker : MonoBehaviour
{
    private Floor.Type currentType;
    [SerializeField] private Transform checkTransform;
    [SerializeField] private float checkDistance;
    [SerializeField] private UnityEvent<Floor.Type> onFloorChangeEvent;

    void OnDrawGizmosSelected()
    {
        if(checkTransform != null){
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(checkTransform.position,checkTransform.position + Vector3.down * checkDistance);  
        }
    }

    void Update()
    {
        RaycastHit hit;
        if(Physics.Raycast(checkTransform.position,Vector3.down,out hit,checkDistance)){
            Floor floor = hit.transform.GetComponent<Floor>();
            if(floor != null && floor.type != currentType){
                currentType = floor.type;
                onFloorChangeEvent.Invoke(currentType);
            }
        }
    }

}
