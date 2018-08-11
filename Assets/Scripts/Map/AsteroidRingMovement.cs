using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidRingMovement : MonoBehaviour {

    public bool start;
    public float speed = 50.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (start) transform.Translate(new Vector3(0.0f,0.0f,-speed*Time.deltaTime));        
	}

    public void StartMovingAsteroids()
    {
        start = true;
    }

    public bool AsteroidsAreMoving()
    {
        return start;
    }
}
