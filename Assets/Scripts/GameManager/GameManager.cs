using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Represents the game's manager
/// </summary>
public class GameManager : MonoBehaviour
{
    [SerializeField] private InputActionAsset inputs;

    public static GameManager instance { get; private set;}
    private SaveFile saveFile;


    void Awake()
    {

        if(instance == null){
            instance = this;
            saveFile = new SaveFile();
            Locals.Init();
            Settings.Init();
            DontDestroyOnLoad(gameObject);
        }else{
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Gets the game's inputs
    /// </summary>
    /// <returns>The game's input</returns>
    public InputActionAsset GetInputs(){
        return inputs;
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
