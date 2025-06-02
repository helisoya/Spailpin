using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

/// <summary>
/// Represents the tutorial puzzle
/// </summary>
public class PuzzleTutorial : Puzzle
{
    [Header("Tutorial MiniGame")]
    [SerializeField] private CinemachineCamera miniGameCamera;
    [SerializeField] private CinemachineBlendDefinition cameraBlend;
    [SerializeField] private DialogGraph endGraph;
    [SerializeField] private int[] sequence;
    [SerializeField] private GameObject[] selectionObjs;
    [SerializeField] private Animator[] animators;
    [SerializeField] private float actionCooldown = 0.1f;
    [Header("Audio Events")]
    [SerializeField] private UnityEvent onMoveSelection;
    [SerializeField] private UnityEvent<int> onSelect;
    [SerializeField] private UnityEvent onWin;
    [SerializeField] private UnityEvent onWrongNote;
    [SerializeField] private UnityEvent onQuitPuzzle;
    private float lastAction;
    private int moveDirection = 0;

    private int currentSequenceIdx;
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
    }

    public override void OnFowardInput(InputType type, InputValue inputValue)
    {
        if (type == InputType.ACCEPT && inputValue.isPressed)
        {
            animators[currentObjIdx].SetTrigger("Use");
            onSelect.Invoke(currentObjIdx);
            if (sequence[currentSequenceIdx] == currentObjIdx)
            {
                // Good
                currentSequenceIdx++;
                if (currentSequenceIdx == sequence.Length)
                {
                    // End
                    onWin.Invoke();
                    EndPuzzle(false);
                }
            }
            else
            {
                // Wrong
                onWrongNote.Invoke();
                currentSequenceIdx = 0;
            }
        }
        else if (type == InputType.MOVEMENT)
        {
            float value = inputValue.Get<Vector2>().x;
            moveDirection = value < -0.75f ? -1 : (value > 0.75f ? 1 : 0);
            if (moveDirection == 0) lastAction = 0;
            onMoveSelection.Invoke();
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

        currentObjIdx = 0;
        currentSequenceIdx = 0;
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
    }
}
