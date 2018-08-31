using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondBossStage : MonoBehaviour {


    public GameObject LeftWeakSpot, RightWeakSpot, Eye;
    public GameObject[] Turrets;
    public Material Exposed;
    public float eyeTimeToMove = 5.0f;
    public float turretShotSpacing = 1.0f;
    private GameObject player;
    private int WeakPointCounter, turretShootIndex;
    private bool start, rotateEye, inSecondStage;
    private float lerpTime, initialRot;

    // Use this for initialization
    void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
        WeakPointCounter = 0;
        lerpTime = 0.0f;
        start = false;
        inSecondStage = false;
        rotateEye = false;
        initialRot = Eye.transform.eulerAngles.x;
        turretShootIndex = 0;
    }

    // Update is called once per frame
    void Update() {

    }   

    public void ManageEyeRotation() {
        lerpTime += Time.deltaTime / eyeTimeToMove;
        if (Eye.transform.eulerAngles.x > 90.0f) Eye.GetComponent<Renderer>().material = Exposed;
        else {
            float rot = Mathf.Lerp(initialRot, 90.0f, lerpTime);
            Eye.transform.eulerAngles = new Vector3(rot, 0.0f, 0.0f);
        }
    }

    public void WeakPointDone() {
        ++WeakPointCounter;
        if (WeakPointCounter >= 2) {
            StartBossPhase2();
        }
    }

    public void WeakPointResurrected() {
        --WeakPointCounter;
    }

    public void StartBossPhase() {
        start = true;
        foreach (GameObject t in Turrets) {
            t.GetComponentInChildren<BossTurretScript>().enabled = true;
        }
    }

    public void StartBossPhase2() {
        inSecondStage = true;
        lerpTime = 0.0f;
        rotateEye = true;
        GetComponentInChildren<BossSecondPhaseWeakpoint>().EnableSecondPhase();
        GetComponentInParent<BossManager>().GoToNextPhase();
    }

    public int GetWeakPointCounter() {
        return WeakPointCounter;
    }

    public void FinishBossPhase() {
        foreach (GameObject t in Turrets) t.GetComponentInChildren<BossTurretScript>().enabled = false;
        GetComponentInParent<BossManager>().GoToNextPhase();
    }

    public bool HasStarted(){
        return start;
    }

    public bool HasSecondStageStarted() {
        return inSecondStage;
    }
}
