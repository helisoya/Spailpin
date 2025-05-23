using System.Collections;
using UnityEngine;

/// <summary>
/// Node to play a sound effect
/// </summary>
[CreateNodeMenu("Event/Play SFX")]
public class PlaySFXNode : SpailpinNode
{
    [Input(connectionType = ConnectionType.Multiple)] public bool entry;
    [SerializeField] private string sfxID;
    [Output(connectionType = ConnectionType.Override)] public bool exit;

	// Use this for initialization
	protected override void Init() {
		base.Init();
	}

    public override IEnumerator Apply()
    {
        AudioManager.instance.PlaySFX(sfxID);
        yield return 0;
    }
}
