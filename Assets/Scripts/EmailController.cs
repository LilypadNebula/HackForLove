using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmailController : MonoBehaviour
{
    private Transform[] children = new Transform[4];
    private int selectedIndex = 0;
    public GameObject emailBody;
    private string[] emailBodies = new string[] {
        "I got a problem with the charity you picked. Trying to save orphans? I don't see how we can make money from saving orphans. Pick something else, " +
        "something cooler at least. Orphans aren't cool. Fire, fast cars, and chicks are cool. \n\n - Clark Fuller, Vice President of Sales & Marketing",
        "Look Quinn, I don't care that your hours are \"technically\" 9 - 5, if I say you have to get something done, you gotta get it done. That's how " +
        "bosses and employees work. If you spent more time working than you did sending emails about taking off early, maybe you'd have everything done " +
        "by now. \n\n - Clark Fuller, Vice President of Sales & Marketing",
        "I TOLD you not to email me, especially not my work email, asshole! Look, for the last time, the account number is " +
        "84324456, just send the funds there once the sales contracts are signed. It was not this complicated with the last guy. Embezzling is supposed to be " +
        "easy, jeezey. \n\n - Clark Fuller, Vice President of Sales & Marketing",
        "Look Marge I spent a lot of money to make sure I didn't get full custody, stop trying to get me to \"spend time with them\" and \"help with their expenses\" " +
        "and all that crap. I got better things to do. \n\n - Clark Fuller, Vice President of Sales & Marketing"
    };
    private float startTime = 0f;
    private float timer = 0f;
    private float holdTime = 3f;
    private bool pressed = false;
    // Start is called before the first frame update
    void Start()
    {
        var index = 0;
        foreach (Transform child in transform)
        {
            if (index > 3) break;
            children[index] = child;
            index++;
        }
        children[0].Find("Arrow").gameObject.SetActive(true);
        emailBody.GetComponent<TMPro.TextMeshProUGUI>().text = emailBodies[0];
    }

    void SelectEmail(int index)
    {
        children[selectedIndex].Find("Arrow").gameObject.SetActive(false);
        selectedIndex = index;
        children[selectedIndex].Find("Arrow").gameObject.SetActive(true);
        emailBody.GetComponent<TMPro.TextMeshProUGUI>().text = emailBodies[selectedIndex];

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            pressed = true;
            startTime = Time.time;
            timer = startTime;
        }

        if (Input.GetKey("space"))
        {
            timer += Time.deltaTime;
            if (timer > (startTime + holdTime))
            {
                pressed = false;
                startTime = 0f;
                timer = 0f;
                if (selectedIndex == 2)
                {
                    gameObject.GetComponentInParent<GameController>().wonGame = true;
                }
                gameObject.GetComponentInParent<GameController>().currentStage = GameController.Stage.FinishHack;
            }
        }

        if (Input.GetKeyUp("space") && pressed)
        {

            if (selectedIndex < 3)
            {
                SelectEmail(selectedIndex + 1);
            }
            else
            {
                SelectEmail(0);
            }
        }
    }
}
