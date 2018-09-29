using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManger : MonoBehaviour {

    public enum Stages {INTRO, METEORS_TIMEWARP, METEORS_ENEMIES, MINIBOSS_FIRSTPHASE, MINIBOSS_SECONDPHASE, STRUCT_TIMEWARP,
        STRUCT_ENEMIES, BOSS_TRANSITION, BOSS, ESCAPE}

    public Stages actualStage = Stages.INTRO ;
    public float meteorDelayDuration = 0.75f;
    public float blackScreenDuration = 30.0f;
    public float secondsBeforeBoss = 3.0f;
    public float secondsAfterEachWave = 3.0f;
    public MiniBossScript mbs;
    public GameObject structure,boss, miniboss, meteors, meteors2d,
        meteorsEnd, asteroidsDodge, timewarpEffect, timewarpBackground,
        battleTunnel, blackHole, spacelights, structlights, bosslights;

    private float afterWaveCounter;
    private EnemyManager em;
    private AsteroidsMovement am;
    private TimeManager tm;
    private GameObject spawnedEndMeteors;
    private StructMovement sm;
    private CameraBehaviour cb;
    private EarthRotation er;
    private bool structureMoving, bossEnabled, meteorsDelayOn,timewarpBackgroundDelay,
        managerstartedminiboss, blackholeenabled,  removeBattleStruct;
    public bool onBlackScreen;
    private float meteorDelayCounter, timewarpDelayCounter;
    public float blackScreenCounter;
    private AudioManagerScript ams;

    // Use this for initialization
    void Start () {
        em = GetComponentInChildren<EnemyManager>();
        //actualStage = Stages.METEORS_TIMEWARP;
        cb = GameObject.Find("Main Camera").GetComponent<CameraBehaviour>();
        am = asteroidsDodge.GetComponent<AsteroidsMovement>();
        tm = GameObject.FindGameObjectWithTag("Player").GetComponent<TimeManager>();
        er = GameObject.Find("EarthHighFullV2").GetComponent<EarthRotation>();
        ams = GetComponentInChildren<AudioManagerScript>();
        meteorDelayCounter = 0.0f;
        timewarpDelayCounter = 0.0f;
        blackScreenCounter = 0.0f;
        afterWaveCounter = 0.0f;
        structureMoving = false;
        bossEnabled = false;
        meteorsDelayOn = false;
        managerstartedminiboss = false;
        timewarpBackgroundDelay = false;
        blackholeenabled = false;
        onBlackScreen = false;
        removeBattleStruct = true;
    //sm = structure.GetComponent<StructMovement>();
}
	
	// Update is called once per frame
	void Update () {
        switch (actualStage)
        {
            case Stages.INTRO:
                if (!em.IsManagerSpawning()) {
                    em.SetIntroWaveIndex();
                    em.StartManager();
                }
                break;
            case Stages.METEORS_TIMEWARP:
                if (afterWaveCounter < secondsAfterEachWave) {
                    afterWaveCounter += Time.unscaledDeltaTime;
                    if (afterWaveCounter >= secondsAfterEachWave) CleanUpBullets();
                }
                else
                {
                    if (!am.AsteroidsAreMoving())
                    {
                        CleanUpBullets();
                        if (!meteors)
                        {
                            GameObject obj = Instantiate(Resources.Load("MeteorStormFull 1"), new Vector3(0, 0, 0), new Quaternion()) as GameObject;
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
                    afterWaveCounter = 0.0f;
                }

                break;
            case Stages.MINIBOSS_FIRSTPHASE:
                if (mbs)
                {
                    if (!mbs.MiniBossStarted())
                    {
                        mbs.InitiateBoss();
                        TimeBombManager.activateBomb2 = true;
                        TimeBombManager.activateBomb3 = true;
                    }
                }
                else {
                    if (!managerstartedminiboss)
                    {
                        em.SetMiniBossIndex();
                        if (!em.IsManagerSpawning()) em.StartManager();                        
                    }
                }
                break;
            case Stages.MINIBOSS_SECONDPHASE:
                if (!mbs.IsSecondPhase()) {
                    TimeBombManager.activateBomb2 = true;
                    TimeBombManager.activateBomb3 = true;
                    er.StartDownTransition();
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
            case Stages.BOSS_TRANSITION:
                if (afterWaveCounter < secondsAfterEachWave) {
                    afterWaveCounter += Time.unscaledDeltaTime;
                    if (afterWaveCounter >= secondsAfterEachWave) CleanUpBullets();
                }
                else
                {
                    if (!blackholeenabled)
                    {
                        blackHole.SetActive(true);
                        tm.StartTimeWarp();
                        timewarpEffect.SetActive(true);
                        blackholeenabled = true;
                    }
                    if (!removeBattleStruct)
                    {
                        if (battleTunnel.GetComponentsInChildren<StructEnemyStageTunnel>().Length > 0)
                        {
                            foreach (StructEnemyStageTunnel sest in battleTunnel.GetComponentsInChildren<StructEnemyStageTunnel>()) sest.FinishSequence();
                        }
                        else
                        {
                            Destroy(battleTunnel);
                            removeBattleStruct = true;
                        }
                    }
                    if (onBlackScreen)
                    {
                        if (blackScreenCounter < blackScreenDuration) blackScreenCounter += Time.deltaTime;
                        else
                        {
                            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<PostProcessingSwitcher>().SwitchPostProcess(PostProcessingSwitcher.Profiles.MAGNETIC_STORM);
                            GoToNextStage();
                        }
                    }
                }
                break;
            case Stages.BOSS:                                               
                if (!bossEnabled) {
                    // Activate boss lights
                    //Destroy(structlights);
                    //bosslights.SetActive(true);
                    if (secondsBeforeBoss > 0.0f) secondsBeforeBoss -= Time.deltaTime;
                    else {
                        ams.SetBlackHoleReverb();
                        tm.StopTimeWarp();
                        ams.ChangeToBossMusic();
                        TimeBombManager.activateBomb2 = true;
                        TimeBombManager.activateBomb3 = true;
                        boss.SetActive(true);
                        bossEnabled = true;
                    }
                }                
                break;
            case Stages.ESCAPE:
                GameObject.FindGameObjectWithTag("Player").GetComponent<SwitchablePlayerController>().isEnding = true;
                GameObject.FindGameObjectWithTag("UI_InGame").GetComponent<ChangeScene>().shutdown = true;
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
        Destroy(spacelights);
        structlights.SetActive(true);
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
                GameObject.FindGameObjectWithTag("Player").GetComponent<SwitchablePlayerController>().isEnding = true;
                GameObject.FindGameObjectWithTag("UI_InGame").GetComponent<ChangeScene>().shutdown = true;
                break;
            default:
                GoToNextStage();
                break;
        }
    }

    public void DebugRestartPhase() {
        switch (actualStage)
        {
            case Stages.INTRO:
                if (!em.IsManagerSpawning())
                {
                    em.SetIntroWaveIndex();
                    em.StartManager();
                }
                break;
            case Stages.METEORS_TIMEWARP:
                if (!am.AsteroidsAreMoving())
                {
                    if (!meteors)
                    {
                        GameObject obj = Instantiate(Resources.Load("MeteorStormFull 1"), new Vector3(0, 0, 0), new Quaternion()) as GameObject;
                        meteors = obj;
                        am = obj.GetComponentInChildren<AsteroidsMovement>();
                        asteroidsDodge = am.gameObject;
                    }
                    meteors.SetActive(true);
                    asteroidsDodge.SetActive(true);
                    am.StartMovingAsteroids();
                    tm.StartTimeWarp();
                    timewarpEffect.SetActive(true);
                    timewarpBackground.SetActive(true);
                }
                break;
            case Stages.METEORS_ENEMIES:
                if (!em.IsManagerSpawning())
                {
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
                if (mbs)
                {
                    if (!mbs.MiniBossStarted())
                    {
                        mbs.InitiateBoss();
                        TimeBombManager.activateBomb2 = true;
                        TimeBombManager.activateBomb3 = true;
                    }
                }
                break;
            case Stages.MINIBOSS_SECONDPHASE:
                --actualStage;
                DebugRestartPhase();
                break;
            case Stages.STRUCT_TIMEWARP:
                if (!sm)
                {
                    structure.SetActive(true);
                    timewarpEffect.SetActive(true);
                    timewarpBackground.SetActive(true);
                    if (!tm.InTimeWarp()) tm.StartTimeWarp();
                    sm = structure.GetComponent<StructMovement>();
                    sm.StartMovingStruct();
                }
                if (!structureMoving)
                {
                    structureMoving = true;
                    er.StartDownTransition();
                }
                break;
            case Stages.STRUCT_ENEMIES:
                if (!em.IsManagerSpawning())
                {
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
                if (!removeBattleStruct)
                {
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
                        TimeBombManager.activateBomb2 = true;
                        TimeBombManager.activateBomb3 = true;
                        boss.SetActive(true);
                        bossEnabled = true;
                    }
                }
                break;
            case Stages.ESCAPE:
                GameObject.FindGameObjectWithTag("Player").GetComponent<SwitchablePlayerController>().isEnding = true;
                GameObject.FindGameObjectWithTag("UI_InGame").GetComponent<ChangeScene>().shutdown = true;
                break;
        }
    }

    private void CleanUpBullets() {
        GameObject[] projectiles = GameObject.FindGameObjectsWithTag("EnemyProjectile");
        GameObject[] powerups = GameObject.FindGameObjectsWithTag("powerUp");
        foreach (GameObject obj in projectiles) {
            Instantiate(Resources.Load("PS_ProjectileHit"), transform.position, transform.rotation);
            Destroy(obj);
        }
        foreach (GameObject obj in powerups)
        {
            Instantiate(Resources.Load("TimeBubbleCatched"), transform.position, transform.rotation);
            Destroy(obj);
        }
    }

    public Stages GetStage() {
        return actualStage;
    }

    public GameObject GetBoss() {
        if (actualStage == Stages.BOSS) return boss;
        else return null;
    }

    public void NotifyGameBlackScreen() {
        onBlackScreen = true;
    }

    public void NotifyEnteredBlackHole() {
        ams.PlayBlackHoleEnterSound();
    }

    public void NotifyManagerSpawnedMinidboss() {
        managerstartedminiboss = true;
    }

    public void NotifyBossInPosition() {
        actualStage = Stages.MINIBOSS_FIRSTPHASE;
    }

    public void NotifyBossSecondPhaseStarted() {
        actualStage = Stages.MINIBOSS_SECONDPHASE;
    }

}
