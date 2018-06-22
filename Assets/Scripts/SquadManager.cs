using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquadManager : MonoBehaviour {

    public int numOfMembers;
    public EnemyManager Manager;
	// Use this for initialization
	void Start () {
        Manager = GameObject.FindObjectOfType<EnemyManager>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void DecreaseNumber() {
        --numOfMembers;
        if (numOfMembers <= 0) {
            Manager.SpawnNext();
        }
    }
}
