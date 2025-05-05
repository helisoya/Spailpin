using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

/// <summary>
/// Represents the audio page in the pause menu
/// </summary>
public class AudioPage : PausePage
{
    [Header("Audio Page")]
    [SerializeField] private Slider sliderMaster;
    [SerializeField] private Slider sliderMusic;
    [SerializeField] private Slider sliderSFX;
    [SerializeField] private UnityEvent<float> onVolumeMasterChange;
    [SerializeField] private UnityEvent<float> onVolumeMusicChange;
    [SerializeField] private UnityEvent<float> onVolumeSFXChange;

    void Start()
    {
        onVolumeMasterChange.Invoke(Settings.instance.GetVolumeMaster());
        onVolumeMusicChange.Invoke(Settings.instance.GetVolumeMusic());
        onVolumeSFXChange.Invoke(Settings.instance.GetVolumeSFX());
    }


    protected override void OnClose()
    {

    }

    protected override void OnOpen()
    {
        sliderMaster.SetValueWithoutNotify(Settings.instance.GetVolumeMaster());
        sliderMusic.SetValueWithoutNotify(Settings.instance.GetVolumeMusic());
        sliderSFX.SetValueWithoutNotify(Settings.instance.GetVolumeSFX());
    }

    /// <summary>
    /// Callback for changing the master volume
    /// </summary>
    /// <param name="newValue">The new volume</param>
    public void Event_ChangeMaster(float newValue)
    {
        Settings.instance.SetVolumeMaster(newValue);
        onVolumeMasterChange.Invoke(newValue);
    }

    /// <summary>
    /// Callback for changing the music volume
    /// </summary>
    /// <param name="newValue">The new volume</param>
    public void Event_ChangeMusic(float newValue)
    {
        Settings.instance.SetVolumeMusic(newValue);
        onVolumeMusicChange.Invoke(newValue);
    }

    /// <summary>
    /// Callback for changing the SFX volume
    /// </summary>
    /// <param name="newValue">The new volume</param>
    public void Event_ChangeSFX(float newValue)
    {
        Settings.instance.SetVolumeSFX(newValue);
        onVolumeSFXChange.Invoke(newValue);
    }
}
