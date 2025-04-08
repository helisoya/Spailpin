using UnityEngine;

[System.Serializable]
public class SaveFile
{
    public string mapName;
    public Vector3 playerPosition;
    public Vector3 playerRotation;
    public int currentRoom;

    public SaveFile(){
        mapName = "Test";
        playerPosition = new Vector3(0,0,0);
        playerRotation = new Vector3(0,0,0);
        currentRoom = 0;
    }
}
