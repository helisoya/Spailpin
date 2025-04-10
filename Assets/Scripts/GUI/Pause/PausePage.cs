using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Represents a page in the pause menu
/// </summary>
public abstract class PausePage : MonoBehaviour
{
    [Header("General")]
    [SerializeField] protected GameObject root;
    [SerializeField] private Selectable firstObject;

    /// <summary>
    /// Gets the page's first object
    /// </summary>
    /// <returns>The first object</returns>
    public Selectable GetFirstObject(){
        return firstObject;
    }
    
    /// <summary>
    /// Opens the page
    /// </summary>
    public void Open(){
        root.SetActive(true);
        OnOpen();
    }

    /// <summary>
    /// Closes the page
    /// </summary>
    public void Close(){
        root.SetActive(false);
        OnClose();
    }

    /// <summary>
    /// Callback on open
    /// </summary>
    protected abstract void OnOpen();

    /// <summary>
    /// Callback on close
    /// </summary>
    protected abstract void OnClose();
}
