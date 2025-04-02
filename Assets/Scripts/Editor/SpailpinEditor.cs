using UnityEngine;
using UnityEditor;

/// <summary>
/// Represents editor tools for spailpin
/// </summary>
public class SpailpinEditor : MonoBehaviour
{
    /// <summary>
    /// Changes the locals to french
    /// </summary>
    [MenuItem("Spailpin/Locals/French")]
    static void LocalsToFra()
    {
        Locals.ChangeLanguage("fra");
    }

    /// <summary>
    /// Changes the locals to english
    /// </summary>
    [MenuItem("Spailpin/Locals/English")]
    static void LocalsToEng()
    {
        Locals.ChangeLanguage("eng");
    }
}
