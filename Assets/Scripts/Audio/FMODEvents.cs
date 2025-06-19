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
    [field: SerializeField] public EventReference CaribouFall { get; private set; }
    [field: SerializeField] public EventReference CaribouLand { get; private set; }
    [field: SerializeField] public EventReference CaribouBomb { get; private set; }
    [field: SerializeField] public EventReference CaribouClock { get; private set; }
    [field: SerializeField] public EventReference CaribouInteract { get; private set; }
    [field: SerializeField] public EventReference PuzzleSuccess { get; private set; }
    [field: SerializeField] public EventReference PuzzleFailed { get; private set; }
    [field: SerializeField] public EventReference Bonk { get; private set; }
    [field: SerializeField] public EventReference PuzzleGreenHouse { get; private set; }
    [field: SerializeField] public EventReference AudreyPuzzle { get; private set; }

    [SerializeField] public SerializedDictionary<string, EventReference> SFX;
    [SerializeField] public SerializedDictionary<string, EventReference> EmitterSFX;

    [field: Header("AMB")]
    [field: SerializeField] public EventReference Ambience { get; private set; }

    [field: Header("MUSIC")]
    [field: SerializeField] public EventReference MusicMenu { get; private set; }
    [field: SerializeField] public EventReference EndStay { get; private set; }
    [field: SerializeField] public EventReference EndLeave { get; private set; }
   
    [SerializeField] public SerializedDictionary<string, EventReference> RoomTheme;
    
   


    [field: Header("UI")]
    [field: SerializeField] public EventReference MenuOpen { get; private set; }
    [field: SerializeField] public EventReference MenuClosed { get; private set; }
    [field: SerializeField] public EventReference Menu { get; private set; }
    [field: SerializeField] public EventReference Button { get; private set; }
    [field: SerializeField] public EventReference PageTurn { get; private set; }
    [field: SerializeField] public EventReference JunoDiary { get; private set; }


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
