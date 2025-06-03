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
    [SerializeField] private TMP_Dropdown typoPrimaryDropdown;
    [SerializeField] private TMP_Dropdown typoSecondaryDropdown;

    protected override void OnClose()
    {

    }

    protected override void OnOpen()
    {
        List<string> listTypos = new List<string>();
        foreach (TMP_FontAsset font in GameManager.instance.GetFonts())
        {
            listTypos.Add(font.name);
        }

        typoPrimaryDropdown.ClearOptions();
        typoPrimaryDropdown.AddOptions(listTypos);
        typoPrimaryDropdown.SetValueWithoutNotify(Settings.instance.GetCurrentTypoIndexPrimary());

        typoSecondaryDropdown.ClearOptions();
        typoSecondaryDropdown.AddOptions(listTypos);
        typoSecondaryDropdown.SetValueWithoutNotify(Settings.instance.GetCurrentTypoIndexSecondary());
    }



    /// <summary>
    /// Changes the current primary typo
    /// </summary>
    /// <param name="value">The new typo's index</param>
    public void Event_ChangeCurrentPrimaryTypo(Int32 value)
    {
        menu.InvokeOnButtonPress();
        Locals.ChangeFontPrimary(value);
        Settings.instance.SetCurrentTypoIndexPrimary(value);
    }
    
    /// <summary>
    /// Changes the current secondary typo
    /// </summary>
    /// <param name="value">The new typo's index</param>
    public void Event_ChangeCurrentSecondaryTypo(Int32 value){
        menu.InvokeOnButtonPress();
        Locals.ChangeFontSecondary(value);
        Settings.instance.SetCurrentTypoIndexSecondary(value);
    }
}
