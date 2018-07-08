using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateEnemy : MonoBehaviour {

    public GameObject enemyToInstantiate;
    
	// Use this for initialization
	void Start () {
        Instantiate(enemyToInstantiate, transform.position, transform.rotation);

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
