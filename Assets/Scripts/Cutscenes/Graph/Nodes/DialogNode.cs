using System.Collections;
using UnityEngine;
using XNode;

/// <summary>
/// Represents a dialog node
/// </summary>
[CreateNodeMenu("Event/Dialog")]
public class DialogNode : SpailpinNode {

    [Input(connectionType = ConnectionType.Override)] public bool entry;
    [Input(connectionType = ConnectionType.Override)] public string dialogID;
    [Output(connectionType = ConnectionType.Override)] public bool exit;

	// Use this for initialization
	protected override void Init() {
		base.Init();
	}

    public override IEnumerator Apply()
    {
        GameGUI.instance.ShowDialog(dialogID);

        // Dialog appears
        while(GameGUI.instance.showingDialog){
            if(CutsceneManager.instance.ConsumeUserSubmit()) GameGUI.instance.SetSkipDialogTag();
            yield return new WaitForEndOfFrame();
        }

        // Wait for user input
        while(!CutsceneManager.instance.ConsumeUserSubmit()){
            yield return new WaitForEndOfFrame();
        }
        GameGUI.instance.SetDialogOpen(false);
        yield return 0;
    }
}