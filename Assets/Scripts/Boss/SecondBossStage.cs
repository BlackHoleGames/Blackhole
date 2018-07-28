using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondBossStage : MonoBehaviour {


    public GameObject LeftWeakSpot, RightWeakSpot;
    public GameObject[] Turrets;
    public GameObject Eye;
    public GameObject player;
    private int WeakPointCounter;
    private bool isVulnerable;
    public Material Exposed;


	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        WeakPointCounter = 0;
	}
	
	// Update is called once per frame
	void Update () {
        foreach (GameObject g in Turrets) {
            g.transform.LookAt(player.transform.position);
        }
	}

    public void WeakPointDone() {
        Debug.Log("LIGMA");
        ++WeakPointCounter;
        if (WeakPointCounter >= 2)
        {
            Eye.GetComponent<Renderer>().material = Exposed;
            isVulnerable = true;
        }
    }
}
