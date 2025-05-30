using System.Collections;
using UnityEngine;
using XNode;

/// <summary>
/// Represents a node that fades the screen
/// </summary>
[CreateNodeMenu("Event/Fade")]
public class FadeNode : SpailpinNode
{

    [Input(connectionType = ConnectionType.Multiple)] public bool entry;
    [SerializeField] private float target;
    [SerializeField] private bool waitForEnd;
    [Output(connectionType = ConnectionType.Override)] public bool exit;

    // Use this for initialization
    protected override void Init()
    {
        base.Init();
    }

    public override IEnumerator Apply()
    {
        GameGUI.instance.FadeTo(target);
        if (waitForEnd)
        {
            yield return new WaitForEndOfFrame();
            while (GameGUI.instance.fading)
            {
                yield return new WaitForEndOfFrame();
            }
        }
        yield return 0;
    }
}