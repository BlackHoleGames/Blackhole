using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthRotation : MonoBehaviour {


    public float speed = 1.0f;
    public float downSpeed = 1.0f;
    private bool goDown = false;
	// Use this for initialization
	void Start () {
	    	
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(-speed*Time.deltaTime,0.0f,0.0f);
        if (goDown) transform.position += new Vector3(0.0f, Time.deltaTime * -downSpeed, Time.deltaTime * -downSpeed);
    }

    public void StartDownTransition() {
        goDown = true;
    }

    public bool IsMovingDown() {
        return goDown;
    }
}
