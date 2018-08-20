using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GTLManager : MonoBehaviour {
    private float MultiplierIncrease = 1.0f;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (ScoreScript.multiplierScore == 1.0f)
        {
            GameObject.FindGameObjectWithTag("MultiplierX1").GetComponent<Image>().fillAmount +=
                    MultiplierIncrease * Time.unscaledDeltaTime;
        }else if (ScoreScript.multiplierScore == 2.0f)
        {

        }
        else if (ScoreScript.multiplierScore == 2.0f)
        {

        }
//        if (GameObject.FindGameObjectWithTag("Player").GetComponent<SwitchablePlayerController>().activateBomb)
//        {
//            GameObject.FindGameObjectWithTag("Player").GetComponent<SwitchablePlayerController>().activateBomb = false;
//        }
    }
}
