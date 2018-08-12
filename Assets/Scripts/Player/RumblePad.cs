using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;
public class RumblePad : MonoBehaviour {
    public int RumbleState = 1;
    private IEnumerator RumbleAction;
    private bool WaitToRumble = false;
    // Use this for initialization
    void Start () {
        RumbleState = 0;
    }
	
	// Update is called once per frame
	void Update () {
        //TESTINGif (!WaitToRumble) { 
        //TESTING    switch (RumbleState)
        //TESTING    {
        //TESTING        case 1: //Hit
        //TESTING            RumbleAction = RumbleTimer(1.0f);
        //TESTING            StartCoroutine(RumbleAction);
        //TESTING            break;
        //TESTING        case 2: //Alarm
        //TESTING            RumbleAction = RumbleTimer(0.001f);
        //TESTING            StartCoroutine(RumbleAction);
        //TESTING            break;
        //TESTING        case 3: //Pulse
        //TESTING            RumbleAction = RumbleTimer(0.5f);
        //TESTING            StartCoroutine(RumbleAction);
        //TESTING            break;
        //TESTING        case 4: //Asteroid Impact
        //TESTING            RumbleAction = RumbleTimer(1.0f);
        //TESTING            StartCoroutine(RumbleAction);
        //TESTING            break;
        //TESTING        case 5: //Death
        //TESTING            RumbleAction = RumbleTimer(4.0f);
        //TESTING            StartCoroutine(RumbleAction);
        //TESTING            break;
        //TESTING        default:
        //TESTING            GamePad.SetVibration(0, 0.0f, 0.0f);
        //TESTING        break;
        //TESTING    }
        //TESTING}
	}
    IEnumerator RumbleTimer(float rumbleDuration)
    {
        WaitToRumble = true;
        GamePad.SetVibration(0, 0.001f, 0.001f);
        yield return new WaitForSeconds(rumbleDuration);
        GamePad.SetVibration(0, 0.0f, 0.0f);
        //if(!RumbleState && !AlarmActivated)
            RumbleState = 0;
    }
}
