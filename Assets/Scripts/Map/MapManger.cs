using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManger : MonoBehaviour {

    public enum Stages {INTRO, METEORS_TIMEWARP, METEORS_ENEMIES, MINIBOSS, STRUCT_TIMEWARP,
        STRUCT_ENEMIES, BOSS, ESCAPE}
    public Stages actualStage = Stages.INTRO ;
    private EnemyManager em;
    private AsteroidsMovement am;
    private TimeManager tm;
    private bool minibossOn = false;
	// Use this for initialization
	void Start () {
        em = GetComponentInChildren<EnemyManager>();
        actualStage = Stages.METEORS_TIMEWARP;
        am = GameObject.Find("AsteroidsDodge").GetComponent<AsteroidsMovement>();
        tm = GameObject.FindGameObjectWithTag("Player").GetComponent<TimeManager>();
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
            case Stages.MINIBOSS:
                if (!minibossOn) {
                    minibossOn = true;
                    GameObject.Find("MiniBoss").GetComponentInChildren<MiniBossScript>().InitiateBoss();
                }
                break;
            case Stages.STRUCT_TIMEWARP:
                tm.StartTimeWarp();
                break;
            case Stages.STRUCT_ENEMIES:
                if (!em.IsManagerSpawning())
                {
                    em.StartManager();
                    tm.StopTimeWarp();
                }
                break;
            case Stages.BOSS:
                break;
            case Stages.ESCAPE:
                break;
        }
    }

    public void GoToNextStage() {
        ++actualStage;
    }

}
