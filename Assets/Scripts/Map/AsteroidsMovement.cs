using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidsMovement : MonoBehaviour {

    public float speed = 50.0f;
    private bool start = false;
    private TimeBehaviour tb;
	// Use this for initialization
	void Start () {
        tb = GetComponent<TimeBehaviour>();
	}
	
	// Update is called once per frame
	void Update () {
        if (start) transform.Translate(new Vector3(0.0f,0.0f,-speed*Time.deltaTime*tb.scaleOfTime));
	}

    public void StartMovingAsteroids() {
        start = true;
    }

    public bool AsteroidsAreMoving() {
        return start;
    }
}
