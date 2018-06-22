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
        SpawnNext();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.E)) {
            Instantiate(squadrons[squadronIndex], spawn);
        }
    }

    public void SpawnNext() {
        if (squadronIndex < squadrons.Length)
        {
            Instantiate(squadrons[squadronIndex], spawn);
            ++squadronIndex;
        }
    }
}
