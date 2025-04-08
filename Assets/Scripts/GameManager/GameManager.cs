using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

/// <summary>
/// Represents the game's manager
/// </summary>
public class GameManager : MonoBehaviour
{
    [SerializeField] private InputActionAsset inputs;

    public static GameManager instance { get; private set;}
    [HideInInspector] public SaveFile saveFile {get; private set;}
    private Coroutine routineChangeScene;

    private string saveFilePath = FileManager.savPath + "save.sav";
    public bool saveFileExists {get{return File.Exists(saveFilePath);}}
    public bool changingScene {get{return routineChangeScene != null;}}
    public bool loadingSave {get; set;}

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

    /// <summary>
    /// Saves the game
    /// </summary>
    public void SaveGame(){
        saveFile.currentRoom = Player.instance.CurrentRoom;
        saveFile.playerPosition = Player.instance.position;
        saveFile.playerRotation = Player.instance.rotation;
        FileManager.SaveJSON(saveFilePath, saveFile);
    }

    /// <summary>
    /// Loads the game
    /// </summary>
    public void LoadGame(){
        if(saveFileExists){
            saveFile = FileManager.LoadJSON<SaveFile>(saveFilePath);
            loadingSave = true;
            ChangeScene(saveFile.mapName);
        }
    }

    /// <summary>
    /// Change the current scene
    /// </summary>
    /// <param name="newScene">The new scene</param>
    public void ChangeScene(string newScene){
        if(routineChangeScene != null) return;
        routineChangeScene = StartCoroutine(Routine_ChangeScene(newScene));
    }

    /// <summary>
    /// Change the current scene
    /// </summary>
    /// <param name="newScene">The new scene</param>
    private IEnumerator Routine_ChangeScene(string newScene){
        GameGUI.instance.FadeTo(1);
        yield return new WaitForEndOfFrame();
        while(GameGUI.instance.fading){
            yield return new WaitForEndOfFrame();
        }
        
        Time.timeScale = 1f;
        SceneManager.LoadScene(newScene);
        routineChangeScene = null;
    }
}
