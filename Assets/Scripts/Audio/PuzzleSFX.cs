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

    public void PlayAudreyPuzzle()
    {
        AudioManager.instance.PlayOneShot(FMODEvents.instance.AudreyPuzzle, this.transform.position);
    }

    public void PlayGreenHousePuzzle()
    {
        AudioManager.instance.PlayOneShot(FMODEvents.instance.PuzzleGreenHouse, this.transform.position);
    }

    public void PlayPuzzleQuit()
    {
        AudioManager.instance.PlayOneShot(FMODEvents.instance.PuzzleQuit, this.transform.position);
    }
}
