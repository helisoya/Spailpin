using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// Represents the pause menu
/// </summary>
public class PauseMenu : MonoBehaviour
{
    [Header("General")]
    [SerializeField] private GameObject root;
    [SerializeField] private PausePage[] pages;
    [SerializeField] private Button[] buttons;
    [SerializeField] private Color[] colors;
    [SerializeField] private Image backgroundImg;

    [Header("Audio Events")]
    [SerializeField] private UnityEvent onButtonPress;
    [SerializeField] private UnityEvent onPageChange;
    [SerializeField] private UnityEvent onSliderChange;
    private int currentPage = 0;

    public bool isOpen {get{return root.activeInHierarchy;}}

    void Awake()
    {
        foreach (PausePage page in pages)
        {
            page.menu = this;
        }

        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].GetComponent<Image>().color = colors[i];
        }
    }

    /// <summary>
    /// Opens the pause menu
    /// </summary>
    public void Open()
    {
        root.SetActive(true);
        OpenNewPage(0);
    }

    /// <summary>
    /// Closes the pause menu
    /// </summary>
    public void Close(){
        root.SetActive(false);
    }

    /// <summary>
    /// Invokes the onButtonPress event
    /// </summary>
    public void InvokeOnButtonPress()
    {
        onButtonPress.Invoke();
    }

    /// <summary>
    /// Invokes the onPageChange event
    /// </summary>
    public void InvokeOnPageChange()
    {
        onPageChange.Invoke();
    }

    /// <summary>
    /// Invokes the onSliderChange event
    /// </summary>
    public void InvokeOnSliderChange()
    {
        onSliderChange.Invoke();
    }

    /// <summary>
    /// Opens a new page
    /// </summary>
    /// <param name="page"></param>
    public void OpenNewPage(int page)
    {
        InvokeOnPageChange();

        pages[currentPage].Close();
        currentPage = page;
        pages[page].Open();
        backgroundImg.color = colors[page];

        foreach (Button button in buttons)
        {
            Navigation navigation = button.navigation;
            navigation.selectOnDown = pages[page].GetFirstObject();
            button.navigation = navigation;
        }
        if (!EventSystem.current.alreadySelecting)
        {
            EventSystem.current.SetSelectedGameObject(buttons[page].gameObject);
        }
    }

}
