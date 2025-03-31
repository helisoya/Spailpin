using UnityEngine;

/// <summary>
/// Represents the game's GUI
/// </summary>
public class GameGUI : MonoBehaviour
{
    [Header("General")]
    [SerializeField] private GameObject root;
    
    public bool isOpen {get{return root.activeInHierarchy;}}
    public static GameGUI instance;


    void Awake()
    {
        instance = this;
    }

    /// <summary>
    /// Opens the pause menu
    /// </summary>
    public void OpenPause(){
        Time.timeScale = 0f;
        root.SetActive(true);
    }

    /// <summary>
    /// Closes the pause menu
    /// </summary>
    public void ClosePause(){
        Time.timeScale = 1f;
        root.SetActive(false);
    }

}
