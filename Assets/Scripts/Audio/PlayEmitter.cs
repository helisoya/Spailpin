using UnityEngine;
using FMODUnity;
using FMOD.Studio;


[RequireComponent(typeof(StudioEventEmitter))]
public class PlayEmitter : MonoBehaviour
{
    private StudioEventEmitter emitter;
    [SerializeField] private string ID;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(FMODEvents.instance.EmitterSFX.TryGetValue(ID, out EventReference reference))
        {
            emitter = AudioManager.instance.InitializeEventEmitter(reference, this.gameObject);
            emitter.Play();
        }
    }
}
