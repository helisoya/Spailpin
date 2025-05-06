using UnityEngine;

public class AmbienceChange : MonoBehaviour
{
    [Header("Parameter Change")]

    [SerializeField] private string parameterName;
    
    [SerializeField] private float parameterValue;

    private void SetNewAmbience()
    {
        AudioManager.instance.SetAmbienceParameter(parameterName, parameterValue);
    }
}
