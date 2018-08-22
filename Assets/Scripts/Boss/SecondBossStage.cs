using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondBossStage : MonoBehaviour {


    public GameObject LeftWeakSpot, RightWeakSpot, Eye, player;
    public GameObject[] Turrets;
    public Material Exposed;

    public float eyeTimeToMove = 5.0f;
    private int WeakPointCounter;
    private bool start, rotateEye;
    private float lerpTime, initialRot;

    // Use this for initialization
    void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
        WeakPointCounter = 0;
        lerpTime = 0.0f;
        start = true;
        rotateEye = false;
        initialRot = Eye.transform.eulerAngles.x;
    }

    // Update is called once per frame
    void Update() {
        if (start) {
            foreach (GameObject g in Turrets) g.transform.LookAt(player.transform.position);
            if (rotateEye) ManageEyeRotation();
        }
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
        if (WeakPointCounter >= 2) GetComponentInParent<BossManager>().GoToNextPhase();
        
    }

    public void WeakPointResurrected() {
        --WeakPointCounter;
    }

    public void StartBossPhase() {
        start = true;
        foreach (GameObject t in Turrets) t.GetComponent<BossTurretScript>().enabled = true;
    }

    public void StartBossPhase2() {
        lerpTime = 0.0f;
        rotateEye = true;
    }

    public int GetWeakPointCounter() {
        return WeakPointCounter;
    }

    public void FinishBossPhase() {
        foreach (GameObject t in Turrets) t.GetComponent<BossTurretScript>().enabled = false;
        GetComponentInParent<BossManager>().GoToNextPhase();
    }
}
