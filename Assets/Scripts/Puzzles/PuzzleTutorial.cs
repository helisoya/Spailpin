using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Represents the tutorial puzzle
/// </summary>
public class PuzzleTutorial : Puzzle
{
    [Header("Tutorial MiniGame")]
    [SerializeField] private CinemachineCamera miniGameCamera;
    [SerializeField] private DialogGraph endGraph;
    [SerializeField] private int[] sequence;
    [SerializeField] private GameObject[] selectionObjs;
    private int currentSequenceIdx;
    private int currentObjIdx;

    /// <summary>
    /// Refreshs the visual section for the puzzle
    /// </summary>
    private void RefreshVisualSelection(){
        for(int i = 0; i < selectionObjs.Length;i++){
            selectionObjs[i].SetActive(i == currentObjIdx);
        }
    }

    public override void FowardInput(InputType type, InputValue inputValue)
    {
        if(type == InputType.ACCEPT && inputValue.isPressed){
            if(sequence[currentSequenceIdx] == currentObjIdx){
                // Good
                currentSequenceIdx++;
                if(currentSequenceIdx == sequence.Length){
                    // End
                    EndPuzzle();
                }
            }else{
                // Wrong
                currentSequenceIdx = 0;
            }
        }

        if(type == InputType.MOVEMENT && inputValue.isPressed){
            float value = inputValue.Get<Vector2>().x;
            currentObjIdx = (currentSequenceIdx + (value < 0 ? -1 : (value > 0 ? 1 : 0)) + 3) % 3;
            RefreshVisualSelection();
        }
    }

    public override void OnEnd()
    {
        miniGameCamera.Priority = 0;
        CutsceneManager.instance.ProcessCutscene(endGraph);
    }

    public override void OnStart()
    {
        miniGameCamera.Priority = 5;

        currentObjIdx = 0;
        currentSequenceIdx = 0;
        RefreshVisualSelection();
    }

    public override void OnUpdate()
    {

    }
}
