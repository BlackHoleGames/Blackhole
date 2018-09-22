using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondBossStage : MonoBehaviour {


    public GameObject LeftWeakSpot, RightWeakSpot, Eye;
    public GameObject[] Turrets;
    public Material Exposed;
    public float eyeTimeToMove = 5.0f;
    public float turretShotSpacing = 1.0f;
    public float speed = 2.0f;
    public float downwardSpeed = 0.05f;
    public float heightLimit = 1.0f;
    public float timeToMoveUp = 3.0f;
    public float initialHeight = 5.0f;
    public float limitX = 9.52f;

    private int WeakPointCounter;//, turretShootIndex;
    private bool start,  inSecondStage,  readjustHeight;
    private float lerpTime, initialRot, direction;
    private Vector3 initialPos;
    private TimeBehaviour tb;
    // Use this for initialization
    void Start() {
        initialPos = transform.parent.transform.position;
        direction = 1;
        readjustHeight = false;
        tb = GetComponent<TimeBehaviour>();
        WeakPointCounter = 0;
        lerpTime = 0.0f;
        start = false;
        inSecondStage = false;
        initialRot = Eye.transform.eulerAngles.x;
        //turretShootIndex = 0;
    }

    // Update is called once per frame
    void Update() {
        if(start) ManageMovement();
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
        foreach (WeakPointSecondStageScript wps in GetComponentsInChildren<WeakPointSecondStageScript>()){
            wps.DisableInvul();
        }
        Eye.GetComponent<EnemyLookAt>().enabled = true;
        foreach (GameObject t in Turrets) {
            t.GetComponentInChildren<BossTurretScript>().enabled = true;
        }
    }

    public void StartBossPhase2() {
        inSecondStage = true;
        lerpTime = 0.0f;
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


    public void ManageMovement()
    {
        if (readjustHeight)
        {
            lerpTime += Time.deltaTime / timeToMoveUp * tb.scaleOfTime;
            transform.parent.transform.position = Vector3.Lerp(initialPos, new Vector3(transform.parent.transform.position.x, transform.parent.transform.position.y, initialHeight), lerpTime);
            if (Mathf.Abs(transform.parent.transform.position.z - initialHeight) < 0.01) readjustHeight = false;
        }
        else
        {
            if (direction > 0)
            {
                if (transform.parent.transform.position.x > limitX) direction = -1;
            }
            else
            {
                if (transform.parent.transform.position.x < -limitX) direction = 1;
            }
            float offsetY = 0;
            if (transform.position.z > heightLimit) offsetY = downwardSpeed * Time.deltaTime * 10.0f * tb.scaleOfTime;
            if (transform.position.z < heightLimit && (transform.position.x <= 1.0f && transform.position.x >= -1.0f))
            {
                readjustHeight = true;
                lerpTime = 0.0f;
                initialPos = transform.position;
            }
            else
            {
                transform.parent.transform.position += new Vector3(speed * Time.deltaTime * tb.scaleOfTime * direction, 0.0f, -offsetY);
            }
        }
    }
}
