using UnityEngine;

public class InterfaceSFX : MonoBehaviour
{
    public void PlayMenuOpen()
    {
        AudioManager.instance.PlayOneShot(FMODEvents.instance.MenuOpen, this.transform.position);
    }

    public void PlayMenuClosed()
    {
        AudioManager.instance.PlayOneShot(FMODEvents.instance.MenuClosed, this.transform.position);
    }

    public void ResetAmbience()
    {
        AudioManager.instance.SetAmbience(0);
    }

    public void PlayMenu(int PauseMenu)
    {
        //FMODEvents(script attaché à AudioManager).instance(créer une instance).NomEvent, Position))
        AudioManager.instance.PlayOneShotParameter(FMODEvents.instance.Menu, this.transform.position, "PauseMenu", PauseMenu);
    }
    public void PlayButton()
    {
        AudioManager.instance.PlayOneShot(FMODEvents.instance.Button, this.transform.position);
    }

    public void PlayPageTurn()
    {
        AudioManager.instance.PlayOneShot(FMODEvents.instance.PageTurn, this.transform.position);
    } 
    
    public void Dialogue()
    {
        AudioManager.instance.PlayOneShot(FMODEvents.instance.JunoDiary, this.transform.position);
    }

    public void EndDialogue()
    {
        AudioManager.instance.PlayOneShot(FMODEvents.instance.JunoEndDiary, this.transform.position);
    }

    public void MenuSelect()
    {
        AudioManager.instance.PlayOneShot(FMODEvents.instance.AudreyPuzzle, this.transform.position);
    }

    public void OpenHint()
    {
        AudioManager.instance.PlayOneShot(FMODEvents.instance.MenuOpen, this.transform.position);
    }

    public void CloseHint()
    {
        AudioManager.instance.PlayOneShot(FMODEvents.instance.MenuClosed, this.transform.position);
    }
}
