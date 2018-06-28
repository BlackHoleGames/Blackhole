using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstTrackProjectile : MonoBehaviour {

    public float speed = 2.0f;
    public float timeToLive = 10.0f;
    public float damage = 10.0f;
    private Vector3 target;
    private TimeBehaviour tb;

    // Use this for initialization
    void Start()
    {
        tb = gameObject.GetComponent<TimeBehaviour>();
        target = GameObject.FindGameObjectsWithTag("Player")[0].transform.position; //(GameObject.FindGameObjectsWithTag("Player")[0].transform.position - gameObject.transform.position).normalized;
    }

    // Update is called once per frame
    void Update()
    {
        timeToLive -= Time.deltaTime * tb.scaleOfTime;
        if (timeToLive <= 0.0f) Destroy(gameObject);
        gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, new Vector3(target.x, 0.0f, target.z), speed* tb.scaleOfTime);
        if (Vector3.Distance(gameObject.transform.position , new Vector3(target.x, 0.0f, target.z)) < 0.1f){
            Destroy(gameObject);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
    }

}
