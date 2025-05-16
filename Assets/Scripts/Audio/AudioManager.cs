using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;
using JetBrains.Annotations;
using System.Configuration.Assemblies;
using System.Runtime.CompilerServices;

public class AudioManager : MonoBehaviour
{

    private List<EventInstance> eventInstances;

    private List<StudioEventEmitter> eventEmitters;
    private EventReference eventReferences;

    private EventInstance ambienceEventInstance;
    private EventInstance musicEventInstance;  



    public static AudioManager instance { get; private set; }

    private void Awake()
    {
        if (instance == null)
        { 
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            //Debug.LogError("Found more than one Audio Manager in the scene");
            Destroy(gameObject);
        }
        

        eventInstances = new List<EventInstance>();

        eventEmitters = new List<StudioEventEmitter>();
    }

    private void Start()
    {
        InitializeAmbience(FMODEvents.instance.AmbOut);
        InitializeMusic(FMODEvents.instance.Music);
    }

    private void InitializeAmbience(EventReference ambienceEventReference)
    {
        ambienceEventInstance = CreateInstance(ambienceEventReference);
        ambienceEventInstance.start();
    }  

    public void SetCarillon(string parameterName, float parameterValue)
    {
        ambienceEventInstance.setParameterByName(parameterName, parameterValue);
    }
    
    private void InitializeMusic(EventReference musicEventReference)
    {
        ambienceEventInstance = CreateInstance(musicEventReference);
        ambienceEventInstance.start();
    }
    
    // Emitter sur un objet
    public StudioEventEmitter InitializeEventEmitter(EventReference eventReference, GameObject emitterGameObject)
    {
        StudioEventEmitter emitter = emitterGameObject.GetComponent<StudioEventEmitter>();
        emitter.EventReference = eventReference;
        eventEmitters.Add(emitter);
        return emitter;
    }

    // Création de la fonction permettant de jouer un son
    public void PlayOneShot(EventReference sound, Vector3 worldPos)
    {
        RuntimeManager.PlayOneShot(sound, worldPos);  
    }

    // Création de la fonction permettant de jouer un event en particulier
    public void PlayCarillon()
    {
        //FMODEvents(script attaché à AudioManager).instance(créer une instance).NomEvent, Position))
        PlayOneShot(FMODEvents.instance.Carillon, this.transform.position);
    }

    /*AudioManager.instance.PlayCarillon(); Exemple de comment appeler une fonction dans le code, il faut instancier l'AudioManager puis lui renseigner
      une position, un comportement etc, si il en a besoin
    */
    
    public EventInstance CreateInstance(EventReference eventReference)
    {
        EventInstance eventInstance = RuntimeManager.CreateInstance(eventReference);
        eventInstances.Add(eventInstance);
        return eventInstance;
    }

    private void CleanUp()
    {

        // stop and release any created instances
        foreach (EventInstance eventInstance in eventInstances)
        {
            eventInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            eventInstance.release();
        }
        
        // stop tout les emitters
        foreach(StudioEventEmitter emitter in eventEmitters)
        {
            emitter.Stop();
        }
    }

    private void OnDestroy()
    {
        CleanUp();
    }


}
