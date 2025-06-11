using System.IO.Compression;
using TMPro;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

/// <summary>
/// Represents the bedroom puzzle
/// </summary>
public class PuzzleBedroom : Puzzle
{
    [Header("Tutorial MiniGame")]
    [SerializeField] private CinemachineCamera miniGameCamera;
    [SerializeField] private CinemachineBlendDefinition cameraBlend;
    [SerializeField] private DialogGraph endGraph;
    [SerializeField] private char[] sequence;
    [SerializeField] private char[] possibilities = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
    [SerializeField] private GameObject[] selectionObjs;
    [SerializeField] private TextMeshPro[] texts;
    [SerializeField] private float actionCooldown = 0.1f;
    [Header("Audio Events")]
    [SerializeField] private UnityEvent onMoveSelection;
    [SerializeField] private UnityEvent onScroll;
    [SerializeField] private UnityEvent onWin;
    [SerializeField] private UnityEvent onWrong;
    [SerializeField] private UnityEvent onQuitPuzzle;
    private float lastAction;
    private int moveDirection = 0;
    private int scrollDirection = 0;

    private int[] selections;
    private int currentObjIdx;

    /// <summary>
    /// Refreshs the visual section for the puzzle
    /// </summary>
    private void RefreshVisualSelection()
    {
        for (int i = 0; i < selectionObjs.Length; i++)
        {
            selectionObjs[i].SetActive(i == currentObjIdx);
        }

        for (int i = 0; i < texts.Length; i++)
        {
            texts[i].text = possibilities[selections[i]].ToString();
        }
    }

    public override void OnFowardInput(InputType type, InputValue inputValue)
    {
        if (type == InputType.ACCEPT && inputValue.isPressed)
        {
            for (int i = 0; i < selections.Length; i++)
            {
                if (sequence[i] != possibilities[selections[i]]) {
                    onWrong.Invoke();
                    return;
                }
            }

            onWin.Invoke();
            EndPuzzle(false);
        }
        else if (type == InputType.MOVEMENT)
        {
            Vector2 vector = inputValue.Get<Vector2>();

            float value = vector.x;
            moveDirection = value < -0.75f ? -1 : (value > 0.75f ? 1 : 0);
            if(value != 0) onMoveSelection.Invoke();

            value = inputValue.Get<Vector2>().y;
            scrollDirection = value < -0.75f ? -1 : (value > 0.75f ? 1 : 0);
            if(value != 0) onScroll.Invoke();
            
            if (vector == Vector2.zero) lastAction = 0;

        }
        else if (type == InputType.PREVIOUS && inputValue.isPressed)
        {
            onQuitPuzzle.Invoke();
            EndPuzzle(true);
        }
    }

    public override void OnEnd(bool cancelled)
    {
        Player.instance.SetPlayerModelActive(true);
        miniGameCamera.Priority = 0;
        currentObjIdx = -1;
        RefreshVisualSelection();
        if (!cancelled) CutsceneManager.instance.ProcessCutscene(endGraph);
    }

    public override void OnStart()
    {
        Player.instance.SetPlayerModelActive(false);
        CinemachineBrain.GetActiveBrain(0).DefaultBlend = cameraBlend;
        miniGameCamera.Priority = 5;
        selections = new int[selectionObjs.Length];
        for (int i = 0; i < selectionObjs.Length; i++) selections[i] = 0;
        currentObjIdx = 0;
        RefreshVisualSelection();
    }

    public override void OnUpdate()
    {
        if (moveDirection != 0 && Time.time - lastAction >= actionCooldown)
        {
            lastAction = Time.time;

            currentObjIdx = (currentObjIdx + moveDirection + selectionObjs.Length) % selectionObjs.Length;
            RefreshVisualSelection();
        }

        if (scrollDirection != 0 && Time.time - lastAction >= actionCooldown)
        {
            lastAction = Time.time;

            selections[currentObjIdx] = ( selections[currentObjIdx] + scrollDirection + possibilities.Length) % possibilities.Length;
            RefreshVisualSelection();
        }
    }
}
