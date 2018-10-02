using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeWarpEffectManager : MonoBehaviour {

    private ParticleSystem[] psArray;

    // Use this for initialization
    void Start () {
        psArray = GetComponentsInChildren<ParticleSystem>();
    }

    // Update is called once per frame
    void Update () {
		
	}

    public void SetSimulationSpeed(float speed) {
        foreach (ParticleSystem ps in psArray)
        {
            ParticleSystem.MainModule main = ps.main;
            main.simulationSpeed = speed;
        }
    }
}
