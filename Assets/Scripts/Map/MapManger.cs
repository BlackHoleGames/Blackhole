using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManger : MonoBehaviour {

    public enum Stages {INTRO, METEORS_TIMEWARP, METEORS_ENEMIES, MINIBOSS_FIRSTPHASE, MINIBOSS_SECONDPHASE, STRUCT_TIMEWARP,
        STRUCT_ENEMIES, BOSS, ESCAPE}
    public Stages actualStage = Stages.INTRO ;
    public float meteorDelayDuration = 0.5f;
    private EnemyManager em;
    private AsteroidsMovement am;
    private TimeManager tm;
    public MiniBossScript mbs;
    public GameObject structure,boss, miniboss, meteors, meteors2d,
        meteorsEnd, asteroidsDodge, timewarpEffect, timewarpBackground,
        battleTunnel;
    private GameObject spawnedEndMeteors;
    private StructMovement sm;
    private CameraBehaviour cb;
    private EarthRotation er;
    private bool structureMoving = false;
    private bool bossEnabled = false;
    private bool meteorsDelayOn = false;
    private bool timewarpBackgroundDelay = false;
    private float meteorDelayCounter = 0.0f;
    private float timewarpDelayCounter = 0.0f;
    private bool removeBattleStruct = true;
	// Use this for initialization
	void Start () {
        em = GetComponentInChildren<EnemyManager>();
        //actualStage = Stages.METEORS_TIMEWARP;
        cb = GameObject.Find("Main Camera").GetComponent<CameraBehaviour>();
        am = asteroidsDodge.GetComponent<AsteroidsMovement>();
        tm = GameObject.FindGameObjectWithTag("Player").GetComponent<TimeManager>();
        er = GameObject.Find("EarthHighFullV2").GetComponent<EarthRotation>();
        
        //sm = structure.GetComponent<StructMovement>();
    }
	
	// Update is called once per frame
	void Update () {
        switch (actualStage)
        {
            case Stages.INTRO:
                if (!em.IsManagerSpawning()) {
                    em.StartManager();
                }
                break;
            case Stages.METEORS_TIMEWARP:
                if (!am.AsteroidsAreMoving()) {
                    if (!meteors) {
                        GameObject obj = Instantiate(Resources.Load("MeteorStormFull 1"),new Vector3(0,0,0), new Quaternion()) as GameObject;
                        meteors = obj;
                        am = obj.GetComponentInChildren<AsteroidsMovement>();
                        asteroidsDodge = am.gameObject;
                    }
                    else meteors.SetActive(true);
                    asteroidsDodge.SetActive(true);
                    am.StartMovingAsteroids();
                    tm.StartTimeWarp();
                    timewarpEffect.SetActive(true);
                    timewarpBackground.SetActive(true);
                }
                break;
            case Stages.METEORS_ENEMIES:
                if (!em.IsManagerSpawning()) {
                    em.SetAsteroidWaveIndex();
                    TimeBombManager.activateBomb2 = true;
                    tm.StopTimeWarp();
                    em.StartManager();
                    meteorsDelayOn = true;
                    timewarpBackgroundDelay = true;
                    timewarpEffect.SetActive(false);
                }
                break;
            case Stages.MINIBOSS_FIRSTPHASE:
                if (mbs) {
                    if (!mbs.MiniBossStarted()) {
                        mbs.InitiateBoss();
                        TimeBombManager.activateBomb2 = true;
                        TimeBombManager.activateBomb3 = true;
                    }
                }
                break;
            case Stages.MINIBOSS_SECONDPHASE:
                if (!mbs.IsSecondPhase()) {
                    TimeBombManager.activateBomb2 = true;
                    TimeBombManager.activateBomb3 = true;
                    structure.SetActive(true);
                    meteorsDelayOn = true;
                    timewarpEffect.SetActive(true);
                    timewarpBackground.SetActive(true);
                    tm.StartTimeWarp();
                    mbs.StartSecondPhase();
                }
                if (!mbs) {
                    GoToNextStage();
                    sm = structure.GetComponent<StructMovement>();
                    sm.StartMovingStruct();
                }
                break;
            case Stages.STRUCT_TIMEWARP:
                if (!sm) {
                    structure.SetActive(true);
                    timewarpEffect.SetActive(true);
                    timewarpBackground.SetActive(true);
                    if (!tm.InTimeWarp()) tm.StartTimeWarp();
                    sm = structure.GetComponent<StructMovement>();
                    sm.StartMovingStruct();
                }
                if (!structureMoving) { 
                    structureMoving = true;
                    er.StartDownTransition();                    
                }
                break;
            case Stages.STRUCT_ENEMIES:
                if (!em.IsManagerSpawning()) {
                    timewarpBackgroundDelay = true;
                    em.SetStructureWaveIndex();
                    timewarpEffect.SetActive(false);
                    em.StartManager();
                    tm.StopTimeWarp();
                    foreach (StructEnemyStageTunnel sest in battleTunnel.GetComponentsInChildren<StructEnemyStageTunnel>()) sest.enabled = true;
                    removeBattleStruct = false;
                }
                break;
            case Stages.BOSS:
                TimeBombManager.activateBomb2 = true;
                TimeBombManager.activateBomb3 = true;
                if (!removeBattleStruct) {
                    if (battleTunnel.GetComponentsInChildren<StructEnemyStageTunnel>().Length > 0) {
                        foreach (StructEnemyStageTunnel sest in battleTunnel.GetComponentsInChildren<StructEnemyStageTunnel>()) sest.FinishSequence();                        
                    }
                    else {
                        Destroy(battleTunnel);
                        removeBattleStruct = true;
                    }
                }
                else {
                    if (!bossEnabled) {
                        boss.SetActive(true);
                        bossEnabled = true;
                    }
                }
                break;
            case Stages.ESCAPE:
                break;
        }
        if (meteorsDelayOn) ManageMeteor();
        if (timewarpBackgroundDelay) ManageTimeWarpBackground();
    }

    public void ManageMeteor() {
        meteorDelayCounter += Time.deltaTime;
        if (meteorDelayCounter > meteorDelayDuration) {
            meteorDelayCounter = 0.0f;
            meteorsDelayOn = false;
            switch (actualStage) {
                case Stages.METEORS_ENEMIES:
                    if(meteors) meteors.SetActive(false);
                    if (meteors2d) meteors2d.SetActive(true);
                    break;
                case Stages.MINIBOSS_SECONDPHASE:
                    if (meteors2d) Destroy(meteors2d);
                    spawnedEndMeteors = Instantiate(meteorsEnd, new Vector3(0.0f,0.0f,0.0f), Quaternion.identity);
                    break;

            }
        }
    }

    public void ManageTimeWarpBackground() {
        timewarpDelayCounter += Time.deltaTime;
        if (timewarpDelayCounter >= 2.0f) {
            timewarpBackground.SetActive(false);
        }
    }

    public void EnteredStructure() {
        if (spawnedEndMeteors) Destroy(spawnedEndMeteors);
    }

    public void GoToNextStage() {
        ++actualStage;
    }

    public void DebugGoToNextStage() {
        switch (actualStage) {
            case Stages.METEORS_TIMEWARP:
                Destroy(am.gameObject.transform.parent.gameObject);
                GoToNextStage();
                break;
            case Stages.METEORS_ENEMIES:
                em.DebugSpawnMiniBoss();
                GoToNextStage();
                break;
            case Stages.STRUCT_ENEMIES:
                GoToNextStage();
                break;
            case Stages.MINIBOSS_FIRSTPHASE:
                Destroy(mbs.gameObject.transform.parent.gameObject);
                actualStage = Stages.STRUCT_TIMEWARP;
                break;
            case Stages.MINIBOSS_SECONDPHASE:
                Destroy(mbs.gameObject.transform.parent.gameObject);
                actualStage = Stages.STRUCT_TIMEWARP;
                break;
            case Stages.BOSS:
                Destroy(boss.gameObject.transform.parent.gameObject);
                GoToNextStage();
                break;
            case Stages.ESCAPE:
                //Call gameover
                break;
            default:
                GoToNextStage();
                break;
        }
    }

    public Stages GetStage() {
        return actualStage;
    }

    public GameObject GetBoss() {
        if (actualStage == Stages.BOSS) return boss;
        else return null;
    }
}
