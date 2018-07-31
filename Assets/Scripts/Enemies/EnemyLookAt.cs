using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLookAt : MonoBehaviour {
    Transform playerTransform;
    float rotationSpeed = 3.0f;
    
    // Use this for initialization
    void Start () {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }
	
	// Update is called once per frame
	void Update () {
        transform.rotation = Quaternion.Slerp(transform.rotation, 
            Quaternion.LookRotation(playerTransform.position - transform.position),
            rotationSpeed * Time.deltaTime);
    }
}
