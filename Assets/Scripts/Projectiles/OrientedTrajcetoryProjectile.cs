using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrientedTrajcetoryProjectile : MonoBehaviour {

    public float speed = 2.0f;
    public float timeToLive = 10.0f;
    public float damage = 10.0f;
    private TimeBehaviour tb;

    // Use this for initialization
    void Start()
    {
        tb = gameObject.GetComponent<TimeBehaviour>();
    }

    // Update is called once per frame
    void Update()
    {
        timeToLive -= Time.deltaTime * tb.scaleOfTime;
        if (timeToLive <= 0.0f) Destroy(gameObject);
        transform.position += transform.forward * speed * tb.scaleOfTime;


    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.tag =="ghost")
        {
            Destroy(gameObject);
        }
    }

}
