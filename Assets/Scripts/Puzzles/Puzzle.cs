using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

/// <summary>
/// Represents a puzzle
/// </summary>
public abstract class Puzzle : MonoBehaviour
{
    public int ID;
    public bool absorbMovements = true;
    public bool absorbPause = true;
    public bool absorbInteract = true;
    public bool absorbHint = true;
    public bool absorbPrevious;
    public enum InputType { MOVEMENT, ACCEPT, PAUSE, PREVIOUS, HINT };

    [Header("Common")]
    [SerializeField] protected GameObject puzzleCanvas;

    [Header("Hint System")]
    [SerializeField] protected Image hintFill;
    [SerializeField] protected float hintFillSpeed = 0.5f;
    [Space]
    [SerializeField] protected GameObject hintMenuRoot;
    [SerializeField] protected GameObject hintMenuSelection;
    [SerializeField] private Image[] hintButtons;
    [Space]
    [SerializeField] protected GameObject hintMenuHint;
    [SerializeField] protected LocalizedText hintMenuHintText;
    [SerializeField] protected string hintPrefix;

    [Header("Hint Audio Events")]
    [SerializeField] private UnityEvent onOpenHint;
    [SerializeField] private UnityEvent onPreviousHint;
    [SerializeField] private UnityEvent onClickHint;

    protected bool[] hintDones;
    protected bool waitingToOpenHintMenu = false;
    protected bool lookingAtHint = false;
    protected int lastHintIndex;

    public bool inHintMenu { get; private set; }
    protected bool active = false;

    void Awake() {
        hintDones = new bool[3];
    }

    /// <summary>
    /// Starts the puzzle
    /// </summary>
    public void StartPuzzle()
    {
        active = true;
        Player.instance.SetCurrentPuzzle(this);
        puzzleCanvas.SetActive(true);
        OnStart();
    }

    /// <summary>
    /// Ends the puzzle
    /// </summary>
    /// <param name="cancelled">True if the minigame was cancelled before it's end</param>
    public void EndPuzzle(bool cancelled) {
        active = false;
        Player.instance.SetCurrentPuzzle(null);
        puzzleCanvas.SetActive(false);
        OnEnd(cancelled);
    }

    /// <summary>
    /// Opens the hint selection screen
    /// </summary>
    /// <param name="startIndex">The start index</param>
    public void OpenHintSelection(int startIndex)
    {
        hintMenuRoot.SetActive(true);
        hintMenuSelection.SetActive(true);
        hintMenuHint.SetActive(false);
        inHintMenu = true;
        lookingAtHint = false;

        for (int i = 0; i < 3; i++)
        {
            hintButtons[i].color = hintDones[i] ? Color.gray : Color.black;
        }
        EventSystem.current.SetSelectedGameObject(hintButtons[startIndex].gameObject);
    }

    /// <summary>
    /// Opens the hint visual screen
    /// </summary>
    /// <param name="hintIndex">The hint index</param>
    public void OpenHintVisual(int hintIndex)
    {
        onClickHint.Invoke();
        hintMenuRoot.SetActive(true);
        hintMenuSelection.SetActive(false);
        hintMenuHint.SetActive(true);
        lastHintIndex = hintIndex;

        inHintMenu = true;
        lookingAtHint = true;
        EventSystem.current.SetSelectedGameObject(null);
        hintMenuHintText.SetNewKey(hintPrefix+"_"+hintIndex);
        hintDones[hintIndex] = true;
    }

    /// <summary>
    /// Closes the hint menu
    /// </summary>
    public void CloseHint()
    {
        inHintMenu = false;
        lookingAtHint = false;
        EventSystem.current.SetSelectedGameObject(null);
        hintMenuRoot.SetActive(false);
    }

    void Update()
    {
        if (active)
        {
            if (waitingToOpenHintMenu)
            {
                hintFill.fillAmount = Mathf.Clamp(hintFill.fillAmount + hintFillSpeed * Time.deltaTime, 0.0f, 1.0f);
                if (hintFill.fillAmount == 1.0f)
                {
                    waitingToOpenHintMenu = false;
                    hintFill.fillAmount = 0.0f;
                    OpenHintSelection(0);
                    onOpenHint.Invoke();
                }
            }
            else if(!inHintMenu)
            {
                OnUpdate();
            }
        }
    }

    /// <summary>
    /// Forwards an input value to the puzzle
    /// </summary>
    /// <param name="type">The input's type</param>
    /// <param name="inputValue">The input value</param>
    public void FowardInput(InputType type, InputValue inputValue)
    {
        if (type == InputType.HINT && !inHintMenu)
        {
            waitingToOpenHintMenu = inputValue.isPressed;
            hintFill.fillAmount = 0.0f;
        }
        else if (type == InputType.PREVIOUS && (inHintMenu || lookingAtHint) && inputValue.isPressed)
        {
            onPreviousHint.Invoke();
            if (lookingAtHint) OpenHintSelection(lastHintIndex);
            else CloseHint();
        }
        else if (!inHintMenu)
        {
            OnFowardInput(type, inputValue);
        }
    }

    /// <summary>
    /// On Foward Input Event
    /// </summary>
    /// <param name="type">The input's type</param>
    /// <param name="inputValue">The input value</param>
    public abstract void OnFowardInput(InputType type, InputValue inputValue);

    /// <summary>
    /// On Start Event
    /// </summary>
    public abstract void OnStart();

    /// <summary>
    ///  On Update Event
    /// </summary>
    public abstract void OnUpdate();

    /// <summary>
    /// On End Event
    /// </summary>
    /// <param name="cancelled">True if the minigame was cancelled before it's end</param>
    public abstract void OnEnd(bool cancelled);
}
