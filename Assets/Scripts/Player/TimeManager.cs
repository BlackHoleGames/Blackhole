
using UnityEngine;

public class TimeManager : MonoBehaviour {

    public float slomoTime = 0.05f;

    public bool slowDown = false;
    // Update is called once per frame
    void Update() {
        Debug.Log(slowDown);
        if (slowDown)
        {
            if (Time.timeScale > slomoTime)
            {
                Debug.Log( "slowing");
                Time.timeScale -= Time.unscaledDeltaTime;
            }
            else if (Time.timeScale < slomoTime && Time.timeScale != slomoTime) Time.timeScale = slomoTime;
        }
        else
        {
            if (Time.timeScale != 1.0f) {
                if (Time.timeScale < 1.0) Time.timeScale += Time.unscaledDeltaTime;
                else if (Time.timeScale > 1.0f && Time.timeScale != 1.0f) Time.timeScale = 1.0f;
            }
        }
        //Debug.Log(Time.timeScale);
    }

    public void StartSloMo() {
        slowDown = true;
    }

    public void RestoreTime() {
        slowDown = false;
    }
}
