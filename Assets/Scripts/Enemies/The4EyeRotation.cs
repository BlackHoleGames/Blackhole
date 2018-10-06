using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class The4EyeRotation : MonoBehaviour {
    public float speed = 1.0f;
    private TimeBehaviour tb;
    // Use this for initialization
    void Start () {
        tb = GetComponent<TimeBehaviour>();
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(0.0f, 0.0f, -speed * Time.deltaTime*tb.scaleOfTime);
    }
}
