using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

/// <summary>
/// Represents the controls page in the pause menu
/// </summary>
public class ControlsPage : PausePage
{
    [Header("Controls")]
    [SerializeField] private RebindActionUI[] keyboardRebinds;
    [SerializeField] private GamepadProfile[] gamepadProfiles;
    [SerializeField] private Toggle[] togglesGamepadProfiles;

    void Start()
    {
        foreach (RebindActionUI actionUI in keyboardRebinds)
        {
            actionUI.menu = menu;
        }
    }

    protected override void OnClose()
    {

    }

    protected override void OnOpen()
    {
        togglesGamepadProfiles[Settings.instance.GetCurrentGamePadProfileIdx()].SetIsOnWithoutNotify(true);
    }


    /// <summary>
    /// Changes the current gamepad profile
    /// </summary>
    public void Event_ChangeGamepadProfile(bool value){
        if(!value) return;
        menu.InvokeOnButtonPress();
        
        for (int i = 0; i < togglesGamepadProfiles.Length; i++)
        {
            if (togglesGamepadProfiles[i].isOn)
            {
                Settings.instance.SetCurrentGamePadProfileIdx(i);
                gamepadProfiles[i].Apply();
                break;
            }
        }
    }

    /// <summary>
    /// Resets all keyboard rebinds
    /// </summary>
    public void Event_ResetAllRebinds(){
        menu.InvokeOnButtonPress();
        foreach (RebindActionUI rebindActionUI in keyboardRebinds)
        {
            rebindActionUI.ResetToDefault();
        }
        Settings.instance.SetBindings(GameManager.instance.GetInputs().SaveBindingOverridesAsJson());
    }
}
