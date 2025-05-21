using UnityEngine;
using FMODUnity;
using AYellowpaper.SerializedCollections;

public class FMODEvents : MonoBehaviour
{
    [field: Header("SFX")]

    [field: SerializeField] public EventReference FS_System { get; private set; }
    [field: SerializeField] public EventReference Carillon { get; private set; }
    [field: SerializeField] public EventReference Bike { get; private set; }
    [field: SerializeField] public EventReference Character { get; private set; }

    [SerializeField] public SerializedDictionary<string, EventReference> SFX;

    [field: Header("AMB")]
    [field: SerializeField] public EventReference AmbOut { get; private set; }

    [field: Header("MUSIC")]
    [field: SerializeField] public EventReference MusicMenu { get; private set; }
   
    [SerializeField] public SerializedDictionary<string, EventReference> RoomTheme;
    
   


    [field: Header("UI")]
    [field: SerializeField] public EventReference MenuOpen { get; private set; }
    [field: SerializeField] public EventReference MenuClosed { get; private set; }
    [field: SerializeField] public EventReference Button { get; private set; }

    [field: SerializeField] public EventReference Magic { get; private set; }

    public static FMODEvents instance {  get; private set; }


    private void Awake()
    {
        if (instance != null)
        {
            //Debug.Log("Found more tahn one FMOD Events instance in the scene.");
        }
        instance = this;
    }

}
