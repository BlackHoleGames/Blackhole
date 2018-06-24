using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionScript : MonoBehaviour {


    public ParticleSystem ps;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        if (ps)
        {
            if (ps.IsAlive()) Debug.Log("alive");
            if (!ps.IsAlive()) Destroy(gameObject);            
        }
	}
}
