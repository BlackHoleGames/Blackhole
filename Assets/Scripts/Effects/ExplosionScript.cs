using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionScript : MonoBehaviour {


    public ParticleSystem ps;
    private AudioSource audioSource;
	// Use this for initialization
	void Start () {
        audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        if (ps)
        {
            if (!ps.IsAlive()) ps.Stop();
            if (!audioSource.isPlaying)Destroy(gameObject);            
        }
	}
}
