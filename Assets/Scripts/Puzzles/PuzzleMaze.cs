using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Represents the puzzle maze
/// </summary>
public class PuzzleMaze : Puzzle
{
    [Header("Maze Puzzle")]
    [SerializeField] private Transform repawnPoint;

    public override void OnFowardInput(InputType type, InputValue inputValue)
    {
        if (type == InputType.PREVIOUS && inputValue.isPressed)
        {
            Player.instance.SetPosition(repawnPoint.position,repawnPoint.rotation);
            //EndPuzzle(true);
        }
    }

    public override void OnEnd(bool cancelled)
    {
    }

    public override void OnStart()
    {
    }

    public override void OnUpdate()
    {

    }
}
