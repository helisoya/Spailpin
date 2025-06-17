using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

/// <summary>
/// Represents an icon that automatically updates itself on device change
/// </summary>
public class AutomaticIcon : MonoBehaviour
{
    [SerializeField] private Image iconImg;
    [SerializeField] private InputActionReference action;
    [SerializeField] private int indexKeyboard;
    [SerializeField] private int indexGamepad;
    [SerializeField] private bool isOnMainMenu = false;

    void Start()
    {
        iconImg.preserveAspect = true;

        if (isOnMainMenu) {
            MainMenuGUI.instance.onDeviceChange.AddListener(OnDeviceChange);
            OnDeviceChange(MainMenuGUI.instance.currentScheme);
        } 
        else
        {
            Player.instance.onDeviceChange.AddListener(OnDeviceChange);
            OnDeviceChange(Player.instance.currentScheme);
        }
        
    }

    /// <summary>
    /// Callback for changing the current device
    /// </summary>
    /// <param name="newDevice">The new device</param>
    private void OnDeviceChange(string newDevice)
    {
        int correctIdx = newDevice.Equals("Gamepad") ? indexGamepad : indexKeyboard;
        string controlPath = action.action.bindings[correctIdx].overridePath;
        if (string.IsNullOrEmpty(controlPath)) controlPath = action.action.bindings[correctIdx].path;
        controlPath = controlPath.Split('/')[1];
        print(newDevice + " - " + controlPath);
        iconImg.sprite = InputIcons.instance.GetIcon(newDevice, controlPath);
    }
}
