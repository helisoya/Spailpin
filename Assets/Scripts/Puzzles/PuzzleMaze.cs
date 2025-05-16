using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Represents the puzzle maze
/// </summary>
public class PuzzleMaze : Puzzle
{
    [Header("Maze Puzzle")]
    [SerializeField] private GameObject puzzleCanvas;
    [SerializeField] private Transform repawnPoint;

    public override void FowardInput(InputType type, InputValue inputValue)
    {
        if (type == InputType.CANCEL && inputValue.isPressed)
        {
            Player.instance.SetPosition(repawnPoint.position,repawnPoint.rotation);
            EndPuzzle(true);
        }
    }

    public override void OnEnd(bool cancelled)
    {
        puzzleCanvas.SetActive(false);
    }

    public override void OnStart()
    {
        puzzleCanvas.SetActive(true);
    }

    public override void OnUpdate()
    {

    }
}
