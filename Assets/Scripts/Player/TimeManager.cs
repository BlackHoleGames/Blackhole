
using UnityEngine;

public class TimeManager : MonoBehaviour {

    public float slomoTime = 0.05f;
    public float actualmaxspeed = 2.0f;
    public float slomoDuration = 2.0f;
    public float timeWarpDuration = 10.0f;
    public float timeToGTLIncrease = 15.0f;
    public bool slowDown = false;
    private bool gtlIncreasing = false;
    private bool speedUp = false;
    private bool inFasterGTL = false;
    private bool returnToFasterGTL = false;
    private float slomoCounter = 0.0f;
    public bool isMaxGTLReached;
    public float gtlFast = 1.5f;
    public float gtlFaster = 2.0f;
    private float targetGTL, maxGTLCounter, gtlCounter;
    private CameraBehaviour cb;
    private SwitchablePlayerController sp;
    private bool firstTimeEnteredSpeedUp;

    void Start(){
        sp = GameObject.FindGameObjectWithTag("Player").GetComponent<SwitchablePlayerController>();
        firstTimeEnteredSpeedUp = true;
        cb = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraBehaviour>();
        isMaxGTLReached = false;
    }

    // Update is called once per frame
    void Update() {
        if (!slowDown && !speedUp && !gtlIncreasing && !isMaxGTLReached) DoGTL();
        if (slowDown) DoSlowDown();
        else if (speedUp) DoSpeedUp();
        else if (gtlIncreasing) DoGTLIncrease();
        if (isMaxGTLReached) DoTimeWarp();
    }

    public void StartSloMo() {
        gtlCounter = 0.0f;        
        gtlIncreasing = false;
        speedUp = false;
        isMaxGTLReached = false;
        slowDown = true;
        inFasterGTL = false;
        slomoCounter = slomoDuration;
    }

    public void IncreaseGTL() {
        if (!inFasterGTL) targetGTL = gtlFast;
        else targetGTL = gtlFaster;
        gtlIncreasing = true;
    }

    public void RestoreTime() {
        slowDown = false;
    }

    public void DoGTL() {
        gtlCounter += Time.deltaTime;
        if (gtlCounter > timeToGTLIncrease)
        {
            gtlCounter = 0.0f;
            if (!inFasterGTL) inFasterGTL = true;
            else isMaxGTLReached = true;
            IncreaseGTL();
            sp.SpawnGhost();
        }
    }

    public void StartTimeWarp() {
        if (!slowDown && !speedUp && !gtlIncreasing && !isMaxGTLReached) {
            cb.SwitchToTimeWarp();
            gtlCounter = 0.0f;
            isMaxGTLReached = true;
            gtlIncreasing = true;
            //if (inFasterGTL) returnToFasterGTL = true;
            //else returnToFasterGTL = false;
            inFasterGTL = false;
            //targetGTL = timeWarp;
        }
    }

    public void DoSlowDown() {
        if (Time.timeScale > slomoTime && (Time.timeScale - Time.unscaledDeltaTime) > 0) Time.timeScale -= Time.unscaledDeltaTime;
        else if (Time.timeScale < slomoTime && Time.timeScale != slomoTime)
        {
            Time.timeScale = slomoTime;
            slowDown = false;
            speedUp = true;
        }
    }

    public void DoSpeedUp() {
        if (slomoCounter < slomoDuration) {
            if (Time.timeScale < 1.0) Time.timeScale += Time.unscaledDeltaTime;
            else if (Time.timeScale > 1.0f) Time.timeScale = 1.0f;
            slomoCounter += Time.unscaledDeltaTime;
        }
        else {
            Time.timeScale = 1.0f;
            speedUp = false;
        }        
    }

    public void DoGTLIncrease() {
        if (Time.timeScale < targetGTL)
        {
            Time.timeScale += Time.unscaledDeltaTime;
            if (Time.timeScale > targetGTL)
            {
                Time.timeScale = targetGTL;
                gtlIncreasing = false;
            }
        }
    }

    public void DoTimeWarp() {
        maxGTLCounter += Time.unscaledDeltaTime;
        if (maxGTLCounter > timeWarpDuration)
        {
            Time.timeScale = gtlFaster;
            isMaxGTLReached = true;
            inFasterGTL = false;
            maxGTLCounter = 0.0f;
            cb.ResetToInitial();
        }
    }



    public bool InSlowMo() {
        return slowDown;
    }
}
