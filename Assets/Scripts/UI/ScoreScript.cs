using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ScoreScript : MonoBehaviour {
    public static int score = 0;
    public static float multiplierScore = 1.0f;
    public Text ScoreTextValue;
    private float MultiplierIncrease = 1.0f;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //score = score + 10;
        ScoreTextValue.text = score.ToString();
        if (ScoreScript.multiplierScore == 1.0f)
        {
            GameObject.FindGameObjectWithTag("MultiplierX1").GetComponent<Image>().fillAmount +=
                    MultiplierIncrease * Time.unscaledDeltaTime;
            GameObject.FindGameObjectWithTag("MultiplierX2").GetComponent<Image>().fillAmount = 0.0f;
            GameObject.FindGameObjectWithTag("MultiplierX3").GetComponent<Image>().fillAmount = 0.0f;
        
        }
        else if (ScoreScript.multiplierScore == 2.0f)
        {
            GameObject.FindGameObjectWithTag("MultiplierX1").GetComponent<Image>().fillAmount = 0.0f;
            GameObject.FindGameObjectWithTag("MultiplierX2").GetComponent<Image>().fillAmount +=
                MultiplierIncrease * Time.unscaledDeltaTime;
            GameObject.FindGameObjectWithTag("MultiplierX3").GetComponent<Image>().fillAmount = 0.0f;
        }
        else if (ScoreScript.multiplierScore == 2.0f)
        {
            GameObject.FindGameObjectWithTag("MultiplierX1").GetComponent<Image>().fillAmount = 0.0f;
            GameObject.FindGameObjectWithTag("MultiplierX2").GetComponent<Image>().fillAmount = 0.0f;
            GameObject.FindGameObjectWithTag("MultiplierX3").GetComponent<Image>().fillAmount +=
                MultiplierIncrease * Time.unscaledDeltaTime;
        }
    }
}
