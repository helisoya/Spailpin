using UnityEngine;
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
    private int currentPage = 0;

    public bool isOpen {get{return root.activeInHierarchy;}}

    /// <summary>
    /// Opens the pause menu
    /// </summary>
    public void Open(){
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
    /// Opens a new page
    /// </summary>
    /// <param name="page"></param>
    public void OpenNewPage(int page){
        pages[currentPage].Close();
        currentPage = page;
        pages[page].Open();

        foreach(Button button in buttons){
            Navigation navigation = button.navigation;
            navigation.selectOnDown = pages[page].GetFirstObject();
            button.navigation = navigation;
        }
        if(!EventSystem.current.alreadySelecting){
            EventSystem.current.SetSelectedGameObject(buttons[page].gameObject);
        }
    }

}
