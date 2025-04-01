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



    void Start()
    {
        FindPlayerSpawnPoint();
        GameManager.instance.mapName = ID;
    }

    /// <summary>
    /// Finds the current player's spawnpoint
    /// </summary>
    private void FindPlayerSpawnPoint(){
        Spawnpoint defaultSpawn = null;
        Spawnpoint selected = null;
        for(int i = 0; i< spawnpoints.Length;i++){
            if(spawnpoints[i].isDefaultSpawnpoint) defaultSpawn = spawnpoints[i];
            else if(spawnpoints[i].linkedMap == GameManager.instance.mapName){
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
}
