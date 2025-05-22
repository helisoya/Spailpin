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
}
