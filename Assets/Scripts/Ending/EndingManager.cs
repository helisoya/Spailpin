using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Handles the game's ending
/// </summary>
public class EndingManager : MonoBehaviour
{
    [Header("Infos")]
    [SerializeField] private string textID;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float endWaitTime = 5f;


    [Header("Components")]
    [SerializeField] private Fade fade;
    [SerializeField] private LocalizedText text;
    

    void Awake()
    {
        fade.ForceAlphaTo(1.0f);
        fade.FadeTo(0.0f);
    }

    void Start()
    {
        StartCoroutine(Routine_Ending());
    }

    /// <summary>
    /// Represents the routine for the ending
    /// </summary>
    IEnumerator Routine_Ending()
    {
        text.SetNewKey(textID);

        int charactersPerFrame = 1;

        TMP_Text txt = text.GetText();

        int runsThisFrame = 0;

        txt.ForceMeshUpdate(false);
        TMP_TextInfo inf = txt.textInfo;
        int vis = 0;
        int max = inf.characterCount;
        int cpf = charactersPerFrame;

        List<char> punctuation = new List<char>(new char[] { '.', ',', ';', '!', '?' });

        while (vis < max)
        {

            //reveal a certain number of characters per frame.
            while (runsThisFrame < charactersPerFrame)
            {
                vis++;
                txt.maxVisibleCharacters = vis;
                runsThisFrame++;
            }

            speed = punctuation.Contains(inf.characterInfo[vis - 1].character) ? 25 : 5;

            //wait for the next available revelation time.
            runsThisFrame = 0;
            yield return new WaitForSeconds(0.01f * speed);
        }

        yield return new WaitForSeconds(endWaitTime);
        fade.FadeTo(1.0f);
        yield return new WaitForEndOfFrame();
        while (fade.fading) yield return new WaitForEndOfFrame();

        SceneManager.LoadScene("MainMenu");
    }
}
