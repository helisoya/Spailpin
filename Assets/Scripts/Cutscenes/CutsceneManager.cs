using System;
using System.Collections;
using System.Collections.Generic;
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
    public bool inParrallelCutscene {get{return currentCutsceneIsParrallel;}}

    private bool userSubmit;
    private bool currentCutsceneIsParrallel = false;

    private Dictionary<string, GameObject> objects;

    /// <summary>
    /// Sets the user submit tag
    /// </summary>
    public void UserSubmit()
    {
        userSubmit = true;
    }

    /// <summary>
    /// Return and consume the user submit tag
    /// </summary>
    /// <returns>True if the submit tag was set</returns>
    public bool ConsumeUserSubmit(){
        bool value = userSubmit;
        userSubmit = false;
        return value;
    }

    /// <summary>
    /// Registers an object
    /// </summary>
    /// <param name="id">Its id</param>
    /// <param name="obj">The object</param>
    public void RegisterObject(string id, GameObject obj)
    {
        objects.TryAdd(id, obj);
    }

    /// <summary>
    /// Changes if an object is active or not
    /// </summary>
    /// <param name="id">Its id</param>
    /// <param name="value">True if it should be active</param>
    public void SetObjectActive(string id, bool value)
    {
        if (objects.TryGetValue(id, out GameObject obj))
        {
            obj.SetActive(value);
        }
    }

    void Awake()
    {
        instance = this;
        objects = new Dictionary<string, GameObject>();
    }

    /// <summary>
    /// Stop processing a cutscene
    /// </summary>
    public void StopProcessing(){
        if(processingCutscene != null){
            StopCoroutine(processingCutscene);
            processingCutscene = null;
        }
    }

    /// <summary>
    /// Start processing a cutscene
    /// </summary>
    /// <param name="graph">The cutscene's graph</param>
    /// <param name="overridePreviousCutscene">True if the previous cutscene should be overriden</param>
    public void ProcessCutscene(DialogGraph graph, bool overridePreviousCutscene = true){
        if (processingCutscene != null && !overridePreviousCutscene) return;

        if (processingCutscene != null)
        {
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
        yield return new WaitForEndOfFrame();
        currentCutsceneIsParrallel = graph.parrallelCutscene;
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
