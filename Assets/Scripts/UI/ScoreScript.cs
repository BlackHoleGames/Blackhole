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
//COMENTED FOR LASTUI        if (ScoreScript.multiplierScore == 1.0f)
//COMENTED FOR LASTUI        {
//COMENTED FOR LASTUI            GameObject.FindGameObjectWithTag("MultiplierX1").GetComponent<Image>().fillAmount +=
//COMENTED FOR LASTUI                    MultiplierIncrease * Time.unscaledDeltaTime;
//COMENTED FOR LASTUI            GameObject.FindGameObjectWithTag("MultiplierX2").GetComponent<Image>().fillAmount = 0.0f;
//COMENTED FOR LASTUI            GameObject.FindGameObjectWithTag("MultiplierX3").GetComponent<Image>().fillAmount = 0.0f;
//COMENTED FOR LASTUI        
//COMENTED FOR LASTUI        }
//COMENTED FOR LASTUI        else if (ScoreScript.multiplierScore == 2.0f)
//COMENTED FOR LASTUI        {
//COMENTED FOR LASTUI            GameObject.FindGameObjectWithTag("MultiplierX1").GetComponent<Image>().fillAmount = 0.0f;
//COMENTED FOR LASTUI            GameObject.FindGameObjectWithTag("MultiplierX2").GetComponent<Image>().fillAmount +=
//COMENTED FOR LASTUI                MultiplierIncrease * Time.unscaledDeltaTime;
//COMENTED FOR LASTUI            GameObject.FindGameObjectWithTag("MultiplierX3").GetComponent<Image>().fillAmount = 0.0f;
//COMENTED FOR LASTUI        }
//COMENTED FOR LASTUI        else if (ScoreScript.multiplierScore == 3.0f)
//COMENTED FOR LASTUI        {
//COMENTED FOR LASTUI            GameObject.FindGameObjectWithTag("MultiplierX1").GetComponent<Image>().fillAmount = 0.0f;
//COMENTED FOR LASTUI            GameObject.FindGameObjectWithTag("MultiplierX2").GetComponent<Image>().fillAmount = 0.0f;
//COMENTED FOR LASTUI            GameObject.FindGameObjectWithTag("MultiplierX3").GetComponent<Image>().fillAmount +=
//COMENTED FOR LASTUI                MultiplierIncrease * Time.unscaledDeltaTime;
//COMENTED FOR LASTUI        }
    }
}
