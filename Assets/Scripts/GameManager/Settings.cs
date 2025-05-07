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
        Save();
    }

    /// <summary>
    /// Save the current gamepad profile's index
    /// </summary>
    /// <param name="newIdx">The new index</param>
    public void SetCurrentGamePadProfileIdx(int newIdx)
    {
        data.currentGamepadProfileIdx = newIdx;
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
    /// Save the current typo index
    /// </summary>
    /// <param name="newIdx">The new typo index</param>
    public void SetCurrentTypoIndex(int newIdx)
    {
        data.currentTypoIndex = newIdx;
        Save();
    }

    /// <summary>
    /// Get the current typo index
    /// </summary>
    /// <returns>The current typo index</returns>
    public int GetCurrentTypoIndex()
    {
        return data.currentTypoIndex;
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
                volumeSfx = 1
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
    public int currentTypoIndex;
    public float volumeMaster;
    public float volumeSfx;
    public float volumeMusic;

    public SettingsData()
    {
    }
}