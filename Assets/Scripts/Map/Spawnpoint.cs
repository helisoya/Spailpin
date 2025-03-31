using UnityEngine;

/// <summary>
/// Represents a spawnpoint
/// </summary>
public class Spawnpoint : MonoBehaviour
{
    [SerializeField] private string linkedMap;
    [SerializeField] private Room linkedRoom;

    /// <summary>
    /// Gets the linked map for the waypoint
    /// </summary>
    /// <returns>The linked map</returns>
    public string GetLinkedMap(){return linkedMap;}

    /// <summary>
    /// Gets the linke room for the waypoint
    /// </summary>
    /// <returns>The linked room</returns>
    public Room GetRoom(){return linkedRoom;}
}
