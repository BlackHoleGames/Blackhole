using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {

    public Transform spawn;
    public GameObject[] squadrons;
    private int squadronIndex;
	// Use this for initialization
	void Start () {
        squadronIndex = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SpawnNext() {
        Instantiate(squadrons[squadronIndex],spawn);
        ++squadronIndex;
    }
}
