using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManager : MonoBehaviour {

    public enum BossStage  {ENTERING, TOPHASE1, PHASE1, TOPHASE2, PHASE2, TOPHASE3, PHASE3}
    // Use this for initialization
    public BossStage actualStage;
    public Transform[] goToPoints;
    public float[] timeToMoveEachPhase;
    private float lerpTime;
    private FirstBossStage fbs;
    private SecondBossStage sbs;
    private ThirdBossStage tbs;
	void Start () {
        actualStage =   BossStage.ENTERING;
        lerpTime = 0.0f;
        fbs = GetComponentInChildren<FirstBossStage>();
        sbs = GetComponentInChildren<SecondBossStage>();
        tbs = GetComponentInChildren<ThirdBossStage>();
    }
	
	// Update is called once per frame
	void Update () {
        switch (actualStage) {
            case BossStage.ENTERING:
                if (Vector3.Distance(transform.position, goToPoints[0].position) > 0.1) lerpTime = Time.deltaTime / timeToMoveEachPhase[0];                
                else actualStage = BossStage.TOPHASE1;                               
                break;
            case BossStage.TOPHASE1:
                if (Vector3.Distance(transform.position, goToPoints[1].position) > 0.1) lerpTime = Time.deltaTime / timeToMoveEachPhase[1];
                else {
                    actualStage = BossStage.PHASE1;
                    fbs.StartBossPhase();
                }
                break;
            case BossStage.TOPHASE2:
                if (Vector3.Distance(transform.position, goToPoints[2].position) > 0.1) lerpTime = Time.deltaTime / timeToMoveEachPhase[2];
                else
                {
                    actualStage = BossStage.PHASE2;
                    sbs.StartBossPhase();
                }
                break;
            case BossStage.TOPHASE3:
                if (Vector3.Distance(transform.position, goToPoints[3].position) > 0.1) lerpTime = Time.deltaTime / timeToMoveEachPhase[3];
                else
                {
                    actualStage = BossStage.PHASE3;
                    tbs.StartBossPhase();
                }
                break;
        }
	}

    public void GoToNextPhase() {
        switch (actualStage)
        {
            case BossStage.PHASE1:
                actualStage = BossStage.TOPHASE2;
                break;
            case BossStage.PHASE2:
                actualStage = BossStage.TOPHASE3;
                break;
            case BossStage.PHASE3:
                // StartLastSection
                break;
        }
    }
}
