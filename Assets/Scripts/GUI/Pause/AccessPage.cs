using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System;

/// <summary>
/// Represents the accessibility page in the pause menu
/// </summary>
public class AccessPage : PausePage
{
    [Header("Accessibility")]
    [SerializeField] private TMP_Dropdown typoDropdown;

    protected override void OnClose()
    {
    
    }

    protected override void OnOpen()
    {
        List<string> listTypos = new List<string>();
        foreach(TMP_FontAsset font in GameManager.instance.GetFonts()){
            listTypos.Add(font.name);
        }
        typoDropdown.ClearOptions();
        typoDropdown.AddOptions(listTypos);
        typoDropdown.SetValueWithoutNotify(Settings.instance.GetCurrentTypoIndex());
    }



    /// <summary>
    /// Changes the current typo
    /// </summary>
    /// <param name="value">The new typo's index</param>
    public void Event_ChangeCurrentTypo(Int32 value){
        TMP_FontAsset selectedFont = GameManager.instance.GetFonts()[value];

        // Do something with typos

        Settings.instance.SetCurrentTypoIndex(value);
    }
}
