using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputNameScore : MonoBehaviour {
    public Text timerText;
    public Text initialsText;
    private int timeRemaining=9;
    private IEnumerator Timercoroutine;
    private IEnumerator Initialcoroutine;
    private string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    private string letter = "Z";
    private float axisX;
    private float RT;
    private string initialAux="";
    private bool initialAuxDone = false;
    private bool isDone = false;
    public bool WaitingForName = false;
    // Use this for initialization
    void Start () {
        Timercoroutine = TimeRemaining(1.0f);
        StartCoroutine(Timercoroutine);
        Initialcoroutine = LetterRemaining(0.2f);
        StartCoroutine(Initialcoroutine);
        isDone = false;
    }

    // Update is called once per frame
    void Update () {
        axisX = Input.GetAxis("Horizontal");
        RT = Input.GetAxis("RT1");
        if (!isDone)
        {

            if (Input.GetButtonDown("AButton"))
            {
                initialAuxDone = true;
                initialAux = initialAux + letter;
                if (initialAux.Length == 3) isDone = true;
            }
            if (Input.GetButtonDown("BButton"))
            {
                initialAuxDone = false;
                switch (initialAux.Length)
                {
                    case 0:
                        initialAux = letter;
                        break;
                    case 1:
                        initialAux = letter;
                        break;
                    case 2:
                        initialAux = initialAux.Substring(initialAux.Length - 1) + letter;
                        break;
                }
            }
        }
    }
    void alphabetIncreaseSearch()
    {
        int index = alphabet.IndexOf(letter);
        System.Console.WriteLine(index);
        if (index == 25) letter = "A";
        else letter = alphabet.Substring(index+1, 1);
        //        initialAux = letter;
        initialsText.text = initialAux + letter;
    }
    void alphabetDecreaseSearch()
    {
        int index = alphabet.IndexOf(letter);
        System.Console.WriteLine(index);
        if (index == 0) letter = "Z";
        else letter = alphabet.Substring(index-1, 1);
        //initials.text = letter;
        initialsText.text = initialAux + letter;
    }

    private IEnumerator TimeRemaining(float seconds)
    {
        while (timeRemaining > 0 && !isDone) { 
            yield return new WaitForSeconds(seconds);
            timeRemaining--;
            timerText.text = timeRemaining.ToString();
        }
        initialsText.text = initialAux;
        SaveGameStatsScript.GameStats.SetScore(initialsText.text, SaveGameStatsScript.GameStats.playerScore.ToString());
        WaitingForName = true;
        FadeOutInputName();
        yield return new WaitForSeconds(seconds);
    }
    private IEnumerator LetterRemaining(float seconds)
    {
        while (timeRemaining > 0 && !isDone)
        {
            yield return new WaitForSeconds(seconds);
            if (axisX > 0)
            {
                alphabetIncreaseSearch();
            }
            else if (axisX < 0)
            {
                alphabetDecreaseSearch();
            }
            else if (RT > 0)
            {
                alphabetDecreaseSearch();
            }
        }

    }
    void FadeOutInputName()
    {
        timerText.CrossFadeAlpha(0.0f, 1.0f, false);
        initialsText.CrossFadeAlpha(0.0f, 1.0f, false);
    }
}
