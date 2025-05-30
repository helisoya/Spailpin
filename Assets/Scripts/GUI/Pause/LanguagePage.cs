using System;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Represents the language page in the pause menu
/// </summary>
public class LanguagePage : PausePage
{
    [Header("Language page")]
    [SerializeField] private Toggle[] textSizeToggles;
    [SerializeField] private Toggle[] textSpacingToggles;
    [SerializeField] private Slider textOpacitySlider;

    protected override void OnClose()
    {
        
    }

    protected override void OnOpen()
    {
        textSizeToggles[Settings.instance.GetTextSize()].SetIsOnWithoutNotify(true);
        textSpacingToggles[Settings.instance.GetTextSpacing()].SetIsOnWithoutNotify(true);
        textOpacitySlider.SetValueWithoutNotify(Settings.instance.GetTextOpacity());
    }

    public void Event_ChangeLanguage(string newLocal){
        menu.InvokeOnButtonPress();
        Locals.ChangeLanguage(newLocal);
        Locals.onChangeLocal.Invoke();

        //LocalizedText[] texts = Object.FindObjectsByType<LocalizedText>(FindObjectsSortMode.None);
        //foreach(LocalizedText text in texts) text.ReloadText();
    }

    public void Event_ChangeTextSize(bool value){
        menu.InvokeOnButtonPress();
        for (int i = 0; i < textSizeToggles.Length; i++)
        {
            if (textSizeToggles[i].isOn)
            {
                Settings.instance.SetTextSize(i);
                break;
            }
        }
    }

    public void Event_ChangeTextSpacing(bool value){
        menu.InvokeOnButtonPress();
        for (int i = 0; i < textSpacingToggles.Length; i++)
        {
            if (textSpacingToggles[i].isOn)
            {
                Settings.instance.SetTextSpacing(i);
                break;
            }
        }
    }

    public void Event_ChangeTextOpacity(float opacity){
        menu.InvokeOnSliderChange();
        Settings.instance.SetTextOpacity(opacity);
    }

}
