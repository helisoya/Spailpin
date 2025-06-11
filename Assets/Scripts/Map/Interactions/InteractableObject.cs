using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Represents an interactable object in Spailpin
/// </summary>
public class InteractableObject : MonoBehaviour
{
    [Header("Interaction")]
    public bool stopPlayerOnInterract = true;
    [SerializeField] protected Transform interactionObject;
    [SerializeField] protected DialogGraph linkedGraph;
    [SerializeField] protected UnityEvent onInterract;
    [SerializeField] private Renderer modelRenderer;
    private bool playerIsInside = false;

    void Awake()
    {
        if (!modelRenderer) return;

        foreach (Material mat in modelRenderer.materials)
        {
            mat.SetFloat("_OutlineWidth", 1.0f);
            mat.SetFloat("_OutlineWidthAffectedByViewDistance", 1);
            mat.SetFloat("_FarDistanceMaxWidth", 1);
        }
    }


    /// <summary>
    /// Changes if the player outline is active or not
    /// </summary>
    /// <param name="active">True if active</param>
    public void SetOutlineActive(bool active)
    {
        if (!modelRenderer) return;

        foreach (Material mat in modelRenderer.materials)
        {
            mat.SetFloat("_N_F_O", active ? 1 : 0);
            mat.SetShaderPassEnabled("SRPDefaultUnlit", active);
        }
    }

    /// <summary>
    /// Changes the player outline's color
    /// </summary>
    /// <param name="color">The outline's color</param>
    public void SetOutlineColor(Color color)
    {
        if (!modelRenderer) return;
        
        foreach (Material mat in modelRenderer.materials)
        {
            mat.SetColor("_OutlineColor", color);
        }
    }

    /// <summary>
    /// Changes if the interaction is "active" or not
    /// </summary>
    /// <param name="value">True if active</param>
    public void SetActive(bool value)
    {
        playerIsInside = value;
        if (value)
        {
            SetOutlineActive(Settings.instance.GetObjectOutlineActive());
            SetOutlineColor(Settings.instance.GetOutlineColor());
            GameGUI.instance.ShowInteractionIcon(interactionObject.position);
        }
        else
        {
            SetOutlineActive(false);
            GameGUI.instance.HideInteractionIcon();
        }
    }

    /// <summary>
    /// Interacts with the object
    /// </summary>
    public void Interract()
    {
        print("Interaction with : " + this.name);
        onInterract.Invoke();
        OnInterract();
    }

    /// <summary>
    /// Callback on interraction
    /// </summary>
    protected virtual void OnInterract()
    {
        // Do thing with the graph
        CutsceneManager.instance.ProcessCutscene(linkedGraph);
    }


    void Update()
    {
        if (playerIsInside) GameGUI.instance.ShowInteractionIcon(interactionObject.position);
        OnUpdate();
    }

    /// <summary>
    /// Called on Update
    /// </summary>
    public virtual void OnUpdate()
    {

    }

}
