using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstTrackProjectile : MonoBehaviour {

    public float speed = 2.0f;
    public float timeToLive = 60.0f;
    public float damage = 10.0f;
    private Vector3 target;
    private TimeBehaviour tb;
    private GameObject projectile;

    // Use this for initialization
    void Start()
    {
        tb = gameObject.GetComponent<TimeBehaviour>();
        if (GameObject.FindGameObjectWithTag("Player") != null)
        {
            target = GameObject.FindGameObjectWithTag("Player").transform.position; //(GameObject.FindGameObjectsWithTag("Player")[0].transform.position - gameObject.transform.position).normalized;
            gameObject.transform.LookAt(target);
        }else //isDestroying
        {
            target = GameObject.FindGameObjectWithTag("PlayerDestroyed").transform.position; //(GameObject.FindGameObjectsWithTag("Player")[0].transform.position - gameObject.transform.position).normalized;
            gameObject.transform.LookAt(target);
        }
        Instantiate(Resources.Load("EnemyBasicProjectile New"), transform);

    }

    // Update is called once per frame
    void Update()
    {
        timeToLive -= Time.deltaTime * tb.scaleOfTime;
        if (timeToLive <= 0.0f) Destroy(gameObject);
        if (transform.position.z < -15) Destroy(gameObject);
        gameObject.transform.position += gameObject.transform.forward * speed * tb.scaleOfTime;
        /* gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, new Vector3(target.x, 0.0f, target.z), speed* tb.scaleOfTime);
        if (Vector3.Distance(gameObject.transform.position , new Vector3(target.x, 0.0f, target.z)) < 0.1f){
            Destroy(gameObject);
        }*/

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "ghost")
        {
            Destroy(gameObject);
        }
    }

}
