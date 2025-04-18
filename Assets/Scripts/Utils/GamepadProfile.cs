using System;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Gamepad input profile
/// </summary>
[CreateAssetMenu(fileName = "GamepadProfile", menuName = "Spailpin/GamepadProfile")]
public class GamepadProfile : ScriptableObject
{
    public Part[] parts;

    [System.Serializable]
    public class Part{
        public InputActionReference action;
        public int index;
        public string group;
        public string path;
    }    


    /// <summary>
    /// Applies the profile
    /// </summary>
    public void Apply(){
        foreach(Part part in parts){
            part.action.action.ApplyBindingOverride(part.index,part.path);
        }
        Settings.instance.SetBindings(GameManager.instance.GetInputs().SaveBindingOverridesAsJson());
    }
}
