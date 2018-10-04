using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleTimeReducer : MonoBehaviour {

    private float slomoDuration;
    private bool DoSlomo;
    private ParticleSystem[] psArray;
    private ParticleSystem objectPS;
    // Use this for initialization
    void Start () {
        DoSlomo = false;
        psArray = GetComponentsInChildren<ParticleSystem>();
        objectPS = GetComponent<ParticleSystem>();
    }
	
	// Update is called once per frame
	void Update () {
        if (DoSlomo) {
            if (slomoDuration > 0.0f) slomoDuration -= Time.unscaledDeltaTime;
            else {
                slomoDuration = 0.0f;
                DoSlomo = false;
                foreach (ParticleSystem ps in psArray)
                {
                    ParticleSystem.MainModule main = ps.main;
                    main.simulationSpeed = 1.0f;
                }
                if (objectPS)
                {
                    ParticleSystem.MainModule main = objectPS.main;
                    main.simulationSpeed = 1.0f;
                }
            }
        }
	}

    public void SlowDown(float quantity, float duration) {
        slomoDuration = duration;
        DoSlomo = true;
        foreach (ParticleSystem ps in psArray) {
            ParticleSystem.MainModule main = ps.main;
            main.simulationSpeed = quantity;
        }
        if (objectPS) {
            ParticleSystem.MainModule main = objectPS.main;
            main.simulationSpeed = quantity;
        }
    }
}
