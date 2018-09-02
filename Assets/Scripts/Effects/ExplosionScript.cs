using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionScript : MonoBehaviour {


    public ParticleSystem ps;
    private AudioSource audioSource;
    public float timeToLive = 1.8f;
	// Use this for initialization
	void Start () {
        audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        if (ps)
        {
            timeToLive -= Time.deltaTime;
            if (timeToLive <= 0.0f)
            {
                ps.Stop();
                Destroy(gameObject);
            }
        }
	}
}
