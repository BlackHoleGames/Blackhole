using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpScript : MonoBehaviour {

    public float XLimit = 10.0f;
    public float speedX = 2.0f;
    public float speedZ = 2.0f;
    public float lifeGiving = 1.0f;

    private float direction;
	// Use this for initialization
	void Start () {
        float randvalue  = Random.Range(-1, 1);
        if (randvalue < 0.0) direction = -1.0f;
        else direction = 1.0f;
    }
	
	// Update is called once per frame
	void Update () {
        float nextPosX = ( speedX * (Time.deltaTime)); // / Time.timeScale));
        float nextPosZ = ( speedZ * (Time.deltaTime));  // / Time.timeScale));
        if (transform.position.x + (nextPosX * direction) <= -XLimit) direction = 1.0f;
        else if (transform.position.x + (nextPosX * direction) >= XLimit) direction = -1.0f;

       transform.position += new Vector3(nextPosX*direction, 0.0f, 0.0f);
       transform.position += new Vector3(0.0f, 0.0f, -nextPosZ);
        if (transform.position.z < -10.0f) Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<SwitchablePlayerController>().AddLife(lifeGiving);
            Destroy(gameObject);
        }
    }
}
