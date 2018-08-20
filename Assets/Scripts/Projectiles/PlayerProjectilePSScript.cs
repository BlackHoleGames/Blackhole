using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectilePSScript : MonoBehaviour {

    private ParticleSystem ps;
	// Use this for initialization
	void Start () {
        ps = GetComponent<ParticleSystem>();
        ps.Play();
	}
	
	// Update is called once per frame
	void Update () {
        if (!ps.isPlaying) Destroy(gameObject);
	}
}
