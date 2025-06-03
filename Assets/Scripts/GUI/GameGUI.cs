using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// Represents the game's GUI
/// </summary>
public class GameGUI : MonoBehaviour
{
    [Header("Pause")]
    [SerializeField] private PauseMenu pauseMenu;

    [Header("Interaction Icon")]
    [SerializeField] private RectTransform interactionIcon;
    [SerializeField] private RectTransform canvasRoot;
    [SerializeField] private float sizeMax = 40f;
    [SerializeField] private float sizeMin = 20f;


    [Header("Dialog")]
    [SerializeField] private GameObject dialogRoot;
    [SerializeField] private Image dialogBg;
    [SerializeField] private LocalizedText dialogText;
    [SerializeField] private Animator cutsceneBar;
    private Coroutine routineDialog;
    private bool skipDialog = false;


    [Header("Fading")]
    [SerializeField] private Fade fade;
    public bool fading { get { return fade.fading; } }

    [Header("Audio Events")]
    [SerializeField] private UnityEvent onDialogStart;
    [SerializeField] private UnityEvent onPauseOpen;
    [SerializeField] private UnityEvent onPauseClose;

    [Header("Hints")]
    [SerializeField] private CanvasGroup hintMovement;
    [SerializeField] private float hintSpeed = 5;
    private float hintMovementAlphaTarget;

    [Header("Choice")]
    [SerializeField] private GameObject choiceRoot;
    [SerializeField] private Transform choiceButtonsRoot;
    [SerializeField] private ChoiceButton choiceButtonPrefab;
    public int selectedChoiceIndex { get; private set; }


    public bool showingDialog { get { return routineDialog != null; } }
    public bool isPauseOpen { get { return pauseMenu.isOpen; } }
    public static GameGUI instance;


    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        fade.ForceAlphaTo(1);
        fade.FadeTo(0);
        SetMovementHintAlpha(0);
        SetDialogBackgroundAlpha(Settings.instance.GetTextOpacity());
        SetDialogSize(Settings.instance.GetTextSize());
        SetDialogSpacing(Settings.instance.GetTextSpacing());
    }

    void Update()
    {
        if (hintMovement.alpha != hintMovementAlphaTarget)
        {
            int side = hintMovement.alpha < hintMovementAlphaTarget ? 1 : -1;
            hintMovement.alpha = Mathf.Clamp(hintMovement.alpha + side * hintSpeed * Time.deltaTime,
            side < 0 ? hintMovementAlphaTarget : 0.0f,
            side > 0 ? hintMovementAlphaTarget : 1.0f);
        }
    }

    /// <summary>
    /// Opens the choice menu
    /// </summary>
    /// <param name="keys">The choice menu</param>
    public void OpenChoiceMenu(string[] keys)
    {
        selectedChoiceIndex = -1;

        foreach (Transform child in choiceButtonsRoot) Destroy(child.gameObject);
        for (int i = 0; i < keys.Length; i++)
        {
            ChoiceButton button = Instantiate(choiceButtonPrefab, choiceButtonsRoot);
            button.Init(i, keys[i]);
            if(i == 0) EventSystem.current.SetSelectedGameObject(button.gameObject);
        }

        choiceRoot.SetActive(true);
    }

    /// <summary>
    /// Selects a choice
    /// </summary>
    /// <param name="index">The choice's index</param>
    public void SelectChoice(int index)
    {
        selectedChoiceIndex = index;
        choiceRoot.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
    }

    /// <summary>
    /// Shows the interaction icon
    /// </summary>
    /// <param name="worldPosition">The target's world position</param>
    public void ShowInteractionIcon(Vector3 worldPosition)
    {
        interactionIcon.gameObject.SetActive(true);

        Vector2 ViewportPosition = Camera.main.WorldToViewportPoint(worldPosition);
        Vector2 WorldObjectScreenPosition = new Vector2(
        ((ViewportPosition.x * canvasRoot.sizeDelta.x) - (canvasRoot.sizeDelta.x * 0.5f)),
        ((ViewportPosition.y * canvasRoot.sizeDelta.y) - (canvasRoot.sizeDelta.y * 0.5f)));

        interactionIcon.anchoredPosition = WorldObjectScreenPosition;


        float distanceToCamera = Mathf.Clamp(Vector3.Distance(worldPosition, Camera.main.transform.position), 5f, 20f);
        float size = sizeMax - (sizeMax - sizeMin) * ((distanceToCamera - 5f) / 15f);
        interactionIcon.sizeDelta = new Vector2(size, size);
    }

    /// <summary>
    /// Hides the interaction icon
    /// </summary>
    public void HideInteractionIcon()
    {
        interactionIcon.gameObject.SetActive(false);
    }

    /// <summary>
    /// Sets the dialog background's alpha
    /// </summary>
    /// <param name="alpha">The alpha</param>
    public void SetDialogBackgroundAlpha(float alpha)
    {
        Color newColor = dialogBg.color;
        newColor.a = alpha;
        dialogBg.color = newColor;
    }

    /// <summary>
    /// Sets the dialog's font size
    /// </summary>
    /// <param name="sizeIndex">The font size index</param>
    public void SetDialogSize(int sizeIndex){
        int correctSize = 0;
        switch (sizeIndex){
            case 0:
                correctSize = 18;
                break;
            case 1:
                correctSize = 28;
                break;
            case 2:
                correctSize = 30;
                break;
        }

        dialogText.GetText().fontSize = correctSize;
    }

    /// <summary>
    /// Sets the dialog's spacing
    /// </summary>
    /// <param name="sizeIndex">The spacing index</param>
    public void SetDialogSpacing(int sizeIndex){
        int correctSize = 0;
        switch (sizeIndex){
            case 0:
                correctSize = 0;
                break;
            case 1:
                correctSize = 5;
                break;
            case 2:
                correctSize = 10;
                break;
        }

        dialogText.GetText().characterSpacing = correctSize;
    }

    /// <summary>
    /// Changes the movement's hint alpha
    /// </summary>
    /// <param name="alpha">The new alpha</param>
    public void SetMovementHintAlpha(float alpha)
    {
        hintMovementAlphaTarget = alpha;
    }


    /// <summary>
    /// Fades the screen
    /// </summary>
    /// <param name="alpha">The alpha target</param>
    /// <param name="speed">The fading speed</param>
    public void FadeTo(float alpha, float speed = 2f)
    {
        fade.FadeTo(alpha, speed);
    }

    /// <summary>
    /// Sets the skip dialog tag to true
    /// </summary>
    public void SetSkipDialogTag()
    {
        skipDialog = true;
    }

    /// <summary>
    /// Opens the pause menu
    /// </summary>
    public void OpenPause()
    {
        Time.timeScale = 0f;
        pauseMenu.Open();
        onPauseOpen.Invoke();
    }

    /// <summary>
    /// Closes the pause menu
    /// </summary>
    public void ClosePause()
    {
        Time.timeScale = 1f;
        pauseMenu.Close();
        onPauseClose.Invoke();
    }

    /// <summary>
    /// Sets if the dialog panel is active or not
    /// </summary>
    /// <param name="value">True if it is active</param>
    public void SetDialogOpen(bool value)
    {
        dialogRoot.SetActive(value);
    }

    /// <summary>
    /// Shows a dialog on screen
    /// </summary>
    /// <param name="dialogID">The dialog's ID</param>
    public void ShowDialog(string dialogID)
    {
        if (routineDialog != null) StopCoroutine(routineDialog);
        routineDialog = StartCoroutine(Routine_Dialog(dialogID));
    }

    /// <summary>
    /// Sets if the cutscene's bar are active or not
    /// </summary>
    /// <param name="active">True if the bars are visible</param>
    public void SetCutsceneBarActive(bool active)
    {
        cutsceneBar.SetBool("Show", active);
    }

    /// <summary>
    /// Routine for showing a dialog
    /// </summary>
    /// <param name="dialogID">The dialog's ID</param>
    /// <returns>IEnumerator</returns>
    private IEnumerator Routine_Dialog(string dialogID)
    {
        onDialogStart.Invoke();
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
    public void Event_CutsceneSubmit()
    {
        CutsceneManager.instance.UserSubmit();
    }
}
