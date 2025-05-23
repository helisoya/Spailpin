using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;
using JetBrains.Annotations;
using System.Configuration.Assemblies;
using System.Runtime.CompilerServices;
using UnityEngine.UIElements;

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
        print(eventInstances);

        eventEmitters = new List<StudioEventEmitter>();
    }

    private void Start()
    {
        InitializeAmbience(FMODEvents.instance.Ambience);
        //InitializeMusic(FMODEvents.instance.MusicMenu);
    }

    private void InitializeAmbience(EventReference ambienceEventReference)
    {
        ambienceEventInstance = CreateInstance(ambienceEventReference);
        ambienceEventInstance.start();
    }
    public void SetAmbience(float parameterValue, string parameterName = "AmbienceChange")
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
    // Cr�ation de la fonction permettant de jouer un son avec des param�tres
    public void PlayOneShotParameter(EventReference sound, Vector3 worldPosition,string parameterName, int parameter)
    {
        EventInstance instance = CreateInstance(sound);
        instance.set3DAttributes(RuntimeUtils.To3DAttributes(worldPosition));
        instance.setParameterByName(parameterName, parameter);
        instance.start();
        instance.release();
    }

    // Cr�ation de la fonction permettant de jouer un son
    public void PlayOneShot(EventReference sound, Vector3 worldPos)
    {
        RuntimeManager.PlayOneShot(sound, worldPos);  
    }


 //FonctionDictionnaire RoomTheme

    public void PlayRoomTheme(string ID)
    {
        PlayOneShot(FMODEvents.instance.RoomTheme[ID], this.transform.position);
    }


    public void PlaySFX(string ID)
    {
        PlayOneShot(FMODEvents.instance.SFX[ID], this.transform.position);
    }


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
