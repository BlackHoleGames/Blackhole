
using UnityEngine;

public class TimeManager : MonoBehaviour {

    public float slomoTime = 0.05f;
    public float actualmaxspeed = 2.0f;
    public float slomoDuration = 2.0f;
    public bool slowDown = false;
    public bool accelerate = false;
    private bool speedUp = false;
    private float slomoCounter = 0.0f;
    // Update is called once per frame
    void Update() {
        if (slowDown)
        {
            if (Time.timeScale > slomoTime) Time.timeScale -= Time.unscaledDeltaTime;
            else if (Time.timeScale < slomoTime && Time.timeScale != slomoTime)
            {
                Time.timeScale = slomoTime;
                slowDown = false;
                speedUp = true;              
            }
        }
        else if (speedUp) {
            if (slomoCounter < slomoDuration)
            {
                if (Time.timeScale < 1.0) Time.timeScale += Time.unscaledDeltaTime;
                else if (Time.timeScale > 1.0f && Time.timeScale != 1.0f) Time.timeScale = 1.0f;
                slomoCounter += Time.unscaledDeltaTime;

            }
            else {
                Time.timeScale = 1.0f;
                speedUp = false;
            }
        }
       /* else
        {
            if (!accelerate) {
                
                if (Time.timeScale > 1.0) Time.timeScale -= Time.unscaledDeltaTime;
                else if (Time.timeScale < 1.0f && Time.timeScale != 1.0f) Time.timeScale = 1.0f;
            }
            if (accelerate)
            {
                if (Time.timeScale < actualmaxspeed) Time.timeScale += Time.unscaledDeltaTime;
                else if (Time.timeScale > actualmaxspeed && Time.timeScale != 1.0f) Time.timeScale = actualmaxspeed;
            }
        }*/
    }

    public void StartSloMo() {
        slowDown = true;
        slomoCounter = slomoDuration;
    }

    public void RestoreTime() {
        slowDown = false;
    }

    public void StartTimeDash()
    {
        accelerate = true;
    }

    public void RestoreTimeDash()
    {
        accelerate = false;
    }
}
