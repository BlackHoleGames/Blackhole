using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeWarpEffectManager : MonoBehaviour {

    private ParticleSystem[] psArray;
    private float actualSpeed;
    // Use this for initialization
    void Start () {
        actualSpeed = Time.timeScale;
        SetSimulationSpeed(actualSpeed);
    }

    // Update is called once per frame
    void Update () {
        if (Time.timeScale != actualSpeed && Time.timeScale > 0.0f) {
            actualSpeed = Time.timeScale;
            SetSimulationSpeed(actualSpeed);
        }
	}

    public void SetSimulationSpeed(float speed) {
        psArray = GetComponentsInChildren<ParticleSystem>();
        foreach (ParticleSystem ps in psArray)
        {
            ParticleSystem.MainModule main = ps.main;
            main.simulationSpeed = speed;
        }
    }
}
