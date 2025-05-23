using System.Collections;
using UnityEngine;

/// <summary>
/// Node to play a room theme
/// </summary>
[CreateNodeMenu("Event/Play Room Theme")]
public class PlayRoomThemeNode : SpailpinNode
{
    [Input(connectionType = ConnectionType.Multiple)] public bool entry;
    [SerializeField] private string roomID;
    [Output(connectionType = ConnectionType.Override)] public bool exit;

	// Use this for initialization
	protected override void Init() {
		base.Init();
	}

    public override IEnumerator Apply()
    {
        AudioManager.instance.PlayRoomTheme(roomID);
        yield return 0;
    }
}
