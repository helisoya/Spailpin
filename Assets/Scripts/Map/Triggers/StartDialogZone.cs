using Unity.Cinemachine;
using UnityEngine;

/// <summary>
/// Represents a trigger that can start a dialog / cutscene
/// </summary>
public class StartDialogZone : TriggerZone
{
    [SerializeField] private DialogGraph graph;

    protected override void OnEnter()
    {
        if(graph){
            CutsceneManager.instance.ProcessCutscene(graph);
            this.enabled = false;
        }
    }
}
