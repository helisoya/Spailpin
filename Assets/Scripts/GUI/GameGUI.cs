using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    private Coroutine routineDialog;
    private bool skipDialog = false;


    [Header("Fading")]
    [SerializeField] private Fade fade;
    public bool fading {get{return fade.fading;}}

    
    public bool showingDialog {get{return routineDialog != null;}}
    public bool isPauseOpen {get{return root.activeInHierarchy;}}
    public static GameGUI instance;


    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        fade.ForceAlphaTo(1);
        fade.FadeTo(0);
    }


    /// <summary>
    /// Fades the screen
    /// </summary>
    /// <param name="alpha">The alpha target</param>
    /// <param name="speed">The fading speed</param>
    public void FadeTo(float alpha,float speed = 2f){
        fade.FadeTo(alpha,speed);
    }

    /// <summary>
    /// Sets the skip dialog tag to true
    /// </summary>
    public void SetSkipDialogTag(){
        skipDialog = true;
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
        if(routineDialog != null) StopCoroutine(routineDialog);
        routineDialog = StartCoroutine(Routine_Dialog(dialogID));
    }

    /// <summary>
    /// Routine for showing a dialog
    /// </summary>
    /// <param name="dialogID">The dialog's ID</param>
    /// <returns>IEnumerator</returns>
    private IEnumerator Routine_Dialog(string dialogID){

        int charactersPerFrame = 1;
        float speed = 5f;
        skipDialog = false;


        SetDialogOpen(true);
        dialogText.SetNewKey(dialogID);
        TMP_Text txt = dialogText.GetText();
        
		int runsThisFrame = 0;

		txt.ForceMeshUpdate(false);
		TMP_TextInfo inf = txt.textInfo;
		int vis = 0;
		int max = inf.characterCount;
		int cpf = charactersPerFrame;

		List<char> punctuation = new List<char>(new char[] { '.', ',', ';', '!', '?' });

        while (vis < max)
        {
            //allow skipping by increasing the characters per frame and the speed of occurance.
            if (skipDialog)
            {
                speed = 1;
                charactersPerFrame = charactersPerFrame < 5 ? 5 : charactersPerFrame + 3;
            }

            //reveal a certain number of characters per frame.
            while (runsThisFrame < charactersPerFrame)
            {
                vis++;
                txt.maxVisibleCharacters = vis;
                runsThisFrame++;
            }

            if (!skipDialog)
            {
                speed = punctuation.Contains(inf.characterInfo[vis - 1].character) ? 25 : 5;
            }

            //wait for the next available revelation time.
            runsThisFrame = 0;
            yield return new WaitForSeconds(0.01f * speed);
        }

        skipDialog = false;
        routineDialog = null;
    }








    /* ------------------------------------------------------- Click events ------------------------------------------------------- */

    /// <summary>
    /// Callback for setting the submit tag in a cutscene
    /// </summary>
    public void Event_CutsceneSubmit(){
        CutsceneManager.instance.UserSubmit();
    }

    /// <summary>
    /// Callback for loading a save
    /// </summary>
    public void Event_LoadGame(){
        GameManager.instance.LoadGame();
    }

    /// <summary>
    /// Callback for saving the game
    /// </summary>
    public void Event_SaveGame(){
        GameManager.instance.SaveGame();
    }

}
