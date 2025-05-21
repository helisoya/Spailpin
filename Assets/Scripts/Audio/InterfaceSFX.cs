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
    public void PlayButton()
    {
        AudioManager.instance.PlayOneShot(FMODEvents.instance.Button, this.transform.position);
    }
}
