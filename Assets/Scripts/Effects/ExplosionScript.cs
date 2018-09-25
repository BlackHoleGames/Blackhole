using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionScript : MonoBehaviour {


    public ParticleSystem ps;
    private AudioSource audioSource;
    public float timeToLive = 1.8f;
    private TimeBehaviour tb;
	// Use this for initialization
	void Start () {
        audioSource = GetComponent<AudioSource>();
        audioSource.Play();
        tb = GetComponent<TimeBehaviour>();
	}
	
	// Update is called once per frame
	void Update () {
        if (ps)
        {
            timeToLive -= Time.deltaTime*tb.scaleOfTime;
            if (timeToLive <= 0.0f)
            {
                Destroy(gameObject);
            }
        }
	}
}
