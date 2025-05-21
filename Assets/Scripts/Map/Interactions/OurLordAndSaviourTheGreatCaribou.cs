using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

/// <summary>
/// And on the third day, our lord descended upon the land
/// And he said "Thou shall meet a timely death, Pinpin"
/// And Pinpin was cleansed by holy fire
/// Hall hail the Great Caribou, saviour of the universe
/// </summary>
public class OurLordAndSaviourTheGreatCaribou : InteractableObject
{
    [SerializeField] private DialogGraph angryCaribouNoises;
    [SerializeField] private UnityEvent onStart;
    [SerializeField] private UnityEvent onLand;
    [SerializeField] private float hisImpressiveSpeed = 2.0f;
    [SerializeField] private float andTheWorldWillBePurgedIn = 10f;
    float whereShouldOurLeaderLand = 0;
    private bool hasOurSaviourLanded = false;
    private int angerStateOfOurLord;
    // 0 = Our Lord is happy
    // 1 = Our lord reprimands you
    // 2 = Thou shall die soon, Pinpin
    // 3 = Thou are cleansed, Pinpin


    /// <summary>
    /// His descent was gracious and divine
    /// Spailpins saw him from a distance, and rejoiced
    /// </summary>
    void Start()
    {
        onStart.Invoke();
        angerStateOfOurLord = 0;
    }

    /// <summary>
    /// And PinPin prayed the holy Caribou for three days
    /// The last day, he camed
    /// </summary>
    /// <param name="prayerSite">Somewhere good, so that our lord may be happy</param>
    public void PrayThatOurLordLandsHere(float prayerSite)
    {
        whereShouldOurLeaderLand = prayerSite;
    }

    /// <summary>
    /// His eyes were filled with fury
    /// He had just seen Lord Elk in the Pinpin Mobile
    /// </summary>
    protected override void OnInterract()
    {
        if (angerStateOfOurLord == 0)
        {
            angerStateOfOurLord = 1;
            CutsceneManager.instance.ProcessCutscene(linkedGraph);
        }
        else if (angerStateOfOurLord == 2)
        {
            CutsceneManager.instance.ProcessCutscene(angryCaribouNoises);
        }

    }

    /// <summary>
    /// His judgement was swift and good
    /// </summary>
    public override void OnUpdate()
    {
        transform.LookAt(Camera.main.transform.position);
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);

        if (!hasOurSaviourLanded)
        {
            transform.position += Vector3.down * hisImpressiveSpeed * Time.deltaTime;
            if (transform.position.y <= whereShouldOurLeaderLand)
            {
                onLand.Invoke();
                hasOurSaviourLanded = true;
            }
        }

        if (angerStateOfOurLord == 1 && !CutsceneManager.instance.inCutscene)
        {
            angerStateOfOurLord = 2;
        }
        else if (angerStateOfOurLord == 2)
        {
            andTheWorldWillBePurgedIn -= Time.deltaTime;
            if (andTheWorldWillBePurgedIn <= 0.0f)
            {
                angerStateOfOurLord = 3;
                NativeWinAlert.Error("Too many caribous detected. Aborting...","Critical Caribou Error");
                Application.Quit();
            }
        }
    }
}
