using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuObjectsFalldown : MonoBehaviour {

	 
	public Vector3 axis;
	public float speed = 0.0f;
	public float fallbackDistance = 10.0f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        gameObject.transform.Translate( axis * speed * Time.deltaTime);
    }

	private void OnTriggerEnter(Collider other){
        if (other.gameObject.tag == "UICamCollider") {
            gameObject.transform.position += axis * fallbackDistance ; 
        }
	}
}
