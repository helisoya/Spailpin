using UnityEngine;

public class JohnCaribou : MonoBehaviour
{
    public void PlayCaribouFall()
    {
        AudioManager.instance.PlayOneShot(FMODEvents.instance.CaribouFall, this.transform.position);
    } 
    
    public void PlayCaribouLand()
    {
        AudioManager.instance.PlayOneShot(FMODEvents.instance.CaribouLand, this.transform.position);
    }

    public void PlayCaribouInteract()
    {
        AudioManager.instance.PlayOneShot(FMODEvents.instance.CaribouInteract, this.transform.position);
    }

    public void PlayCaribouClock()
    {
        AudioManager.instance.PlayOneShot(FMODEvents.instance.CaribouClock, this.transform.position);
    }

    public void PlayCaribouBomb()
    {
        AudioManager.instance.PlayOneShot(FMODEvents.instance.CaribouBomb, this.transform.position);
    }
}
