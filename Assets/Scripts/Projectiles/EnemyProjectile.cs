using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour {


    public float speed;
    public float timeToLive = 20.0f;
    public float damage = 10.0f;
    private TimeBehaviour tb;
    private GameObject projectile;

    // Use this for initialization
    void Start () {
        tb = gameObject.GetComponent<TimeBehaviour>();
        Instantiate(Resources.Load("EnemyBasicProjectile New"), transform);
	}
	
	// Update is called once per frame
	void Update () {
        timeToLive -= Time.deltaTime*tb.scaleOfTime;
        if (timeToLive <= 0.0f) Destroy(gameObject);
        if (transform.position.z < -15) Destroy(gameObject);
        gameObject.transform.Translate(0.0f, 0.0f,  speed * Time.deltaTime*tb.scaleOfTime);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "ghost")
        {
            Destroy(gameObject);
        }
    }
}


