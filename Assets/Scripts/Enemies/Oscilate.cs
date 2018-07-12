using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscilate : MonoBehaviour {

    private TimeBehaviour tb;
    public float travelDistance = 2.0f;
    public float speed = 1.0f;
    float direction = 1.0f;
    float leftPos, rightPos, upPos, downPos;
	// Use this for initialization
	void Start () {
        tb = gameObject.GetComponent<TimeBehaviour>();
        leftPos = gameObject.transform.position.x - travelDistance;
        rightPos = gameObject.transform.position.x + travelDistance;
        upPos = gameObject.transform.position.y - travelDistance;
        downPos = gameObject.transform.position.y + travelDistance;
    }
	
	// Update is called once per frame
	void Update () {
        if (!SwitchablePlayerController.camMovementEnemies)
        {
            if (gameObject.transform.position.x < leftPos) direction = 1.0f;
            if (gameObject.transform.position.x > rightPos) direction = -1.0f;
            gameObject.transform.position += new Vector3(Time.deltaTime * (speed * tb.scaleOfTime) * direction, 0.0f, 0.0f);
        }else
        {
            if (gameObject.transform.position.y < leftPos) direction = 1.0f;
            if (gameObject.transform.position.y > rightPos) direction = -1.0f;
            gameObject.transform.position += new Vector3(0.0f, Time.deltaTime * (speed * tb.scaleOfTime) * direction, 0.0f);

        }
    }
}
