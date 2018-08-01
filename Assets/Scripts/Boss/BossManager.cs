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
	void Start () {
        actualStage =   BossStage.ENTERING;
        lerpTime = 0.0f;
    }
	
	// Update is called once per frame
	void Update () {
        switch (actualStage) {
            case BossStage.ENTERING:
                if (Vector3.Distance(transform.position, goToPoints[0].position) > 0.1) {
                    lerpTime = Time.deltaTime / timeToMoveEachPhase[0];

                }
                else {

                }               
                break;
            case BossStage.TOPHASE1:
                break;
            case BossStage.PHASE1:
                break;
            case BossStage.TOPHASE2:
                break;
            case BossStage.PHASE2:
                break;
            case BossStage.TOPHASE3:
                break;
            case BossStage.PHASE3:
                break;
        }
	}
}
