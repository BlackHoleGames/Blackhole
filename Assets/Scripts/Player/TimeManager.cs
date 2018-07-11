
using UnityEngine;

public class TimeManager : MonoBehaviour {

    public float slomoTime = 0.05f;
    public float actualmaxspeed = 2.0f;
    public float slomoDuration = 2.0f;
    public bool slowDown = false;
    public bool accelerate = false;
    private bool gtlIncreasing = false;
    private bool speedUp = false;
    private float slomoCounter = 0.0f;
    private bool isMaxGTLReached;
    private float[] gtlSeries = { 1.5f, 2.0f};
    private float timeWarp = 3.0f;
    private int gtlIndex;
    private float targetGTL;

    void Start(){
        gtlIndex = 0;
    }

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
                else if (Time.timeScale > 1.0f) Time.timeScale = 1.0f;
                slomoCounter += Time.unscaledDeltaTime;
            }
            else {
                Time.timeScale = 1.0f;
                speedUp = false;
            }
        }
        else if (gtlIncreasing) {
            if (Time.timeScale < targetGTL)
            {
                Debug.Log("Doing");
                Time.timeScale += Time.unscaledDeltaTime;
                if (Time.timeScale > targetGTL)
                {
                    Debug.Log("This is the time scale " + Time.timeScale);
                    Time.timeScale = targetGTL;
                    gtlIncreasing = false;
                    if (gtlIndex < gtlSeries.Length - 1) ++gtlIndex;
                }
            }
        }
       /*else
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
        gtlIndex = 0;
        slomoCounter = slomoDuration;
    }

    public void IncreaseGTL() {
        targetGTL = gtlSeries[gtlIndex];
        Debug.Log("Increasing");
        gtlIncreasing = true;
    }

    public void RestoreTime() {
        slowDown = false;
    }

    public void StartTimeWarp() {
        gtlIncreasing = true;
        targetGTL = timeWarp;
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
