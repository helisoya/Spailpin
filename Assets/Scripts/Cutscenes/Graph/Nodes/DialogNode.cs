using System.Collections;
using UnityEngine;
using XNode;

/// <summary>
/// Represents a dialog node
/// </summary>
[CreateNodeMenu("Event/Dialog")]
public class DialogNode : SpailpinNode {

    [Input] public bool entry;
    [Input] public string dialogID;
    [Output] public bool exit;

	// Use this for initialization
	protected override void Init() {
		base.Init();
	}

    public override IEnumerator Apply()
    {
        Debug.Log("Dialog : "+Locals.GetLocal(dialogID));
        yield return 0;
    }
}