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
        Spawnpoint selected = spawnpoints[0];
        for(int i = 1; i< spawnpoints.Length;i++){
            if(spawnpoints[i].GetLinkedMap() == GameManager.instance.mapName){
                selected = spawnpoints[i];
                break;
            }
        }
        selected.GetRoom().Apply();
        Player.instance.SetPosition(selected.transform.position,selected.transform.rotation);
    }
}
