using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GTLManager : MonoBehaviour {
    private float MultiplierIncrease = 0.5f;
	
	// Update is called once per frame
	void Update () {
        if (ScoreScript.multiplierScore == 1.0f)
        {
            GameObject.FindGameObjectWithTag("MultiplierX1").GetComponent<Image>().fillAmount +=
                    MultiplierIncrease * Time.unscaledDeltaTime;
        }else if (ScoreScript.multiplierScore == 2.0f)
        {
            GameObject.FindGameObjectWithTag("MultiplierX2").GetComponent<Image>().fillAmount +=
                    MultiplierIncrease * Time.unscaledDeltaTime;
        }
        else if (ScoreScript.multiplierScore == 3.0f)
        {
            GameObject.FindGameObjectWithTag("MultiplierX3").GetComponent<Image>().fillAmount +=
                    MultiplierIncrease * Time.unscaledDeltaTime;
        }
    }
}
