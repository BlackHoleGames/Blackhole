using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ScoreScript : MonoBehaviour {
    public static int score = 0;
    public static float multiplierScore = 1.0f;
    public Text ScoreTextValue;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //score = score + 10;
        ScoreTextValue.text = score.ToString();
    }
}
