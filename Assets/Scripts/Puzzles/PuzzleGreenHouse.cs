using AYellowpaper.SerializedCollections;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

/// <summary>
/// Represents the GreenHouse puzzle
/// </summary>
public class PuzzleGreenHouse : Puzzle
{
    [System.Serializable]
    public enum Placement
    {
        NONE,
        TOP,
        BOTTOM,
        LEFT,
        RIGHT
    }

    [System.Serializable]
    public struct PlacementData
    {
        public GameObject selectionObj;
        public Animator animator;
    }

    [Header("GreenHouse MiniGame")]
    [SerializeField] private CinemachineCamera miniGameCamera;
    [SerializeField] private CinemachineBlendDefinition cameraBlend;
    [SerializeField] private DialogGraph endGraph;
    [SerializeField] private Placement[] sequence;
    [SerializeField] private SerializedDictionary<Placement, PlacementData> datas;
    [Header("Audio Events")]
    [SerializeField] private UnityEvent onMoveSelection;
    [SerializeField] private UnityEvent onSelect;
    [SerializeField] private UnityEvent onWin;
    [SerializeField] private UnityEvent onWrong;
    [SerializeField] private UnityEvent onQuitPuzzle;

    private int currentSequenceIdx;
    private Placement currentPlacement;

    /// <summary>
    /// Refreshs the visual section for the puzzle
    /// </summary>
    private void RefreshVisualSelection()
    {
        foreach (Placement placement in datas.Keys)
        {
            datas[placement].selectionObj.SetActive(currentPlacement == placement);
        }
    }

    public override void OnFowardInput(InputType type, InputValue inputValue)
    {
        if (type == InputType.ACCEPT && inputValue.isPressed)
        {
            datas[currentPlacement].animator.SetTrigger("Use");
            onSelect.Invoke();
            if (sequence[currentSequenceIdx] == currentPlacement)
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
                onWrong.Invoke();
                currentSequenceIdx = 0;

                // If you get wrong but you got the first right, start from the second
                if (sequence[currentSequenceIdx] == currentPlacement) currentSequenceIdx++;
            }
        }
        else if (type == InputType.MOVEMENT)
        {
            bool move = true;
            Vector2 vec = inputValue.Get<Vector2>();
            if (vec.x < -0.75f && currentPlacement != Placement.LEFT)
            {
                currentPlacement = Placement.LEFT;
            }
            else if (vec.x >= 0.75f && currentPlacement != Placement.RIGHT)
            {
                currentPlacement = Placement.RIGHT;
            }
            else if (vec.y >= 0.75f && currentPlacement != Placement.TOP)
            {
                currentPlacement = Placement.TOP;
            }
            else if (vec.y <= -0.75f && currentPlacement != Placement.BOTTOM)
            {
                currentPlacement = Placement.BOTTOM;
            }
            else
            {
                move = false;
            }

            if (move)
            {
                RefreshVisualSelection();
                onMoveSelection.Invoke();
            }            
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
        currentPlacement = Placement.NONE;
        RefreshVisualSelection();
        if (!cancelled) CutsceneManager.instance.ProcessCutscene(endGraph);
    }

    public override void OnStart()
    {
        Player.instance.SetPlayerModelActive(false);
        CinemachineBrain.GetActiveBrain(0).DefaultBlend = cameraBlend;
        miniGameCamera.Priority = 5;

        currentPlacement = Placement.TOP;
        currentSequenceIdx = 0;
        RefreshVisualSelection();
    }

    public override void OnUpdate()
    {
    }
}
