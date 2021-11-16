using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GameController : MonoBehaviour
{
    private TMPro.TextMeshProUGUI textMesh;
    private enum Stage
    {
        Init,
        Start,
        HackingTime,
        End
    }
    private Stage currentStage = Stage.Init;
    private string initText = "Press SPACE to start";
    private string firstEmail = "Hey babe, I'm really sorry but I can't come over tonight. The Bosshole is making me stay late\n\n" +
        "Maybe we can hang out this weekend?\n\n" +
        "XOXO -Quinn";
    private string secondEmail = "OMG babe you're not gonna believe this!!! Mr Fuller just got walked out of the office by CorpSec, " +
        "I knew he was shady. Guess I can come over after all ;)\n\n" +
        "See you soon! -Quinn\n\n" +
        "PS - Did you have something to do with this?? Don't answer that XD";
    Dictionary<Stage, string> dictionaryThing = new Dictionary<Stage, string>();
    // Start is called before the first frame update
    void Start()
    {
        dictionaryThing.Add(Stage.Init, initText);
        dictionaryThing.Add(Stage.Start, firstEmail);
        dictionaryThing.Add(Stage.End, secondEmail);
        textMesh = GetComponent<TMPro.TextMeshProUGUI>();
        textMesh.text = dictionaryThing[currentStage];
        
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            currentStage = currentStage + 1;

        }

        if (currentStage == Stage.HackingTime)
        {
            gameObject.SetActive(false);
        }

    }
}
