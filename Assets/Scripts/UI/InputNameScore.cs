using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputNameScore : MonoBehaviour {
    public Text time;
    public Text initials;
    private int timeRemaining=59;
    private IEnumerator coroutine;
    private string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    private string letter = "Z";
    // Use this for initialization
    void Start () {
        coroutine = TimeRemaining(1.0f);
        StartCoroutine(coroutine);
    }

    // Update is called once per frame
    void Update () {
        float axisX = Input.GetAxis("Horizontal");        
        if (axisX > 0)
        {
            alphabetIncreaseSearch();
        }else if (axisX < 0)
        {
            alphabetDecreaseSearch();
        }        
    }
    void alphabetIncreaseSearch()
    {
        int index = alphabet.IndexOf(letter);
        System.Console.WriteLine(index);
        if (index == 26) letter = "A";
        else letter = alphabet.Substring(index, 1);
    }
    void alphabetDecreaseSearch()
    {
        int index = alphabet.IndexOf(letter);
        System.Console.WriteLine(index);
        if (index == 0) letter = "Z";
        else letter = alphabet.Substring(index-1, 1);
    }

    private IEnumerator TimeRemaining(float seconds)
    {
        while (timeRemaining > 0) { 
            yield return new WaitForSeconds(seconds);
            timeRemaining--;
            time.text = timeRemaining.ToString();
        }
    }
}
