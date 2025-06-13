using FMOD;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Handles the main menu GUI
/// </summary>
public class MainMenuGUI : MonoBehaviour
{

    [Header("Start Data")]
    [SerializeField] private string startScene = "ExteriorHouse";

    [Header("Components")]
    [SerializeField] private Button continueButton;
    [SerializeField] private PauseMenu pauseMenu;
    [SerializeField] private Fade fade;

    [Header("Selectable Objects")]
    [SerializeField] private GameObject defaultSelectable;
    [SerializeField] private GameObject optionsSelectable;

    [Header("Audio Event")]
    [SerializeField] private UnityEvent onButtonClick;
    [SerializeField] private UnityEvent<int> onPauseOpen;
    [SerializeField] private UnityEvent onNewGame;
    public UnityEvent<string> onDeviceChange;

    public static MainMenuGUI instance;
    public bool fading { get { return fade.fading; } }

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        continueButton.interactable = GameManager.instance.GetSaveManager().saveFileExists;
        EventSystem.current.SetSelectedGameObject(defaultSelectable);
    }

    /// <summary>
    /// Fades the screen
    /// </summary>
    /// <param name="alpha">The alpha target</param>
    /// <param name="speed">The fading speed</param>
    public void FadeTo(float alpha, float speed = 2f)
    {
        fade.FadeTo(alpha, speed);
    }


    /// <summary>
    /// On Previous Callback
    /// </summary>
    /// <param name="value">The input value</param>
    public void OnPrevious(InputValue value)
    {
        if (GameManager.instance.changingScene) return;

        if (pauseMenu.isOpen)
        {
            pauseMenu.Close();
            onPauseOpen.Invoke(1);
            EventSystem.current.SetSelectedGameObject(optionsSelectable);
        }
    }

    /// <summary>
    /// On Controls changed callback
    /// </summary>
    /// <param name="input">The player input</param>
    void OnControlsChanged(PlayerInput input)
    {
        print(input.currentControlScheme);
        if (Gamepad.current != null) Gamepad.current.SetMotorSpeeds(0.0f, 0.0f);
        onDeviceChange.Invoke(input.currentControlScheme);
    }

    /// <summary>
    /// OnEasterEgg callback
    /// </summary>
    /// <param name="value">The movement value</param>
    void OnEasterEgg(InputValue value)
    {
        if (GameManager.instance.changingScene) return;
        SceneManager.LoadScene("PinpinMobile");
    }


    /// <summary>
    /// Callback for starting a new game
    /// </summary>
    public void Event_NewGame()
    {
        if (GameManager.instance.changingScene) return;

        GameManager.instance.loadingSave = false;
        GameManager.instance.ResetSave();
        onButtonClick.Invoke();
        onNewGame.Invoke();
        GameManager.instance.ChangeScene(startScene, true);
    }

    /// <summary>
    /// Callback for loading a saved game
    /// </summary>
    public void Event_Continue()
    {
        if (GameManager.instance.changingScene) return;

        GameManager.instance.loadingSave = true;
        onButtonClick.Invoke();
        GameManager.instance.GetSaveManager().LoadGame(true);
    }

    /// <summary>
    /// Callback for opening the options
    /// </summary>
    public void Event_Options()
    {
        if (GameManager.instance.changingScene) return;

        pauseMenu.Open();
        onPauseOpen.Invoke(0);
    }

    /// <summary>
    /// Callback for quiting the game
    /// </summary>
    public void Event_Quit()
    {
        if (GameManager.instance.changingScene) return;

        onButtonClick.Invoke();
        Application.Quit();
    }
}
