using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargedShot : MonoBehaviour {


    public float chargeTime = 1.0f;
    public float timeToLive = 10.0f;
    public float speed = 2.0f;
    public GameObject chargePS;
    private Vector3 target;
    private TimeBehaviour tb;
	// Use this for initialization
	void Start () {
        tb = gameObject.GetComponent<TimeBehaviour>();
        target = GameObject.FindGameObjectWithTag("Player").transform.position; //(GameObject.FindGameObjectsWithTag("Player")[0].transform.position - gameObject.transform.position).normalized;
        gameObject.transform.LookAt(target);
        if (chargePS) {
            chargePS.GetComponent<ParticleSystem>().Play();
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (chargeTime <= 0.0f) {
            if (chargePS) chargePS.GetComponent<ParticleSystem>().Stop();
            timeToLive -= Time.deltaTime * tb.scaleOfTime;
            if (timeToLive <= 0.0f) Destroy(gameObject);
            gameObject.transform.position += gameObject.transform.forward * speed * tb.scaleOfTime;
        }
        else chargeTime -= Time.deltaTime;
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
    }
}
