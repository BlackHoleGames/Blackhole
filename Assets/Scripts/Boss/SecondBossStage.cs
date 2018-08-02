using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondBossStage : MonoBehaviour {


    public GameObject LeftWeakSpot, RightWeakSpot, Eye, player;
    public GameObject[] Turrets;
    private int WeakPointCounter;
    public Material Exposed;
    private bool start, rotateEye;
    private float lerpTime;
    private float initialRot;
    public float eyeTimeToMove = 5.0f;
	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        WeakPointCounter = 0;
        lerpTime = 0.0f;
        start = true;
        initialRot = Eye.transform.eulerAngles.x;
	}
	
	// Update is called once per frame
	void Update () {
        if (start) {
            foreach (GameObject g in Turrets) g.transform.LookAt(player.transform.position);
            if (rotateEye) {
                lerpTime += Time.deltaTime / eyeTimeToMove;
                if (Eye.transform.eulerAngles.x > 90.0f)
                {
                    float rot = Mathf.Lerp(initialRot, 90.0f, lerpTime);
                    Eye.transform.eulerAngles = new Vector3(rot, 0.0f, 0.0f);
                }
                else {
                    Eye.GetComponent<Renderer>().material = Exposed;
                }
            }
        }
	}

    public void WeakPointDone() {
        ++WeakPointCounter;
        if (WeakPointCounter >= 2) {
            rotateEye = true;
            lerpTime = 0.0f;
            
        }
    }

    public void StartBossPhase() {
        start = true;
        foreach (GameObject t in Turrets) t.GetComponent<BossTurretScript>().enabled = true;        
    }

    public void FinishBossPhase() {
        foreach (GameObject t in Turrets) t.GetComponent<BossTurretScript>().enabled = false;
    }
}
