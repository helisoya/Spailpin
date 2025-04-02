using UnityEngine;

/// <summary>
/// Represents the game's manager
/// </summary>
public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set;}
    private SaveFile saveFile;


    void Awake()
    {

        if(instance == null){
            instance = this;
            saveFile = new SaveFile();
            Locals.Init();
            DontDestroyOnLoad(gameObject);
        }else{
            Destroy(gameObject);
        }
    }


    public string mapName {
        get{
            return saveFile.mapName;
        }
        set{
            saveFile.mapName = value;
        }
    }
}
