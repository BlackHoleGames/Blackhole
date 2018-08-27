using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;
public class RumblePad : MonoBehaviour {
    public static int RumbleState = 1;
    private IEnumerator RumbleAction;
    private bool WaitToRumble = false;
    public float rumbleHit = 0.5f;
    public float rumbleHitIntensity = 1.0f;
    public float rumbleAlarm = 0.5f;
    public float rumbleAlarmIntensity = 0.5f;
    public float rumblePulse = 0.5f;
    public float rumblePulseIntensity = 2.0f;
    public float rumbleAsteroid = 0.5f;
    public float rumbleAsteroidIntensity = 0.5f;
    public float rumbleDeath = 3.0f;
    public float rumbleDeathIntensity = 3.0f;
    
    // Use this for initialization
    void Start () {
        RumbleState = 0;
    }
	
	// Update is called once per frame
	void Update () {
        if (!WaitToRumble) { 
            switch (RumbleState)
            {
                case 1: //Hit
                    RumbleAction = RumbleTimer(rumbleHit, rumbleHitIntensity);
                    StartCoroutine(RumbleAction);
                    break;
                case 2: //Alarm
                    RumbleAction = RumbleTimer(rumbleAlarm, rumbleAlarmIntensity);
                    StartCoroutine(RumbleAction);
                    break;
                case 3: //Pulse
                    RumbleAction = RumbleTimer(rumblePulse, rumblePulseIntensity);
                    StartCoroutine(RumbleAction);
                    break;
                case 4: //Asteroid Impact
                    RumbleAction = RumbleTimer(rumbleAsteroid, rumbleAsteroidIntensity);
                    StartCoroutine(RumbleAction);
                    break;
                case 5: //Death
                    RumbleAction = RumbleTimer(rumbleDeath, rumbleDeathIntensity);
                    StartCoroutine(RumbleAction);
                    break;
                default:
                    //GamePad.SetVibration(0, 0.0f, 0.0f);
                break;
            }
        }
	}
    IEnumerator RumbleTimer(float rumbleDuration, float rumbleIntensity)
    {
        WaitToRumble = true;
        GamePad.SetVibration(0, rumbleIntensity, rumbleIntensity);
        yield return new WaitForSeconds(rumbleDuration);
        GamePad.SetVibration(0, 0.0f, 0.0f);
        RumbleState = 0;
        WaitToRumble = false;
    }
}
