using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscilate : MonoBehaviour {

    public float travelDistance = 2.0f;
    public float speed = 1.0f;
    float direction = 1.0f;
    float leftPos, rightPos;
	// Use this for initialization
	void Start () {
        leftPos = gameObject.transform.position.x - travelDistance;
        rightPos = gameObject.transform.position.x + travelDistance;
    }
	
	// Update is called once per frame
	void Update () {
        if (gameObject.transform.position.x < leftPos) direction = 1.0f;
        if (gameObject.transform.position.x > rightPos) direction = -1.0f;
        gameObject.transform.position += new Vector3(Time.deltaTime * speed * direction,0.0f,0.0f);
	}
}
