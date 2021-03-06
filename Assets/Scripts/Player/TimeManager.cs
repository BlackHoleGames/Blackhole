﻿
using UnityEngine;

public class TimeManager : MonoBehaviour {

    public float slomoTime = 0.05f;
    public float slomoDuration = 2.0f;
    public float actualmaxspeed = 2.0f;
    public float timeWarpDuration = 10.0f;
    public float timeToGTLIncrease = 5.0f;
    private bool slowDown = false;
    private bool gtlIncreasing = false;
    private bool speedUp = false;
    private bool inFasterGTL = false;
    private bool inTimeWarp = false;
    private float slomoCounter = 0.0f;
    public bool isMaxGTLReached;
    public float gtlFast = 1.5f;
    public float gtlFaster = 2.0f;
    private float targetGTL, gtlCounter, previousTimeScale;
    public CameraBehaviour cb;
    public GameObject timewarpEffect;
    public TimeWarpEffectManager twem;
    private SwitchablePlayerController sp;
    private AudioManagerScript ams;
    private AudioSource timewarpSound;
    private AudioSource gtlUp;
    private bool paused;


    void Start(){
        sp = GameObject.FindGameObjectWithTag("Player").GetComponent<SwitchablePlayerController>();
        cb = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraBehaviour>();
        ams = GameObject.FindGameObjectWithTag("Managers").GetComponentInChildren<AudioManagerScript>();
        isMaxGTLReached = false;
        gtlCounter = 0.0f;
        timewarpSound = GetComponents<AudioSource>()[3];
        gtlUp = GetComponents<AudioSource>()[5];
        paused = false;
    }

    // Update is called once per frame
    void Update() {
        if (!paused)
        {
            if (!gtlIncreasing && !isMaxGTLReached) DoGTL();
            if (slowDown) DoSlowDown();
            else if (speedUp)
            {
                if (slomoCounter < slomoDuration) slomoCounter += Time.unscaledDeltaTime;
                else DoSpeedUp();
            }
            else if (gtlIncreasing) DoGTLIncrease();
        }
    }

    public void StartSloMo() {
        gtlCounter = 0.0f;        
        gtlIncreasing = false;
        speedUp = false;
        isMaxGTLReached = false;
        slowDown = true;
        inFasterGTL = false;
        ams.LowerMusic(slomoDuration);
    }

    public void IncreaseGTL() {
        gtlUp.Play();
        if (!inFasterGTL)
        {
            targetGTL = gtlFast;
            ScoreScript.multiplierScore = 1.5f;
            if (twem.gameObject.activeInHierarchy) twem.SetSimulationSpeed(gtlFast);
            else twem.gameObject.SetActive(true);
        }
        else
        {
            targetGTL = gtlFaster;
            ScoreScript.multiplierScore = 2.0f;
            
            if (twem.gameObject.activeInHierarchy) twem.SetSimulationSpeed(gtlFaster);            
            else twem.gameObject.SetActive(true);  
            
        }
        gtlIncreasing = true;
    }

    public void DoGTLIncrease()
    {
        if (Time.timeScale < targetGTL)
        {
            Time.timeScale += Time.deltaTime;
            if (Time.timeScale > targetGTL)
            {
                Time.timeScale = targetGTL;
                gtlIncreasing = false;
            }
        }
    }

    public void StartWorhmHole() {

    }

    public void RestoreTime() {
        slowDown = false;
        Time.timeScale = 1.0f;
        twem.SetSimulationSpeed(1.0f);
        isMaxGTLReached = false;
        inFasterGTL = false;
        gtlIncreasing = false;
    }

    public void DoGTL() {
        gtlCounter += Time.deltaTime;
        if (gtlCounter > timeToGTLIncrease)
        {
            gtlCounter = 0.0f;
            IncreaseGTL();
            if (!inFasterGTL) inFasterGTL = true;
            else isMaxGTLReached = true;
            sp.SpawnGhost();
        }
    }


    public void StartTimeWarp() {
        cb.SwitchToTimeWarp();
        if (sp.VerticalAxisOn()) sp.SwitchAxis();
        gtlCounter = 0.0f;
        slowDown = false;
        speedUp = false;
        inTimeWarp = true;
        timewarpSound.Play();
        twem.SetSimulationSpeed(Time.timeScale);
    
    }

    public void DoSlowDown() {
        Time.timeScale = slomoTime;
        slowDown = false;
        speedUp = true;
        slomoCounter = 0.0f;
        ScoreScript.multiplierScore = 1.0f;
    }

    public void DoSpeedUp() {       
        if (Time.timeScale < 1.0) Time.timeScale += Time.unscaledDeltaTime;
        else if (Time.timeScale >= 1.0f) {
            Time.timeScale = 1.0f;
            speedUp = false;
        }        
    }

   

    public void DoTimeWarp() {

    }


    public bool InSlowMo() {
        return slowDown;
    }

    public bool InTimeWarp() {
        return inTimeWarp;
    }

    public void StopTimeWarp() {
        inTimeWarp = false;
        cb.SwitchToMiddle();
        if (!sp.VerticalAxisOn()) sp.SwitchAxis();
    }

    public void DebugSwitchGTL(int gtlindex) {
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
                ScoreScript.multiplierScore = 1.5f;
                break;
            case 2:
                inFasterGTL = true;
                isMaxGTLReached = true;
                Time.timeScale = gtlFaster;
                ScoreScript.multiplierScore = 2.0f;
                break;         
        }
        sp.DebugInstantiateGhosts(gtlindex);
    }

    public void PauseGame() {
        paused = true;
        previousTimeScale = Time.timeScale;
        Time.timeScale = 0;
    }

    public void UnPauseGame() {
        paused = false;
        Time.timeScale = previousTimeScale;
    }
}
