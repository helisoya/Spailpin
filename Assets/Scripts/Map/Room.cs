using Unity.Cinemachine;
using UnityEngine;

/// <summary>
/// Represents a room in spailpin
/// A room has a certain camera angle and a linked trigger collision
/// </summary>
public class Room : MonoBehaviour
{
    [Header("Room")]
    [SerializeField] private int ID;
    [SerializeField] private CinemachineCamera cinemachineCamera;

    /// <summary>
    /// Gets the room's ID
    /// </summary>
    /// <returns>The room's ID</returns>
    public int GetID(){
        return ID;
    }

    /// <summary>
    /// Gets the room's camera
    /// </summary>
    /// <returns>The room's camera</returns>
    public CinemachineCamera GetCamera(){
        return cinemachineCamera;
    }

    /// <summary>
    /// Gets the room's forward vector
    /// </summary>
    /// <returns>The room's forward vector</returns>
    public Vector3 GetRoomForward(){
        Vector3 forward = cinemachineCamera.transform.forward;
        forward.y = 0;
        forward.Normalize();
        return forward;
    }

    /// <summary>
    /// Gets the room's right vector
    /// </summary>
    /// <returns>The room's right vector</returns>
    public Vector3 GetRoomRight(){
        Vector3 right = cinemachineCamera.transform.right;
        right.y = 0;
        right.Normalize();
        return right;
    }
    

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player"){
            Apply();
        }
    }

    /// <summary>
    /// Applies the room's effects
    /// </summary>
    public void Apply(){
        Player.instance.ChangeRoom(this);
    }
}
