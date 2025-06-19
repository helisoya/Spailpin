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
        GRASS,
        GRAVEL,
        STONE
    }

    public Type type;
}
