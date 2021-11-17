using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Text.RegularExpressions;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject mainText;
    public GameObject emails;
    private TMPro.TextMeshProUGUI textMesh;
    public enum Stage
    {
        Init,
        Start,
        FirstEmail,
        Angy,
        ShowHackingText,
        HackingTime,
        DuringHacking,
        FinishHack,
        AfterHack,
        SuccessEmail,
        EndSuccess,
        FailedEmail,
        EndFailed
    }
    public Stage currentStage = Stage.Init;
    private string initText = "Press SPACE to start";
    private string startText = "You are Maxi, one of the best hackers around. You have a wonderful girlfriend named Quinn, who is unfortunately stuck in a dead end job working for a jerk. \n\n" +
        "To play, just press SPACE to advance, and when you can pick between different options, press SPACE to change your selected option, and hold SPACE for a few seconds to select it.\n\n" +
        "Oh, you're getting an email! Let's see what it is :D";
    private string angyText = "...WHAT??? That jerk Fuller is keeping her late again?!?!\n\n...Not this time. He's messed with her too much to keep getting away with it." +
        "\n\nLet's see if his emails have anything that might help us get him...out of the picture. \n\n PRESS SPACE TO INITIATE EASYHACK PROTOCOL";
    private string initHackText = "NOW HACKING\n\nTeraflopping ethernets...\n\nOverflowing buffer tri-states...\n\nChecking for passwords under keyboards...\n\nHACKING SUCCESSFUL!\n\n PRESS SPACE TO ACCESS EMAIL";
    private string firstEmail = "NEW EMAIL FROM <3 Quinn <3: \n\nHey babe, I'm really sorry but I can't come over tonight. The Bosshole is making me stay late\n\n" +
        "Maybe we can hang out this weekend?\n\n" +
        "XOXO - Quinn";
    private string afterHackText = "HACKING SUCCESSFUL\n\nEVIDENCE SENT\n\nCool, hopefully that should expose this guy for the trash heap he is :D\n\n Oh hey, another email!";
    private string successEmail = "NEW EMAIL FROM <3 Quinn <3: \n\nOMG babe you're not gonna believe this!!! Mr Fuller just got walked out of the office by a bunch of tough looking dudes, " +
        "I knew he was shady. Guess I can come over after all ;)\n\n" +
        "See you soon! - Quinn\n\n" +
        "PS - Did you have something to do with this?? Don't answer that XD";
    private string failedEmail = "NEW EMAIL FROM <3 Quinn <3: \n\n Ugh, just confirmed it, whole department has to stay late tonight T_T\n\n" +
        "I'll text you when I'm out, and we can figure out another time to hang. Can't wait to see you~\n\n" +
        "XOXO - Quinn";
    private string endSuccessText = "CONGRATS!!!!\n\n You have freed your girlfriend from the perils of capitalism, for tonight at least. Time to relax and be gay.\n\n" +
        "Thanks for playing!!!\n\nPress SPACE to play again";
    private string endFailureText = ":( Looks like that wasn't the right email. Luckily, Maxi is an amazing hacker and can just go again!\n\nPRESS SPACE TO INITIATE EASYHACK PROTOCOL";
    public bool wonGame = false;
    Dictionary<Stage, string> dictionaryThing = new Dictionary<Stage, string>();
    private readonly Regex whitespace = new Regex(@"\s+");
    // Start is called before the first frame update
    void Start()
    {
        DOTween.Init();
        DOTween.defaultEaseType = Ease.Linear;
        dictionaryThing.Add(Stage.Init, initText);
        dictionaryThing.Add(Stage.Start, startText);
        dictionaryThing.Add(Stage.FirstEmail, firstEmail);
        dictionaryThing.Add(Stage.Angy, angyText);
        dictionaryThing.Add(Stage.ShowHackingText, initHackText);
        dictionaryThing.Add(Stage.AfterHack, afterHackText);
        dictionaryThing.Add(Stage.SuccessEmail, successEmail);
        dictionaryThing.Add(Stage.FailedEmail, failedEmail);
        dictionaryThing.Add(Stage.EndSuccess, endSuccessText);
        dictionaryThing.Add(Stage.EndFailed, endFailureText);
        textMesh = mainText.GetComponent<TMPro.TextMeshProUGUI>();
        DOTween.To(() => textMesh.text, x => textMesh.text = x, dictionaryThing[currentStage], GetDuration(dictionaryThing[currentStage]));
    }

    void ChangeText()
    {
        if (currentStage == Stage.Angy)
        {
            gameObject.GetComponentInParent<MusicController>().PlayAngy();
        }

        if (currentStage == Stage.AfterHack)
        {
            gameObject.GetComponentInParent<MusicController>().PlayMain();
        }
        DOTween.KillAll();
        DOTween.To(() => textMesh.text, x => textMesh.text = x, "", 1);
        DOTween.To(() => textMesh.text, x => textMesh.text = x, dictionaryThing[currentStage], GetDuration(dictionaryThing[currentStage]));
    }

    float GetDuration(string text)
    {
        var chars = whitespace.Replace(text, "");
        return chars.Length * 0.1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space") && mainText.activeSelf)
        {
            if (currentStage == Stage.AfterHack)
            {
                if (wonGame)
                {
                    currentStage = Stage.SuccessEmail;
                }
                else
                {
                    currentStage = Stage.FailedEmail;
                }
            }
            else if (currentStage == Stage.EndSuccess)
            {
                currentStage = Stage.Start;
            }
            else if (currentStage == Stage.EndFailed)
            {
                currentStage = Stage.ShowHackingText;
            }
            else
            {
                currentStage++;
                

            }
            if (dictionaryThing.ContainsKey(currentStage))
            {
                ChangeText();
            }

        }

        
        

        if (currentStage == Stage.HackingTime)
        {
            mainText.SetActive(false);
            emails.SetActive(true);
            currentStage = Stage.DuringHacking;
        }

        if (currentStage == Stage.FinishHack)
        {
            emails.SetActive(false);
            mainText.SetActive(true);
            currentStage = Stage.AfterHack;
            ChangeText();
        }

    }
}
