using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateEnemy : MonoBehaviour {

    public GameObject enemyToInstantiate;
    
	// Use this for initialization
	void Start () {
        GameObject go = Instantiate(enemyToInstantiate, transform.position, transform.rotation);        
        go.transform.parent = transform.parent;
        Destroy(gameObject);
        //GameObject pf = (GameObject)Resources.Load("Resources/"+enemyToInstantiate.name);
        
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
