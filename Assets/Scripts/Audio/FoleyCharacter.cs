using UnityEngine;
using FMODUnity;

public class FoleyCharacter : MonoBehaviour
{
    [SerializeField] private EventReference FS_System;
    [SerializeField] private EventReference Character;
    private void JunoFoley()
    {
        AudioManager.instance.PlayOneShot(FS_System, this.transform.position);
        AudioManager.instance.PlayOneShot(Character, this.transform.position);

    }
}
