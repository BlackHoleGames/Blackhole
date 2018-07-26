using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLaser : MonoBehaviour {

    private float lifeTime;
    private bool start = false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (start) {
            lifeTime -= Time.unscaledDeltaTime;
            if (lifeTime <= 0.0f) Destroy(gameObject);
        }
	}

    public void StartBehaviour(float timeToLive) {
        lifeTime = timeToLive;
        start = true;
    }
}
