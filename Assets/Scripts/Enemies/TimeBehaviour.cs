using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeBehaviour : MonoBehaviour {


    public float scaleOfTime = 1.0f;
    private float slowDownCounter, t;
    bool startSpeedUp, slowingDown;
    private float startingPointScaleOfTime;
    // Use this for initialization
    void Start() {
        startSpeedUp = false;
        slowingDown = false;
        t = 0;
    }

    // Update is called once per frame
    void Update() {
        if (startSpeedUp) {
            if (scaleOfTime < 1.0f)
            {
                t += Time.unscaledDeltaTime / 1.0f;
                scaleOfTime = Mathf.Lerp( startingPointScaleOfTime, 1.0f , t);
            }
        }  
        if (slowingDown) {
            slowDownCounter -= Time.unscaledDeltaTime;
            if (slowDownCounter <= 0.0f) {
                t = 0;
                SpeedUp();
            }
        }
    }

    public void SlowDown(float newScaleOfTime,float slowdownduration) {
        scaleOfTime = newScaleOfTime;
        slowDownCounter = slowdownduration;
        startingPointScaleOfTime = slowdownduration;
        startSpeedUp = false;
        slowingDown = true;
    }

    public void SpeedUp(){
        startSpeedUp = true;
        slowingDown = false;
    }
}
