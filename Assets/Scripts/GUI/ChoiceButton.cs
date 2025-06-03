using UnityEngine;

/// <summary>
/// Represents a choice button
/// </summary>
public class ChoiceButton : MonoBehaviour
{
    [SerializeField] private LocalizedText label;
    private int linkedIndex;

    /// <summary>
    /// Initialize the button
    /// </summary>
    /// <param name="index">The button's index</param>
    /// <param name="key">The label's key</param>
    public void Init(int index, string key)
    {
        label.SetNewKey(key);
        linkedIndex = index;
    }


    /// <summary>
    /// On Click Event
    /// </summary>
    public void Click()
    {
        GameGUI.instance.SelectChoice(linkedIndex);
    }
}
