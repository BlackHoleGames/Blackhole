using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManager : MonoBehaviour {

    public enum BossStage  {ENTERING, TOPHASE1, PHASE1, TOPHASE21, PHASE21, TOPHASE22, PHASE22,TOPHASE3, PHASE3}
    // Use this for initialization
    public BossStage actualStage;
    public Transform[] goToPoints;
    public float[] timeToMoveEachPhase;
    public GameObject thirdPhaseBoss;
    private float lerpTime;
    private FirstBossStage fbs;
    private SecondBossStage sbs;
    private Vector3 initialPos;

	void Start () {
        //actualStage =   BossStage.ENTERING;
        lerpTime = 0.0f;
        fbs = GetComponentInChildren<FirstBossStage>();
        sbs = GetComponentInChildren<SecondBossStage>();
        initialPos = transform.position;    
    }
	
	// Update is called once per frame
	void Update () {
        switch (actualStage) {
            case BossStage.ENTERING:
                lerpTime += Time.deltaTime / timeToMoveEachPhase[0];
                if (Vector3.Distance(transform.position, goToPoints[0].position) > 0.1f) transform.position = Vector3.Lerp(initialPos, goToPoints[0].position, lerpTime);
                else {
                    actualStage = BossStage.TOPHASE1;
                    lerpTime = 0.0f;
                }
                break;
            case BossStage.TOPHASE1:
                lerpTime += Time.deltaTime / timeToMoveEachPhase[1];
                if (Vector3.Distance(transform.position, goToPoints[1].position) > 0.1f) transform.position = Vector3.Lerp(goToPoints[0].position, goToPoints[1].position, lerpTime);
                else {
                    actualStage = BossStage.PHASE1;
                    fbs.StartBossPhase();
                    lerpTime = 0.0f;

                }
                break;
            case BossStage.TOPHASE21:
                lerpTime += Time.deltaTime / timeToMoveEachPhase[2];
                if (Vector3.Distance(transform.position, goToPoints[2].position) > 0.1f) transform.position = Vector3.Lerp(goToPoints[1].position, goToPoints[2].position, lerpTime);
                else {
                    actualStage = BossStage.PHASE21;
                    sbs.StartBossPhase();
                    lerpTime = 0.0f;
                }
                break;
            case BossStage.TOPHASE22:
                lerpTime += Time.deltaTime / timeToMoveEachPhase[3];
                if (Vector3.Distance(transform.position, goToPoints[3].position) > 0.1f) transform.position = Vector3.Lerp(goToPoints[2].position, goToPoints[3].position, lerpTime);
                else
                {
                    actualStage = BossStage.PHASE22;
                    sbs.StartBossPhase2();
                    lerpTime = 0.0f;

                }
                break;
            case BossStage.TOPHASE3:
                lerpTime += Time.deltaTime / timeToMoveEachPhase[4];
                if (Vector3.Distance(transform.position, goToPoints[4].position) > 0.1f) transform.position = Vector3.Lerp(goToPoints[3].position, goToPoints[4].position, lerpTime);
                else
                {
                    actualStage = BossStage.PHASE3;
                    lerpTime = 0.0f;
                    GoToNextPhase();
                }
                break;
        }
	}

    public void GoToNextPhase() {
        switch (actualStage)
        {
            case BossStage.PHASE1:
                actualStage = BossStage.TOPHASE21;
                break;
            case BossStage.PHASE21:
                actualStage = BossStage.TOPHASE22;
                break;
            case BossStage.PHASE22:
                actualStage = BossStage.TOPHASE3;
                break;
            case BossStage.PHASE3:
                thirdPhaseBoss.SetActive(true);
                thirdPhaseBoss.GetComponent<ThirdBossStage>().enabled = true;
                // StartLastSection
                break;
        }
    }

    // Add control to deactivate scripts
    public void DebugNextStage() {
        switch (actualStage)
        {
            case BossStage.ENTERING:
                actualStage = BossStage.TOPHASE1;
                break;
            case BossStage.TOPHASE1:
                actualStage = BossStage.PHASE1;
                break;
            case BossStage.PHASE1:
                actualStage = BossStage.TOPHASE21;
                break;
            case BossStage.TOPHASE21:               
                 actualStage = BossStage.PHASE21;
                break;
            case BossStage.PHASE21:
                actualStage = BossStage.TOPHASE22;
                break;
            case BossStage.TOPHASE22:
                actualStage = BossStage.PHASE22;               
                break;
            case BossStage.PHASE22:
                actualStage = BossStage.TOPHASE3;
                break;
            case BossStage.TOPHASE3:               
                actualStage = BossStage.PHASE3;
                break;
        }
    }
}
