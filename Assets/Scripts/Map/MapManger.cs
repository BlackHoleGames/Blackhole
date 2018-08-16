using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManger : MonoBehaviour {

    public enum Stages {INTRO, METEORS_TIMEWARP, METEORS_ENEMIES, MINIBOSS_FIRSTPHASE, MINIBOSS_SECONDPHASE, STRUCT_TIMEWARP,
        STRUCT_ENEMIES, BOSS, ESCAPE}
    public Stages actualStage = Stages.INTRO ;
    private EnemyManager em;
    private AsteroidsMovement am;
    private TimeManager tm;
    public MiniBossScript mbs;
    public GameObject structure,boss, miniboss;
    private StructMovement sm;
    private CameraBehaviour cb;
    private EarthRotation er;
    private bool structureMoving = false;
    private bool bossEnabled = false;
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
                }
                break;
            case Stages.METEORS_ENEMIES:
                if (!em.IsManagerSpawning()) {
                    tm.StopTimeWarp();
                    em.StartManager();
                }
                break;
            case Stages.MINIBOSS_FIRSTPHASE:
                if (mbs) {
                    if (!mbs.MiniBossStarted()) mbs.InitiateBoss();
                }
                break;
            case Stages.MINIBOSS_SECONDPHASE:
                if (!mbs.IsSecondPhase()) {
                    tm.StartTimeWarp();
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
                break;
        }
    }

    public void GoToNextStage() {
        ++actualStage;
    }

}
