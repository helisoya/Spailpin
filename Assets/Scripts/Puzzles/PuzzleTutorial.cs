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
                    EndPuzzle(false);
                }
            }else{
                // Wrong
                currentSequenceIdx = 0;
            }
        }
        else if(type == InputType.MOVEMENT)
        {
            float value = inputValue.Get<Vector2>().x;
            currentObjIdx = (currentObjIdx + (value < 0 ? -1 : (value > 0 ? 1 : 0)) + 3) % 3;
            RefreshVisualSelection();
        }
        else if(type == InputType.CANCEL && inputValue.isPressed){
            EndPuzzle(true);
        }
    }

    public override void OnEnd(bool cancelled)
    {
        miniGameCamera.Priority = 0;
        if(!cancelled) CutsceneManager.instance.ProcessCutscene(endGraph);
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
