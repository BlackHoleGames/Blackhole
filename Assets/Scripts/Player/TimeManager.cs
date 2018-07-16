
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
    private float slomoCounter = 0.0f;
    public bool isMaxGTLReached;
    private float gtlFast = 1.5f;
    private float timeWarp = 2.0f;
    private float targetGTL, maxGTLCounter, gtlCounter;
    private CameraBehaviour cb;
    private int timeWarpCharges;
    private SwitchablePlayerController sp;


    void Start(){
        sp = GameObject.FindGameObjectWithTag("Player").GetComponent<SwitchablePlayerController>();

        cb = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraBehaviour>();
        isMaxGTLReached = false;
        timeWarpCharges = 3;
    }

    // Update is called once per frame
    void Update() {
        if (!slowDown && !speedUp && !gtlIncreasing && !isMaxGTLReached) {
            gtlCounter += Time.deltaTime;
            if (gtlCounter > timeToGTLIncrease) {
                gtlCounter = 0.0f;
                Debug.Log("GTL INCREASE!");
                if (!inFasterGTL) {
                    IncreaseGTL();
                    sp.SpawnGhost();
                    cb.SwitchCamPosRot();
                    inFasterGTL = true;
                }
                if (inFasterGTL && timeWarpCharges < 3) ++timeWarpCharges;
            }
        }
        if (slowDown) DoSlowDown();
        else if (speedUp) DoSpeedUp();
        else if (gtlIncreasing) DoGTLIncrease();
        if (isMaxGTLReached) DoTimeWarp();
    }

    public void StartSloMo() {
        gtlCounter = 0.0f;        
        cb.ResetToInitial();
        gtlIncreasing = false;
        speedUp = false;
        isMaxGTLReached = false;
        slowDown = true;
        inFasterGTL = false;
        slomoCounter = slomoDuration;
    }

    public void IncreaseGTL() {
        targetGTL =gtlFast;
        gtlIncreasing = true;
    }

    public void RestoreTime() {
        slowDown = false;
    }

    public void StartTimeWarp() {
        if (timeWarpCharges > 0 && !slowDown && !speedUp && !gtlIncreasing && !isMaxGTLReached) {
            gtlCounter = 0.0f;
            cb.SwitchToTimeWarp();
            sp.SpawnGhost();
            isMaxGTLReached = true;
            gtlIncreasing = true;
            inFasterGTL = false;
            targetGTL = timeWarp;
            --timeWarpCharges;
        }
    }

    public void DoSlowDown() {
        if (Time.timeScale > slomoTime) Time.timeScale -= Time.unscaledDeltaTime;
        else if (Time.timeScale < slomoTime && Time.timeScale != slomoTime)
        {
            Time.timeScale = slomoTime;
            slowDown = false;
            speedUp = true;
        }
    }

    public void DoSpeedUp() {
        if (slomoCounter < slomoDuration)
        {
            if (Time.timeScale < 1.0) Time.timeScale += Time.unscaledDeltaTime;
            else if (Time.timeScale > 1.0f) Time.timeScale = 1.0f;
            slomoCounter += Time.unscaledDeltaTime;
        }
        else
        {
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
            Time.timeScale = 1.5f;
            isMaxGTLReached = false;
            inFasterGTL = true;
            cb.SwitchCamPosRot();
            maxGTLCounter = 0.0f;
        }
    }

    public bool HasCharges() {
        return (timeWarpCharges > 0);
    }
}
