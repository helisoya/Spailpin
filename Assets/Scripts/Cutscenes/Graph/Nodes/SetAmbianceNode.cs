using System.Collections;
using UnityEngine;

/// <summary>
/// Node to set the ambiance
/// </summary>
[CreateNodeMenu("Event/Set ambiance")]
public class SetAmbianceNode : SpailpinNode
{
    [Input(connectionType = ConnectionType.Multiple)] public bool entry;
    [SerializeField] private int ambianceID;
    [Output(connectionType = ConnectionType.Override)] public bool exit;

    // Use this for initialization
    protected override void Init()
    {
        base.Init();
    }

    public override IEnumerator Apply()
    {
        AudioManager.instance.SetAmbience(ambianceID);
        yield return 0;
    }
}
