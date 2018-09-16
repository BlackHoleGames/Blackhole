
using UnityEngine;

public class TimeManager : MonoBehaviour {

    public float slomoTime = 0.05f;
    public float actualmaxspeed = 2.0f;
    public float slomoDuration = 2.0f;
    public float timeWarpDuration = 10.0f;
    public float timeToGTLIncrease = 5.0f;
    public float wormHoleDuration = 10.0f;
    public float wormholeMultiplier = 3.0f;
    private bool slowDown = false;
    private bool gtlIncreasing = false;
    private bool speedUp = false;
    private bool inFasterGTL = false;
    private bool returnToFasterGTL = false;
    private bool inTimeWarp = false;
    private float slomoCounter = 0.0f;
    public bool isMaxGTLReached;
    public float gtlFast = 1.5f;
    public float gtlFaster = 2.0f;
    private float targetGTL, maxGTLCounter, gtlCounter, wormHoleCounter;
    public CameraBehaviour cb;
    private SwitchablePlayerController sp;
    private bool wormhole = false;
    private AudioManagerScript ams;
    public GameObject timewarpEffect;

    void Start(){
        sp = GameObject.FindGameObjectWithTag("Player").GetComponent<SwitchablePlayerController>();
        cb = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraBehaviour>();
        ams = GameObject.FindGameObjectWithTag("Managers").GetComponentInChildren<AudioManagerScript>();
        isMaxGTLReached = false;
        wormHoleCounter = wormHoleDuration;
    }

    // Update is called once per frame
    void Update() {
        if (!slowDown && !speedUp && !gtlIncreasing && !isMaxGTLReached && !wormhole) DoGTL();
        if (slowDown) DoSlowDown();
        else if (speedUp) DoSpeedUp();
        else if (gtlIncreasing) DoGTLIncrease();
        else if (wormhole) DoWorhmHole();
        //if (isMaxGTLReached) DoTimeWarp();
    }

    public void StartSloMo() {
        gtlCounter = 0.0f;        
        gtlIncreasing = false;
        speedUp = false;
        isMaxGTLReached = false;
        slowDown = true;
        inFasterGTL = false;
        wormhole = false;
        ams.LowerThePitch();
    }

    public void IncreaseGTL() {
        if (!inFasterGTL)
        {
            targetGTL = gtlFast;
            ScoreScript.multiplierScore = 2.0f;
        }
        else
        {
            targetGTL = gtlFaster;
            ScoreScript.multiplierScore = 3.0f;
        }
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

    public void StartWorhmHole() {
        wormhole = true;
        wormHoleCounter = wormHoleDuration;
        if (slowDown) {
            Time.timeScale = 1.0f;
            slowDown = false;
            ScoreScript.multiplierScore = 1.0f;
        }
        if (speedUp) {
            Time.timeScale = 1.0f;
            speedUp = false;
        }
        if (gtlIncreasing)
        {
            Time.timeScale = 1.0f;
            gtlIncreasing = false;
            ScoreScript.multiplierScore = 1.0f;
        }
        Time.timeScale = Time.timeScale * wormholeMultiplier;
    }

    public void StartTimeWarp() {
        Time.timeScale = 1.0f;
        cb.SwitchToTimeWarp();
        if (sp.VerticalAxisOn()) sp.SwitchAxis();
        gtlCounter = 0.0f;
        slowDown = false;
        speedUp = false;
        isMaxGTLReached = true;
        gtlIncreasing = true;
        inTimeWarp = true;
        //if (inFasterGTL) returnToFasterGTL = true;
        //else returnToFasterGTL = false;
        inFasterGTL = false;
        //timewarpEffect.SetActive(true);
        //targetGTL = timeWarp;        
    }

    public void DoSlowDown() {
        if (Time.timeScale > slomoTime && (Time.timeScale - Time.unscaledDeltaTime) > 0) Time.timeScale -= Time.unscaledDeltaTime;
        else if (Time.timeScale < slomoTime && Time.timeScale != slomoTime)
        {
            Time.timeScale = slomoTime;
            slowDown = false;
            speedUp = true;
            slomoCounter = 0.0f;
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
            ams.RestoreThePitch();
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
       /* maxGTLCounter += Time.unscaledDeltaTime;
        if (maxGTLCounter > timeWarpDuration)
        {
            Time.timeScale = gtlFaster;
            isMaxGTLReached = true;
            inFasterGTL = false;
            maxGTLCounter = 0.0f;
            cb.ResetToInitial();
        }*/
    }

    public void DoWorhmHole() {
        wormHoleCounter -= Time.unscaledDeltaTime;
        if (wormHoleCounter <= 0.0f) {
            wormhole = false;
            Time.timeScale = Time.timeScale / wormholeMultiplier;
        }
    }

    public bool InSlowMo() {
        return slowDown;
    }

    public bool InTimeWarp() {
        return inTimeWarp;
    }

    public void StopTimeWarp() {
        Time.timeScale = 1.0f;
        isMaxGTLReached = false;
        inFasterGTL = false;
        inTimeWarp = false;
        maxGTLCounter = 0.0f;
        cb.SwitchToMiddle();
        if (!sp.VerticalAxisOn()) sp.SwitchAxis();
    }

    public void DebugSwitchGTL(int gtlindex) {
        Debug.Log("Switching");
        switch (gtlindex) {
            case 0:
                inFasterGTL = false;
                isMaxGTLReached = false;
                Time.timeScale = 1.0f;
                ScoreScript.multiplierScore = 1.0f;
                
                break;
            case 1:
                inFasterGTL = true;
                isMaxGTLReached = false;
                Time.timeScale = gtlFast;
                ScoreScript.multiplierScore = 2.0f;
                break;
            case 2:
                inFasterGTL = true;
                isMaxGTLReached = true;
                Time.timeScale = gtlFaster;
                ScoreScript.multiplierScore = 3.0f;
                break;         
        }
        maxGTLCounter = 0.0f;
        sp.DebugInstantiateGhosts(gtlindex);
    }
}
