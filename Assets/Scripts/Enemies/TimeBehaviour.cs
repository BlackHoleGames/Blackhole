using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeBehaviour : MonoBehaviour {


    public float scaleOfTime = 1.0f;
    bool startSpeedUp;
    // Use this for initialization
    void Start() {
        startSpeedUp = false;
    }

    // Update is called once per frame
    void Update() {
        if (startSpeedUp) {
            if (scaleOfTime < 1.0f) scaleOfTime += Time.unscaledDeltaTime;
            else if (scaleOfTime > 1.0f)
            {
                scaleOfTime = 1.0f;
                startSpeedUp = false;
            }

        }
    }


    public void SpeedUp(){
        startSpeedUp = true;
    }

}
