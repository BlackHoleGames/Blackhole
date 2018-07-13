
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
    private float slomoCounter = 0.0f;
    public bool isMaxGTLReached;
    private float[] gtlSeries = {1.25f, 1.75f};
    private float timeWarp = 2.0f;
    private int gtlIndex;
    private float targetGTL, maxGTLCounter, gtlCounter;
    private CameraBehaviour cb;
    private int timeWarpCharges;
    private SwitchablePlayerController sp;


    void Start(){
        sp = GameObject.FindGameObjectWithTag("Player").GetComponent<SwitchablePlayerController>();

        cb = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraBehaviour>();
        isMaxGTLReached = false;
        gtlIndex = 0;
        timeWarpCharges = 0;
    }

    // Update is called once per frame
    void Update() {
        gtlCounter += Time.deltaTime;
        if (gtlCounter > timeToGTLIncrease && gtlIndex < 2) {
            gtlCounter = 0.0f;
            Debug.Log("GTL INCREASE!");
            if (gtlIndex <= 1) {
                IncreaseGTL();
                sp.SpawnGhost();
                 cb.switchCamPosRot(gtlIndex);
                if (gtlIndex < 1) ++gtlIndex;

            }
            if (gtlIndex > 0 && timeWarpCharges < 3) ++timeWarpCharges;             
        }
        if (slowDown) DoSlowDown();
        else if (speedUp) DoSpeedUp();
        else if (gtlIncreasing) DoGTLIncrease();
        if (isMaxGTLReached) {
            maxGTLCounter += Time.unscaledDeltaTime;
            if (maxGTLCounter > timeWarpDuration) {
                Time.timeScale = 1.0f;
                isMaxGTLReached = false;
                cb.ResetToInitial();
            }
        }
    }

    public void StartSloMo() {
        cb.ResetToInitial();
        slowDown = true;
        gtlIndex = 0;
        slomoCounter = slomoDuration;
    }

    public void IncreaseGTL() {
        targetGTL = gtlSeries[gtlIndex];
        gtlIncreasing = true;
    }

    public void RestoreTime() {
        slowDown = false;
    }

    public void StartTimeWarp() {
        if (timeWarpCharges > 0) {
            cb.switchCamPosRot(2);
            sp.SpawnGhost();
            isMaxGTLReached = true;
            gtlIncreasing = true;
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
}
