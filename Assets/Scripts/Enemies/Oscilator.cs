using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscilator : MonoBehaviour {
    float timeCounter = 0;

    public float speed = 2.0f;
    public float width = 7.0f;
    public float height = 4.0f;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        timeCounter += Time.deltaTime ;
        if (!SwitchablePlayerController.camMovementEnemies) { 
            float x = Mathf.Cos(timeCounter) * width;
            float y = 0;
            float z = Mathf.Sin(timeCounter) * height; 

            transform.position = new Vector3(x, y, z) * speed;

        }else
        {
            
            float x = Mathf.Cos(timeCounter) * width;
            float z = 10;
            float y = Mathf.Sin(timeCounter) * height;

            transform.position = new Vector3(x, y, z) * speed;
        }
    }
}
