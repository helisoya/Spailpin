using UnityEngine;

public class PuzzleSFX : MonoBehaviour
{
    public void PlayPuzzleSuccess()
    {
        AudioManager.instance.PlayOneShot(FMODEvents.instance.PuzzleSuccess, this.transform.position);
    }

    public void PlayPuzzleFailed()
    {
        AudioManager.instance.PlayOneShot(FMODEvents.instance.PuzzleFailed, this.transform.position);
    }
}
