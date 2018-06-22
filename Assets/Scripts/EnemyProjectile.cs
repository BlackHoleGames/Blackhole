using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour {


    public float speed;
    public float timeToLive = 10.0f;
    public float damage = 10.0f;
    bool inPosition;
    // Use this for initialization
    void Start () {
        inPosition = false;
	}
	
	// Update is called once per frame
	void Update () {
        timeToLive -= Time.deltaTime;
        if (timeToLive <= 0.0f) Destroy(gameObject);
        gameObject.transform.Translate(0.0f, -speed * Time.deltaTime, 0.0f);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
    }
}


