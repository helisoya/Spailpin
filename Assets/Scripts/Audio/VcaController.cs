using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VcaController : MonoBehaviour
{
    [SerializeField] private float vcaVolume;

    private FMOD.Studio.VCA VcaControl;
    public string VcaName;

    private Slider slider;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        VcaControl = FMODUnity.RuntimeManager.GetVCA("vca:/" + VcaName);
        slider = GetComponent<Slider>();
        VcaControl.getVolume(out vcaVolume);
    }

    public void SetVolume(float volume)
    {
        VcaControl.setVolume(volume);
        VcaControl.getVolume(out vcaVolume);

    }
}
