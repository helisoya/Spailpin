using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

/// <summary>
/// Represents the puzzle maze
/// </summary>
public class PuzzleMaze : Puzzle
{
    [Header("Maze Puzzle")]
    [SerializeField] private Transform repawnPoint;
    [SerializeField] protected Image respawnFill;
    [SerializeField] protected float respawnFillSpeed = 0.5f;
    private bool waitingToRespawn = false;
    

    public override void OnFowardInput(InputType type, InputValue inputValue)
    {
        if (type == InputType.PREVIOUS)
        {
            waitingToRespawn = inputValue.isPressed;
            respawnFill.fillAmount = 0.0f;
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
        if (waitingToRespawn)
        {
            respawnFill.fillAmount = Mathf.Clamp(respawnFill.fillAmount + Time.deltaTime * respawnFillSpeed, 0f, 1f);
            if (respawnFill.fillAmount == 1.0f)
            {
                waitingToRespawn = false;
                respawnFill.fillAmount = 0.0f;
                Player.instance.SetPosition(repawnPoint.position, repawnPoint.rotation);
            }
            
        }
    }
}
