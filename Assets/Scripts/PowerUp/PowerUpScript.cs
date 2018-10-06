using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpScript : MonoBehaviour {

    public float XLimit = 10.0f;
    public float speedX = 2.0f;
    public float speedZ = 2.0f;
    public float lifeGiving = 1.0f;
    public GameObject particleCatched;
    private AudioSource audioSource;
    private bool alive;
    private TimeBehaviour tb;
	// Use this for initialization
	void Start () {
        audioSource = GetComponent<AudioSource>();
        alive = true;

        tb = GetComponent<TimeBehaviour>();
    }
	
	// Update is called once per frame
	void Update () {
        if (alive)
        {
            float nextPosZ = (speedZ * (Time.deltaTime)*tb.scaleOfTime);  
            
            transform.parent.position += new Vector3(0.0f, 0.0f, -nextPosZ);
            if (transform.position.z < -10.0f) Destroy(transform.parent.gameObject);
        }
        else if (!audioSource.isPlaying) Destroy(gameObject);
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (alive)
        {
            if (other.gameObject.tag == "Player")
            {
                audioSource.Play();
                other.gameObject.GetComponent<SwitchablePlayerController>().AddPoints();
                GetComponent<Renderer>().enabled = false;
                particleCatched.SetActive(true);
                alive = false;
            }
            else if (other.gameObject.tag == "BubbleObject")
            {
                audioSource.Play();
                other.gameObject.transform.parent.GetComponentInChildren<SwitchablePlayerController>().AddPoints();
                GetComponent<Renderer>().enabled = false;
                particleCatched.SetActive(true);                  
                alive = false;
            }
        }
    }
}
