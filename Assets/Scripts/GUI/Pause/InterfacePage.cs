using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Represents the interface page in the pause menu
/// </summary>
public class InterfacePage : PausePage
{  
    [Header("Interface")]
    [SerializeField] private Toggle fullscreenToggle;
    [SerializeField] private TMP_Dropdown resolutionDropdown; 
    private Resolution[] resolutions;

    protected override void OnClose()
    {
    
    }

    protected override void OnOpen()
    {
        resolutions = Screen.resolutions;
        List<string> resList = new List<string>();
        int currentResIdx = 0;
        Resolution currentRes = Screen.currentResolution;
        Resolution resolution;
        for(int i = 0; i < resolutions.Length;i++){
            resolution = resolutions[i];
            resList.Add(resolution.width+"x"+resolution.height);
            if(currentResIdx == 0 && resolution.width == currentRes.width && resolution.height == currentRes.height) currentResIdx = i;
        }
        resolutionDropdown.ClearOptions();
        resolutionDropdown.AddOptions(resList);
        resolutionDropdown.SetValueWithoutNotify(currentResIdx);

        fullscreenToggle.SetIsOnWithoutNotify(Screen.fullScreen);
    }


    public void Event_ChangeFullscreen(bool fullscreen){
        Settings.instance.SetFullScreen(fullscreen);
    }

    public void Event_ChangeResolution(Int32 index){
        Settings.instance.SetResolution(resolutions[index]);
    }

    public void Event_ChangeBrightness(float value){

    }

    public void Event_ChangeShadows(bool shadows){

    }

    public void Event_ChangeBloom(bool bloom){

    }
}
