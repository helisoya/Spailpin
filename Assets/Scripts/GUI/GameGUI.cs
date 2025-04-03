using UnityEngine;

/// <summary>
/// Represents the game's GUI
/// </summary>
public class GameGUI : MonoBehaviour
{
    [Header("Pause")]
    [SerializeField] private GameObject root;

    [Header("Dialog")]
    [SerializeField] private GameObject dialogRoot;
    [SerializeField] private LocalizedText dialogText;
    
    public bool isPauseOpen {get{return root.activeInHierarchy;}}
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

    /// <summary>
    /// Sets if the dialog panel is active or not
    /// </summary>
    /// <param name="value">True if it is active</param>
    public void SetDialogOpen(bool value){
        dialogRoot.SetActive(value);
    }

    /// <summary>
    /// Shows a dialog on screen
    /// </summary>
    /// <param name="dialogID">The dialog's ID</param>
    public void ShowDialog(string dialogID){
        SetDialogOpen(true);
        dialogText.SetNewKey(dialogID);
    }










    /* ------------------------------------------------------- Click events ------------------------------------------------------- */

    /// <summary>
    /// Callback for setting the submit tag in a cutscene
    /// </summary>
    public void Event_CutsceneSubmit(){
        CutsceneManager.instance.UserSubmit();
    }


}
