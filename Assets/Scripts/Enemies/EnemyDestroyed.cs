using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDestroyed : MonoBehaviour {

    public float timeBeforeDestroy = 1.5f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        timeBeforeDestroy -= Time.deltaTime;
        if (timeBeforeDestroy < 0.0f) CallDestroy();
	}

    public void CallDestroy() {
        foreach (Transform child in transform)
        {
            Instantiate(Resources.Load("Explosion"), child.position, child.rotation);
            Destroy(child.gameObject);
        }
        Destroy(gameObject);
    }
}
