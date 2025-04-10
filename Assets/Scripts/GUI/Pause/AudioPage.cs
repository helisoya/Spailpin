using UnityEngine;

/// <summary>
/// Represents the audio page in the pause menu
/// </summary>
public class AudioPage : PausePage
{
    protected override void OnClose()
    {
    
    }

    protected override void OnOpen()
    {

    }

    /// <summary>
    /// Callback for changing the master volume
    /// </summary>
    /// <param name="newValue">The new volume</param>
    public void Event_ChangeMaster(float newValue){

    }

    /// <summary>
    /// Callback for changing the music volume
    /// </summary>
    /// <param name="newValue">The new volume</param>
    public void Event_ChangeMusic(float newValue){
        
    }

    /// <summary>
    /// Callback for changing the SFX volume
    /// </summary>
    /// <param name="newValue">The new volume</param>
    public void Event_ChangeSFX(float newValue){
        
    }
}
