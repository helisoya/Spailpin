using Unity.Cinemachine;
using UnityEngine;

/// <summary>
/// Represents a trigger that can start a dialog / cutscene
/// </summary>
public class StartDialogZone : TriggerZone
{
    [SerializeField] private DialogGraph graph;
    [SerializeField] private bool stopPlayer;

    protected override void OnEnter()
    {
        if (graph && Map.instance.started)
        {
            if(stopPlayer) Player.instance.SetMovementVector(Vector2.zero);
            CutsceneManager.instance.ProcessCutscene(graph,false);
            this.enabled = false;
        }
    }
}
