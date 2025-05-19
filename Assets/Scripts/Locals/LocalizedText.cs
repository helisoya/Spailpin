using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// Represents a text that is localized
/// </summary>
public class LocalizedText : MonoBehaviour
{
    [SerializeField] protected TMP_Text text;
    [SerializeField] protected string localKey;

    /// <summary>
    /// Changes the ID of the localized text
    /// </summary>
    /// <param name="key">The new ID</param>
    public void SetNewKey(string key)
    {
        localKey = key;
        ReloadText();
    }

    /// <summary>
    /// Reloads the localized text
    /// </summary>
    public virtual void ReloadText()
    {
        text.text = Locals.GetLocal(localKey);
    }

    void Start()
    {
        ReloadText();
        SetFont(GameManager.instance.GetFonts()[Locals.fontIndex]);
        Locals.onChangeLocal.AddListener(ReloadText);
        Locals.onChangeFont.AddListener(SetFont);
    }

    protected void OnDestroy()
    {
        Locals.onChangeLocal.RemoveListener(ReloadText);
        Locals.onChangeFont.RemoveListener(SetFont);
    }

    /// <summary>
    /// Sets the current font for the text
    /// </summary>
    /// <param name="font">The new font</param>
    public void SetFont(TMP_FontAsset font){
        text.font = font;
    } 

    /// <summary>
    /// Returns the text field
    /// </summary>
    /// <returns>The text field</returns>
    public TMP_Text GetText()
    {
        return text;
    }

    /// <summary>
    /// Changes the text's color
    /// </summary>
    /// <param name="color">The new color</param>
    public void SetColor(Color color)
    {
        text.color = color;
    }

    public string key { get { return localKey; } }
}