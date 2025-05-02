using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents the game's items / variable
/// </summary>
[CreateAssetMenu(fileName = "GameItems", menuName = "Spailpin/GameItems")]
public class GameItems : ScriptableObject
{
    public List<Item> items;
    
    /// <summary>
    /// Represents an 'item'
    /// </summary>
    [System.Serializable]
    public class Item{
        public string ID;
        public int value;


        public override bool Equals(object obj)
        {
            if(obj.GetType() != typeof(Item)) return false;
            return this.ID.Equals(((Item)obj).ID);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
