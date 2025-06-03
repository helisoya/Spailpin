using System.Collections;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

/// <summary>
/// Represents the game's manager
/// </summary>
public class GameManager : MonoBehaviour
{
    [SerializeField] private InputActionAsset inputs;
    [SerializeField] private TMP_FontAsset[] fonts;
    [SerializeField] private GameItems items;
    [SerializeField] private Volume volume;

    public static GameManager instance { get; private set;}
    private Coroutine routineChangeScene;
    private SaveManager save;

    public bool changingScene {get{return routineChangeScene != null;}}
    public bool loadingSave {get; set;}

    void Awake()
    {

        if(instance == null){
            instance = this;
            Locals.Init();
            Settings.Init();
            save = new SaveManager(items);
            DontDestroyOnLoad(gameObject);
        }else{
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Updates the game's volume
    /// </summary>
    public void UpdateVolume(){
        LiftGammaGain gamma;
        float gammaValue = Settings.instance.GetCurrentGamma();
        volume.profile.TryGet<LiftGammaGain>(out gamma);
        gamma.gamma.SetValue(new Vector4Parameter(new Vector4(1.0f,1.0f,1.0f,gammaValue)));

        Bloom bloom;
        volume.profile.TryGet<Bloom>(out bloom);
        if(bloom) bloom.active = Settings.instance.IsBloomEnabled();
    }

    /// <summary>
    /// Gets the game's inputs
    /// </summary>
    /// <returns>The game's input</returns>
    public InputActionAsset GetInputs(){
        return inputs;
    }

    /// <summary>
    /// Gets the game's available fonts
    /// </summary>
    /// <returns>The game's fonts</returns>
    public TMP_FontAsset[] GetFonts(){
        return fonts;
    }


    public string mapName {
        get{
            return save.saveFile.mapName;
        }
        set{
            save.saveFile.mapName = value;
        }
    }

    /// <summary>
    /// Gets the save manager
    /// </summary>
    /// <returns>The save manager</returns>
    public SaveManager GetSaveManager(){
        return save;
    }
    
    /// <summary>
    /// Resets the current save
    /// </summary>
    public void ResetSave()
    {
        save = new SaveManager(items);
    }

    /// <summary>
    /// Change the current scene
    /// </summary>
    /// <param name="newScene">The new scene</param>
    /// <param name="inMainMenu">True if in the main menu</param>
    public void ChangeScene(string newScene, bool inMainMenu = false)
    {
        if (routineChangeScene != null) return;
        routineChangeScene = StartCoroutine(Routine_ChangeScene(newScene,inMainMenu));
    }

    /// <summary>
    /// Change the current scene
    /// </summary>
    /// <param name="newScene">The new scene</param>
    /// <param name="inMainMenu">True if in the main menu</param>
    private IEnumerator Routine_ChangeScene(string newScene, bool inMainMenu = false){
        
        if (inMainMenu) MainMenuGUI.instance.FadeTo(1);
        else GameGUI.instance.FadeTo(1);

        yield return new WaitForEndOfFrame();
        while((!inMainMenu && GameGUI.instance.fading) || (inMainMenu && MainMenuGUI.instance.fading)){
            yield return new WaitForEndOfFrame();
        }
        
        Time.timeScale = 1f;
        SceneManager.LoadScene(newScene);
        routineChangeScene = null;
    }
}
