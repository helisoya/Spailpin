using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using XNode;

/// <summary>
/// Handles the game's cutscenes
/// </summary>
public class CutsceneManager : MonoBehaviour
{
    public static CutsceneManager instance;
    private Coroutine processingCutscene = null;
    public bool inCutscene {get{return processingCutscene != null;}}

    void Awake()
    {
        instance = this;   
    }

    public void ProcessCutscene(DialogGraph graph){
        if(processingCutscene != null){
            StopCoroutine(processingCutscene);
        }
        processingCutscene = StartCoroutine(Routine_ProcessingCutscene(graph));
    }


    /// <summary>
    /// Routine for processing a dialog graph
    /// </summary>
    /// <param name="graph">The graph</param>
    /// <returns>IEnumerator</returns>
    private IEnumerator Routine_ProcessingCutscene(DialogGraph graph){

        SpailpinNode currentNode = graph.GetStartNode();
        int result = 0;
        NodePort port;
        while(currentNode != null){
            
            yield return Run<int>(currentNode.Apply(), (output) => result = output);

            // Next node
            if(currentNode.Outputs.Count() > result){
                port = currentNode.Outputs.ElementAt(result);
                if(port.IsConnected) currentNode = (SpailpinNode)port.Connection.node;
                else currentNode = null;
            }else{
                currentNode = null;
            }
        }


        processingCutscene = null;
        yield return null;
    }


    /// <summary>
    /// Runs a Coroutine with a return value
    /// </summary>
    /// <typeparam name="T">The return value's type</typeparam>
    /// <param name="target">The target Coroutine</param>
    /// <param name="output">The output action</param>
    /// <returns>IEnumerator</returns>
    public static IEnumerator Run<T>(IEnumerator target, Action<T> output)
    {
        object result = null;
        while (target.MoveNext())
        {
            result = target.Current;
            yield return result;
        }
        output((T)result);
    }
}
