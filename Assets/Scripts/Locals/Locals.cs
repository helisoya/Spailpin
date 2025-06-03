using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Handles the languages
/// </summary>
public class Locals
{
    private static Locals self;
    private string currentLanguage;
    private int currentFontIdxPrimary;
    private int currentFontIdxSecondary;

    public static string current
    {
        get
        {
            return self.currentLanguage;
        }
    }

    public static int fontIndexPrimary {
        get{
            return self.currentFontIdxPrimary;
        }
    }

    public static int fontIndexSecondary {
        get{
            return self.currentFontIdxSecondary;
        }
    }

    private Dictionary<string, string> locals;

    public static UnityEvent onChangeLocal = new UnityEvent();
    public static UnityEvent<TMP_FontAsset> onChangeFontPrimary = new UnityEvent<TMP_FontAsset>();
    public static UnityEvent<TMP_FontAsset> onChangeFontSecondary = new UnityEvent<TMP_FontAsset>();

    /// <summary>
    /// Initiliazes the Locals
    /// </summary>
    public static void Init()
    {
        new Locals();
    }

    public Locals()
    {
        self = this;
        locals = new Dictionary<string, string>();
        currentFontIdxPrimary = 0;
        currentFontIdxSecondary = 1;
        ChangeLanguage("eng");

    }

    /// <summary>
    /// Changes the current language
    /// </summary>
    /// <param name="newOne">The new language's code</param>
    public static void ChangeLanguage(string newOne)
    {
        if(Locals.self == null) Init();
        if (newOne.Equals(self.currentLanguage)) return;

        self.currentLanguage = newOne;
        self.locals.Clear();
        self.LoadContent(newOne + "_system");
        self.LoadContent(newOne + "_story");
    }
    
    /// <summary>
    /// Changes the current primary font
    /// </summary>
    /// <param name="fontIndex">The new font</param>
    public static void ChangeFontPrimary(int fontIndex){
        if(Locals.self == null) Init();

        self.currentFontIdxPrimary = fontIndex;
        onChangeFontPrimary.Invoke(GameManager.instance.GetFonts()[fontIndex]);
    }
    
    /// <summary>
    /// Changes the current secondary font
    /// </summary>
    /// <param name="fontIndex">The new font</param>
    public static void ChangeFontSecondary(int fontIndex){
        if(Locals.self == null) Init();

        self.currentFontIdxSecondary = fontIndex;
        onChangeFontSecondary.Invoke(GameManager.instance.GetFonts()[fontIndex]);
    }

    /// <summary>
    /// Gets a localized string
    /// </summary>
    /// <param name="key">The string's ID</param>
    /// <returns>The localized string</returns>
    public static string GetLocal(string key)
    {
        if (Locals.self == null) Init();
        if (key != null && self.locals.ContainsKey(key)) return self.locals[key];
        return key;
    }

    /// <summary>
    /// Loads the content of a file
    /// </summary>
    /// <param name="fileName">The filename</param>
    void LoadContent(string fileName)
    {
        List<string> fileContent = FileManager.ReadTextAsset(Resources.Load<TextAsset>("Locals/" + fileName));
        string line;
        string[] split;

        for (int i = 0; i < fileContent.Count; i++)
        {
            line = fileContent[i];
            if (line.StartsWith("#") || string.IsNullOrWhiteSpace(line)) continue;

            split = line.Split(" = ");

            if (split.Length != 2)
            {
                Debug.Log("Error on line " + line + ". There should be only one = .");
                continue;
            }

            if (split[0].EndsWith(" "))
            {
                split[0] = split[0].Substring(0, split[0].Length - 1);
            }
            if (split[1].EndsWith(" "))
            {
                split[1] = split[1].Substring(0, split[1].Length - 1);
            }
            locals.Add(split[0], split[1]);
        }
    }
}