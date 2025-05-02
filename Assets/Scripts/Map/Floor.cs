using UnityEngine;

/// <summary>
/// Represents the floor
/// </summary>
public class Floor : MonoBehaviour
{   
    /// <summary>
    /// Floor type
    /// </summary>
    public enum Type{
        NONE,
        WOOD,
        GRASS
    }

    public Type type;
}
