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
    [SerializeField] private float actionCooldown = 0.1f;
    private float lastAction;
    private int moveDirection = 0;

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
            moveDirection = value < -0.75f ? -1 : (value > 0.75f ? 1 : 0);
            print(value+" -> "+moveDirection);
            if(moveDirection == 0) lastAction = 0;
        }
        else if(type == InputType.CANCEL && inputValue.isPressed){
            EndPuzzle(true);
        }
    }

    public override void OnEnd(bool cancelled)
    {
        miniGameCamera.Priority = 0;
        currentObjIdx = -1;
        RefreshVisualSelection();
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
        if(moveDirection != 0  && Time.time - lastAction >= actionCooldown){
            lastAction = Time.time;

            currentObjIdx = (currentObjIdx + moveDirection + 3) % 3;
            RefreshVisualSelection();
        }
    }
}
