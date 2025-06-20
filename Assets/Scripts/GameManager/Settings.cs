using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Handles the game's settings
/// </summary>
public class Settings
{
    private SettingsData data;
    public static Settings instance;

    public string filePath
    {
        get
        {
            return FileManager.savPath + "settings.sav";
        }
    }

    public bool fileExistsOnDisk
    {
        get
        {
            return System.IO.File.Exists(filePath);
        }
    }

    public static void Init()
    {
        instance = new Settings();
    }

    /// <summary>
    /// Changes the current language
    /// </summary>
    /// <param name="newLanguage">The new language</param>
    public void ChangeLanguage(string newLanguage)
    {
        data.language = newLanguage;
        Locals.ChangeLanguage(newLanguage);
        Save();
    }

    /// <summary>
    /// Changes if the game is in fullscreen or not
    /// </summary>
    /// <param name="isFullScreen">True if the game is in fullscreen</param>
    public void SetFullScreen(bool isFullScreen)
    {
        data.fullscreen = isFullScreen;
        Screen.fullScreen = isFullScreen;
        Save();
    }

    /// <summary>
    /// Changes the game's resolution
    /// </summary>
    /// <param name="newResolution">The new resolution</param>
    public void SetResolution(Resolution newResolution)
    {
        data.refreshRateNumerator = newResolution.refreshRateRatio.numerator;
        data.refreshRateDenominator = newResolution.refreshRateRatio.denominator;
        data.screenWidth = newResolution.width;
        data.screenHeight = newResolution.height;
        Screen.SetResolution(newResolution.width, newResolution.height, Screen.fullScreenMode, newResolution.refreshRateRatio);
        Save();
    }

    /// <summary>
    /// Sets the master's volume
    /// </summary>
    /// <param name="value">The new volume</param>
    public void SetVolumeMaster(float value)
    {
        data.volumeMaster = value;
        Save();
    }

    /// <summary>
    /// Gets the master's volume
    /// </summary>
    /// <returns>The master's volume</returns>
    public float GetVolumeMaster()
    {
        return data.volumeMaster;
    }

    /// <summary>
    /// Sets the music's volume
    /// </summary>
    /// <param name="value">The new volume</param>
    public void SetVolumeMusic(float value)
    {
        data.volumeMusic = value;
        Save();
    }

    /// <summary>
    /// Gets the music's volume
    /// </summary>
    /// <returns>The music's volume</returns>
    public float GetVolumeMusic()
    {
        return data.volumeMusic;
    }

    /// <summary>
    /// Sets the SFX's volume
    /// </summary>
    /// <param name="value">The new volume</param>
    public void SetVolumeSFX(float value)
    {
        data.volumeSfx = value;
        Save();
    }

    /// <summary>
    /// Gets the SFX's volume
    /// </summary>
    /// <returns>The SFX's volume</returns>
    public float GetVolumeSFX()
    {
        return data.volumeSfx;
    }

    /// <summary>
    /// Save the game's bindings
    /// </summary>
    /// <param name="bindings">The new bindings (JSON)</param>
    public void SetBindings(string bindings)
    {
        data.remaping = bindings;
        if(!GameManager.instance.inMainMenu) Player.instance.RefreshBindings();
        Save();
    }

    /// <summary>
    /// Save the current gamepad profile's index
    /// </summary>
    /// <param name="newIdx">The new index</param>
    public void SetCurrentGamePadProfileIdx(int newIdx)
    {
        data.currentGamepadProfileIdx = newIdx;
        if(!GameManager.instance.inMainMenu) Player.instance.RefreshBindings();
        Save();
    }

    /// <summary>
    /// Get the current gamepad's profile index
    /// </summary>
    /// <returns>The current index</returns>
    public int GetCurrentGamePadProfileIdx()
    {
        return data.currentGamepadProfileIdx;
    }

    /// <summary>
    /// Save the current primary typo index
    /// </summary>
    /// <param name="newIdx">The new typo index</param>
    public void SetCurrentTypoIndexPrimary(int newIdx)
    {
        data.currentTypoIndexPrimary = newIdx;
        Save();
    }

    /// <summary>
    /// Get the current primary typo index
    /// </summary>
    /// <returns>The current typo index</returns>
    public int GetCurrentTypoIndexPrimary()
    {
        return data.currentTypoIndexPrimary;
    }
    
    /// <summary>
    /// Save the current secondary typo index
    /// </summary>
    /// <param name="newIdx">The new typo index</param>
    public void SetCurrentTypoIndexSecondary(int newIdx)
    {
        data.currentTypoIndexSecondary = newIdx;
        Save();
    }

    /// <summary>
    /// Get the current secondary typo index
    /// </summary>
    /// <returns>The current typo index</returns>
    public int GetCurrentTypoIndexSecondary()
    {
        return data.currentTypoIndexSecondary;
    }

    /// <summary>
    /// Gets the current gamma
    /// </summary>
    /// <returns>The current gama</returns>
    public float GetCurrentGamma()
    {
        return data.gamma;
    }

    /// <summary>
    /// Sets the current gamma
    /// </summary>
    /// <param name="gamma">The new gamma</param>
    public void SetGamma(float gamma){
        data.gamma = gamma;
        GameManager.instance.UpdateVolume();
        Save();
    }

    /// <summary>
    /// Gets if the bloom is enabled
    /// </summary>
    /// <returns>True if the bloom is enabled</returns>
    public bool IsBloomEnabled(){
        return data.bloom;
    }

    /// <summary>
    /// Changes if the bloom is enabled or not
    /// </summary>
    /// <param name="value">True if enabled</param>
    public void SetBloomEnabled(bool value){
        data.bloom = value;
        GameManager.instance.UpdateVolume();
        Save();
    }

    /// <summary>
    /// Sets the game's text size
    /// </summary>
    /// <param name="size">The size index</param>
    public void SetTextSize(int size){
        data.textSize = size;
        if(!GameManager.instance.inMainMenu) GameGUI.instance.SetDialogSize(size);
        Save();
    }

    /// <summary>
    /// Gets the game's text size index
    /// </summary>
    /// <returns>The size index</returns>
    public int GetTextSize(){
        return data.textSize;
    }

    /// <summary>
    /// Sets the game's text spacing
    /// </summary>
    /// <param name="spacing">The spacing index</param>
    public void SetTextSpacing(int spacing){
        data.textSpacing = spacing;
        if(!GameManager.instance.inMainMenu) GameGUI.instance.SetDialogSpacing(spacing);
        Save();
    }

    /// <summary>
    /// Gets the game's text spacing index
    /// </summary>
    /// <returns>The spacing index</returns>
    public int GetTextSpacing(){
        return data.textSpacing;
    } 

    /// <summary>
    /// Sets the game's test opacity index
    /// </summary>
    /// <param name="opacity">The opacity index</param>
    public void SetTextOpacity(float opacity){
        data.textOpacity = opacity;
        if(!GameManager.instance.inMainMenu) GameGUI.instance.SetDialogBackgroundAlpha(opacity);
        Save();
    }

    /// <summary>
    /// Gets the game's text opacity index
    /// </summary>
    /// <returns>The opacity index</returns>
    public float GetTextOpacity(){
        return data.textOpacity;
    }

    /// <summary>
    /// Changes the outline color
    /// </summary>
    /// <param name="color">The outline color</param>
    public void SetOutlineColor(Color color)
    {
        data.outlineColor = color;
        Save();
    }

    /// <summary>
    /// Gets the outline's color
    /// </summary>
    /// <returns>The outline's color</returns>
    public Color GetOutlineColor()
    {
        return data.outlineColor;
    }

    /// <summary>
    /// Changes if the player outline is active or not
    /// </summary>
    /// <param name="active">True if active</param>
    public void SetPlayerOutlineActive(bool active)
    {
        data.outlinePlayer = active;
        Save();
    }

    /// <summary>
    /// Gets if the player outline is active or not
    /// </summary>
    /// <returns>True if the player outline is active</returns>
    public bool GetPlayerOutlineActive()
    {
        return data.outlinePlayer;
    }

    /// <summary>
    /// Changes if the objects outline is active or not
    /// </summary>
    /// <param name="active">True if active</param>
    public void SetObjectOutlineActive(bool active)
    {
        data.outlineObjects = active;
        Save();
    }

    /// <summary>
    /// Gets if the objects outline is active or not
    /// </summary>
    /// <returns>True if the objects outline is active</returns>
    public bool GetObjectOutlineActive()
    {
        return data.outlineObjects;
    }




    /// <summary>
    /// Loads the settings from disk
    /// </summary>
    private void Load()
    {
        data = FileManager.LoadJSON<SettingsData>(filePath);

        RefreshRate refreshRate = new RefreshRate
        {
            denominator = data.refreshRateDenominator,
            numerator = data.refreshRateNumerator
        };

        Screen.SetResolution(data.screenWidth, data.screenHeight, Screen.fullScreenMode, refreshRate);
        Locals.ChangeLanguage(data.language);
        Screen.fullScreen = data.fullscreen;
        GameManager.instance.GetInputs().LoadBindingOverridesFromJson(data.remaping);
        GameManager.instance.UpdateVolume();
        Locals.ChangeFontPrimary(data.currentTypoIndexPrimary);
        Locals.ChangeFontSecondary(data.currentTypoIndexSecondary);
    }

    /// <summary>
    /// Saves the settings to disk
    /// </summary>
    private void Save()
    {
        FileManager.SaveJSON(filePath, data);
    }

    private Settings()
    {
        instance = this;
        if (fileExistsOnDisk)
        {
            Load();
        }
        else
        {
            data = new SettingsData
            {
                refreshRateNumerator = Screen.currentResolution.refreshRateRatio.numerator,
                refreshRateDenominator = Screen.currentResolution.refreshRateRatio.denominator,
                screenHeight = Screen.currentResolution.height,
                screenWidth = Screen.currentResolution.width,
                fullscreen = Screen.fullScreen,
                language = Locals.current,
                remaping = GameManager.instance.GetInputs().SaveBindingOverridesAsJson(),
                volumeMaster = 1,
                volumeMusic = 1,
                volumeSfx = 1,
                gamma = 0,
                bloom = true,
                currentTypoIndexPrimary = 0,
                currentTypoIndexSecondary = 1,
                textSize = 0,
                textSpacing = 0,
                textOpacity = 0.8f,
                outlinePlayer = false,
                outlineObjects = true,
                outlineColor = Color.white
            };
            Save();
        }
    }
}



[System.Serializable]
public class SettingsData
{
    public uint refreshRateNumerator;
    public uint refreshRateDenominator;
    public int screenHeight;
    public int screenWidth;
    public bool fullscreen;

    public string language;

    public string remaping;
    public int currentGamepadProfileIdx;
    public int currentTypoIndexPrimary;
    public int currentTypoIndexSecondary;
    public float volumeMaster;
    public float volumeSfx;
    public float volumeMusic;

    public float gamma;
    public bool bloom;

    public int textSize;
    public int textSpacing;
    public float textOpacity;

    public bool outlinePlayer;
    public bool outlineObjects;
    public Color outlineColor;

    public SettingsData()
    {
    }
}