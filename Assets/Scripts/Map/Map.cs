using Unity.Cinemachine;
using UnityEngine;

/// <summary>
/// Represents a map in Spailpin
/// </summary>
public class Map : MonoBehaviour
{
    [Header("Map")]
    [SerializeField] private string ID;
    [SerializeField] private Room[] rooms;
    [SerializeField] private Spawnpoint[] spawnpoints;
    private bool isUpdatingCamera;


    void Start()
    {

        if(GameManager.instance.loadingSave){
            SaveFile saveFile = GameManager.instance.GetSaveManager().saveFile;
            Player.instance.SetPosition(saveFile.playerPosition,Quaternion.Euler(saveFile.playerRotation));

            foreach(Room room in rooms){
                if(room.GetID() == saveFile.currentRoom){
                    room.Apply();
                    break;
                }
            }
            GameManager.instance.loadingSave = false;
        }else{
            FindPlayerSpawnPoint();
            GameManager.instance.mapName = ID;
        }

        isUpdatingCamera = true;
        CinemachineCore.UniformDeltaTimeOverride = 500;
    }

    /// <summary>
    /// Finds the current player's spawnpoint
    /// </summary>
    private void FindPlayerSpawnPoint(){
        Spawnpoint defaultSpawn = null;
        Spawnpoint selected = null;
        for(int i = 0; i< spawnpoints.Length;i++){
            if(spawnpoints[i].isDefaultSpawnpoint) defaultSpawn = spawnpoints[i];
            else if(spawnpoints[i].linkedMap.Equals(GameManager.instance.mapName)){
                selected = spawnpoints[i];
                break;
            }
        }
        if(selected == null && defaultSpawn != null) selected = defaultSpawn;

        if(selected){
            selected.linkedRoom.Apply();
            Player.instance.SetPosition(selected.transform.position,selected.transform.rotation);
        }else{
            Debug.LogError("No valid spawnpoint found. Did you forget to add a default ");
        }
    }

    void LateUpdate()
    {
        if(isUpdatingCamera){
            isUpdatingCamera = false;
            CinemachineCore.UniformDeltaTimeOverride = -1;
        }
    }
}
