using UnityEngine;

/// <summary>
/// Represents the general page in the pause menu
/// </summary>
public class GeneralPage : PausePage
{
    protected override void OnClose()
    {
    }

    protected override void OnOpen()
    {
    }



    /* ------------------------------------------------------- Click events ------------------------------------------------------- */

    /// <summary>
    /// Callback for loading a save
    /// </summary>
    public void Event_LoadGame(){
        GameManager.instance.GetSaveManager().LoadGame();
    }

    /// <summary>
    /// Callback for saving the game
    /// </summary>
    public void Event_SaveGame(){
        if(!Player.instance.inPuzzle) GameManager.instance.GetSaveManager().SaveGame();
    }

    /// <summary>
    /// Callback for returning to the main menu
    /// </summary>
    public void Event_ToMainMenu(){
        GameManager.instance.ChangeScene("MainMenu");
    }

    /// <summary>
    /// Callback for quitting the game
    /// </summary>
    public void Event_QuitGame(){
        Application.Quit();
    }

}
