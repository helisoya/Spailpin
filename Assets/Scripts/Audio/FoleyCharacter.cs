using UnityEngine;
using FMODUnity;

public class FoleyCharacter : MonoBehaviour
{
    [SerializeField] private EventReference FS_System;
    [SerializeField] private EventReference Character;
    private int currentParameter = 0;

    private void JunoFoley()
    {
        AudioManager.instance.PlayOneShotParameter(FS_System, this.transform.position, "Footstep", currentParameter);
        AudioManager.instance.PlayOneShot(Character, this.transform.position);

    }


    public void ChangeFloorType(Floor.Type type)
    {
        currentParameter = ((int)type) - 1;
        if (currentParameter == -1) currentParameter = 0;
        
    }
}
