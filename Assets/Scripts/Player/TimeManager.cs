
using UnityEngine;

public class TimeManager : MonoBehaviour {

    public float slomoTime = 0.05f;
    public float actualmaxspeed = 2.0f;
    public bool slowDown = false;
    public bool accelerate = false;
    // Update is called once per frame
    void Update() {
        if (slowDown)
        {
            if (Time.timeScale > slomoTime) Time.timeScale -= Time.unscaledDeltaTime;            
            else if (Time.timeScale < slomoTime && Time.timeScale != slomoTime) Time.timeScale = slomoTime;
        }
        else
        {
            if (!accelerate) {
                if (Time.timeScale < 1.0) Time.timeScale += Time.unscaledDeltaTime;
                else if (Time.timeScale > 1.0f && Time.timeScale != 1.0f) Time.timeScale = 1.0f;
                if (Time.timeScale > 1.0) Time.timeScale -= Time.unscaledDeltaTime;
                else if (Time.timeScale < 1.0f && Time.timeScale != 1.0f) Time.timeScale = 1.0f;
            }
            if (accelerate)
            {
                if (Time.timeScale < actualmaxspeed) Time.timeScale += Time.unscaledDeltaTime;
                else if (Time.timeScale > actualmaxspeed && Time.timeScale != 1.0f) Time.timeScale = actualmaxspeed;
            }
        }
        Debug.Log(Time.timeScale);
    }

    public void StartSloMo() {
        if (accelerate) accelerate = false;
        slowDown = true;
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
