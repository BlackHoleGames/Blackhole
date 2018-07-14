using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {


    public float speed;
    public float timeToLive = 10.0f;
    public float damage = 10.0f; 
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        timeToLive -= Time.deltaTime;
        if (timeToLive <= 0.0f) Destroy(gameObject);
        gameObject.transform.Translate(0.0f, 0.0f, speed*Time.unscaledDeltaTime);
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "Player" && other.gameObject.tag != "timeBubble") {
            Destroy(gameObject);
        }
    }
}
