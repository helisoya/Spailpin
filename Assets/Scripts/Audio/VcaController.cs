using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VcaController : MonoBehaviour
{
    [SerializeField] private float vcaVolume;

    private FMOD.Studio.VCA VcaControl;
    public string VcaName;

    private void Awake()
    {
        VcaControl = FMODUnity.RuntimeManager.GetVCA("vca:/" + VcaName);
    }

    public void SetVolume(float volume)
    {
        VcaControl.setVolume(volume);
        VcaControl.getVolume(out vcaVolume);

    }
}
