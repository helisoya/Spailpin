using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Represents a puzzle
/// </summary>
public abstract class Puzzle : MonoBehaviour
{
    public int ID;
    public bool absorbMovements = true;
    public bool absorbPause = true;
    public bool absorbInteract = true;
    public enum InputType {MOVEMENT,ACCEPT,CANCEL};

    private  bool active = false;

    /// <summary>
    /// Starts the puzzle
    /// </summary>
    public void StartPuzzle()
    {
        active = true;
        Player.instance.SetCurrentPuzzle(this);
        OnStart();
    }

    /// <summary>
    /// Ends the puzzle
    /// </summary>
    /// <param name="cancelled">True if the minigame was cancelled before it's end</param>
    public void EndPuzzle(bool cancelled){
        active = false;
        Player.instance.SetCurrentPuzzle(null);
        OnEnd(cancelled);
    }

    void Update()
    {
        if(active) OnUpdate();
    }

    /// <summary>
    /// Forwards an input value to the puzzle
    /// </summary>
    /// <param name="type">The input's type</param>
    /// <param name="inputValue">The input value</param>
    public abstract void FowardInput(InputType type, InputValue inputValue);

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
