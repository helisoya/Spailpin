using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

/// <summary>
/// Represents a dialog graph in Spailpin
/// </summary>
[CreateAssetMenu(menuName ="Spailpin/DialogGraph")]
public class DialogGraph : NodeGraph { 

    /// <summary>
    /// Gets the starting node from the graph
    /// </summary>
    /// <returns>The starting node if it exists</returns>
    public SpailpinNode GetStartNode(){
        foreach(SpailpinNode node in nodes){
            if(node.GetType() == typeof(StartNode)){ return node;}
        }
        return null;
    }
}