using System.IO;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents the save manager
/// </summary>
public class SaveManager
{
    public SaveFile saveFile {get; private set;}
    private string saveFilePath = FileManager.savPath + "save.sav";
    public bool saveFileExists {get{return File.Exists(saveFilePath);}}

    public SaveManager(GameItems items){
        saveFile = new SaveFile();
        saveFile.items = new List<GameItems.Item>();
        foreach(GameItems.Item item in items.items){
            saveFile.items.Add(new()
            {
                ID = item.ID,
                value = item.value
            });
        }
    }

    /// <summary>
    /// Gets a variable's value
    /// </summary>
    /// <param name="ID">The variable's ID</param>
    /// <returns>The variable's value. -1 if not found</returns>
    public int GetVariable(string ID){
        foreach(GameItems.Item item in saveFile.items){
            if(item.ID == ID){return item.value;}
        }

        return -1;
    }

    /// <summary>
    /// Sets a variable's value
    /// </summary>
    /// <param name="ID">The variable's ID</param>
    /// <param name="value">The variable's new value</param>
    public void SetVariable(string ID, int value){
        foreach(GameItems.Item item in saveFile.items){
            if(item.ID == ID){item.value = value; return;}
        }
    }


    /// <summary>
    /// Saves the game
    /// </summary>
    public void SaveGame(){
        saveFile.currentRoom = Player.instance.CurrentRoom;
        saveFile.playerPosition = Player.instance.position;
        saveFile.playerRotation = Player.instance.rotation;
        FileManager.SaveJSON(saveFilePath, saveFile);
    }

    /// <summary>
    /// Loads the game
    /// </summary>
    public void LoadGame(){
        if(saveFileExists){
            saveFile = FileManager.LoadJSON<SaveFile>(saveFilePath);
            GameManager.instance.loadingSave = true;
            GameManager.instance.ChangeScene(saveFile.mapName);
        }
    }

}
