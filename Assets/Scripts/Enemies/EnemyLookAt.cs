using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLookAt : MonoBehaviour {
    Transform playerTransform;
    public float rotationSpeed = 3.0f;
    private TimeBehaviour tb;
    // Use this for initialization
    void Start () {

        if (GameObject.FindGameObjectWithTag("Player") != null)
        {
            playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        }
        else //isDestroying
        {
            playerTransform = GameObject.FindGameObjectWithTag("PlayerDestroyed").transform;
        }


        tb = GetComponent<TimeBehaviour>();
    }
	
	// Update is called once per frame
	void Update () {
        transform.rotation = Quaternion.Slerp(transform.rotation, 
            Quaternion.LookRotation(playerTransform.position - transform.position),
            rotationSpeed * Time.deltaTime*tb.scaleOfTime);
    }
}
