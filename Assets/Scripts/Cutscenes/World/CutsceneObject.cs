using UnityEngine;

/// <summary>
/// Represents an object that can manipulated in a cutscene
/// </summary>
public class CutsceneObject : MonoBehaviour
{
    [SerializeField] private string id;
    void Start()
    {
        CutsceneManager.instance.RegisterObject(id, gameObject);
    }
}
