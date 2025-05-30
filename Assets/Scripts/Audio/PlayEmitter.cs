using UnityEngine;
using FMODUnity;
using FMOD.Studio;


[RequireComponent(typeof(StudioEventEmitter))]
public class PlayEmitter : MonoBehaviour
{
    private StudioEventEmitter emitter;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //emitter = AudioManager.instance.InitializeEventEmitter(FMODEvents.instance.Magic, this.gameObject);
        emitter.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
