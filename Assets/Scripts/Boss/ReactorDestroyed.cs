using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactorDestroyed : MonoBehaviour {

    public GameObject destroyedReactor;
    private float delayCounter = 0.0f;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        if (delayCounter >= 0.25f) Instantiate(Resources.Load("Explosion"), transform.position, transform.rotation);
        else delayCounter += delayCounter+Time.deltaTime;        
	}
}
