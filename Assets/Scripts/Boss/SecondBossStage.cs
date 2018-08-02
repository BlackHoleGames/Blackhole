using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondBossStage : MonoBehaviour {


    public GameObject LeftWeakSpot, RightWeakSpot, Eye, player;
    public GameObject[] Turrets;
    private int WeakPointCounter;
    public Material Exposed;
    private bool start;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        WeakPointCounter = 0;
        start = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (start) {
            foreach (GameObject g in Turrets) g.transform.LookAt(player.transform.position);            
        }
	}

    public void WeakPointDone() {
        ++WeakPointCounter;
        if (WeakPointCounter >= 2) {
            Eye.GetComponent<Renderer>().material = Exposed;
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
