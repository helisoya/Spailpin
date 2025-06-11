using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System;
using UnityEngine.UI;
using UnityEngine.Rendering;

/// <summary>
/// Represents the accessibility page in the pause menu
/// </summary>
public class AccessPage : PausePage
{
    [Header("Accessibility")]
    [SerializeField] private TMP_Dropdown typoPrimaryDropdown;
    [SerializeField] private TMP_Dropdown typoSecondaryDropdown;
    [SerializeField] private Toggle togglePlayerOutline;
    [SerializeField] private Toggle toggleObjectsOutline;
    [SerializeField] private Slider outlineSliderRed;
    [SerializeField] private Slider outlineSliderGreen;
    [SerializeField] private Slider outlineSliderBlue;
    private Color outlineColor;

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


        togglePlayerOutline.SetIsOnWithoutNotify(Settings.instance.GetPlayerOutlineActive());
        toggleObjectsOutline.SetIsOnWithoutNotify(Settings.instance.GetObjectOutlineActive());

        outlineColor = Settings.instance.GetOutlineColor();
        outlineSliderRed.SetValueWithoutNotify(outlineColor.r);
        outlineSliderGreen.SetValueWithoutNotify(outlineColor.g);
        outlineSliderBlue.SetValueWithoutNotify(outlineColor.b);
    }

    /// <summary>
    /// Toggle the player's outline
    /// </summary>
    /// <param name="value">True if the outline is active</param>
    public void Event_TogglePlayerOutline(bool value)
    {
        menu.InvokeOnButtonPress();
        Settings.instance.SetPlayerOutlineActive(value);
    }

    /// <summary>
    /// Toggle the objects's outline
    /// </summary>
    /// <param name="value">True if the outline is active</param>
    public void Event_ToggleObjectOutline(bool value)
    {
        menu.InvokeOnButtonPress();
        Settings.instance.SetObjectOutlineActive(value);
    }

    /// <summary>
    /// Changes the outline color (red value)
    /// </summary>
    /// <param name="value">The color value</param>
    public void Event_SetRedSlider(float value)
    {
        menu.InvokeOnSliderChange();
        outlineColor.r = value;
        Settings.instance.SetOutlineColor(outlineColor);
    }

    /// <summary>
    /// Changes the outline color (green value)
    /// </summary>
    /// <param name="value">The color value</param>
    public void Event_SetGreenSlider(float value)
    {
        menu.InvokeOnSliderChange();
        outlineColor.g = value;
        Settings.instance.SetOutlineColor(outlineColor);
    }

    /// <summary>
    /// Changes the outline color (blue value)
    /// </summary>
    /// <param name="value">The color value</param>
    public void Event_SetBlueSlider(float value)
    {
        menu.InvokeOnSliderChange();
        outlineColor.b = value;
        Settings.instance.SetOutlineColor(outlineColor);
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
    public void Event_ChangeCurrentSecondaryTypo(Int32 value)
    {
        menu.InvokeOnButtonPress();
        Locals.ChangeFontSecondary(value);
        Settings.instance.SetCurrentTypoIndexSecondary(value);
    }
}
