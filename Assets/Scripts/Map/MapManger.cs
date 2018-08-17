using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManger : MonoBehaviour {

    public enum Stages {INTRO, METEORS_TIMEWARP, METEORS_ENEMIES, MINIBOSS_FIRSTPHASE, MINIBOSS_SECONDPHASE, STRUCT_TIMEWARP,
        STRUCT_ENEMIES, BOSS, ESCAPE}
    public Stages actualStage = Stages.INTRO ;
    public float meteorDelayDuration = 0.2f;
    private EnemyManager em;
    private AsteroidsMovement am;
    private TimeManager tm;
    private MiniBossScript mbs;
    public GameObject structure,boss, meteors2d, meteorsEnd, TimeWarp;
    private StructMovement sm;
    private CameraBehaviour cb;
    private EarthRotation er;
    private GameObject EndMeteors ;
    private bool structureMoving = false;
    private bool bossEnabled = false;
    private bool meteorsDelayOn = false;
    private float meteorDelayCounter = 0.0f;
	// Use this for initialization
	void Start () {
        em = GetComponentInChildren<EnemyManager>();
        //actualStage = Stages.METEORS_TIMEWARP;
        cb = GameObject.Find("Main Camera").GetComponent<CameraBehaviour>();
        am = GameObject.Find("AsteroidsDodge").GetComponent<AsteroidsMovement>();
        tm = GameObject.FindGameObjectWithTag("Player").GetComponent<TimeManager>();
        er = GameObject.Find("EarthMapped").GetComponent<EarthRotation>();
        //sm = structure.GetComponent<StructMovement>();
    }
	
	// Update is called once per frame
	void Update () {
        switch (actualStage)
        {
            case Stages.INTRO:
                break;
            case Stages.METEORS_TIMEWARP:
                if (!am.AsteroidsAreMoving()){
                    am.StartMovingAsteroids();
                    tm.StartTimeWarp();
                    TimeWarp.SetActive(true);
                }
                break;
            case Stages.METEORS_ENEMIES:
                if (!em.IsManagerSpawning()) {
                    Destroy(GameObject.Find("MeteorStormFull 1"));
                    tm.StopTimeWarp();
                    TimeWarp.SetActive(false);
                    em.StartManager();
                    meteorsDelayOn = true;
                }
                break;
            case Stages.MINIBOSS_FIRSTPHASE:
                if (mbs) {
                    if (!mbs.MiniBossStarted()) mbs.InitiateBoss();
                }
                break;
            case Stages.MINIBOSS_SECONDPHASE:
                if (!mbs.IsSecondPhase()) {
                    meteorsDelayOn = true;
                    tm.StartTimeWarp();
                    TimeWarp.SetActive(true);
                    mbs.StartSecondPhase();
                }
                if (!mbs) GoToNextStage();
                break;
            case Stages.STRUCT_TIMEWARP:
                if (!structureMoving)
                {
                    er.StartDownTransition();
                    structure.SetActive(true);
                    sm = structure.GetComponent<StructMovement>();
                    sm.StartMovingStruct();
                    structureMoving = true;
                }
                break;
            case Stages.STRUCT_ENEMIES:
                if (!em.IsManagerSpawning()) {
                    em.StartManager();
                    tm.StopTimeWarp();

                }
                break;
            case Stages.BOSS:
                if (!bossEnabled) {
                    cb.SwitchToBoss();
                    boss.SetActive(true);
                    bossEnabled = true;
                }
                break;
            case Stages.ESCAPE:
                tm.StartTimeWarp();
                TimeWarp.SetActive(true);
                break;
        }
        if (meteorsDelayOn) ManageMeteor();
    }

    public void ManageMeteor() {
        meteorDelayCounter += Time.deltaTime;
        if (meteorDelayCounter > meteorDelayDuration) {
            meteorDelayCounter = 0.0f;
            meteorsDelayOn = false;
            switch (actualStage) {
                case Stages.METEORS_ENEMIES:
                    meteors2d.SetActive(true);
                    break;
                case Stages.MINIBOSS_SECONDPHASE:
                    Destroy(meteors2d);
                    EndMeteors = Instantiate(meteorsEnd, new Vector3(0.0f,0.0f,0.0f), Quaternion.identity);
                    break;

            }
        }
    }

    public void EnteredStructure() {
        Destroy(EndMeteors);
        TimeWarp.SetActive(false);

    }

    public void GoToNextStage() {
        ++actualStage;
    }

    public void SetMiniBoss(MiniBossScript miniBossScript) {
        mbs = miniBossScript;
        Destroy(er.gameObject);
    }
}
