using UnityEngine;

/// <summary>
/// Represents the language page in the pause menu
/// </summary>
public class LanguagePage : PausePage
{
    protected override void OnClose()
    {
    
    }

    protected override void OnOpen()
    {

    }

    public void Event_ChangeLanguage(string newLocal){
        Locals.ChangeLanguage(newLocal);

        LocalizedText[] texts = Object.FindObjectsByType<LocalizedText>(FindObjectsSortMode.None);
        foreach(LocalizedText text in texts) text.ReloadText();
    }

    public void Event_ChangeTextSize(bool value){

    }

    public void Event_ChangeTextSpacing(bool value){

    }

    public void Event_ChangeTextOpacity(float opacity){

    }

}
