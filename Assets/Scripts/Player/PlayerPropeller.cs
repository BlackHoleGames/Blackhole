using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPropeller : MonoBehaviour {
    public ParticleSystem propeller;
    public float MinIntensityPropeller = 0.6f;
    public float MaxIntensityPropeller = 1.0f;
    public float CurrentIntensityPropeller = 0.6f;
    public float IncreaseIntensityPropeller = 0.1f;
    public bool speedOn = false;
    public static bool isStillAlive = true;
	
	// Update is called once per frame
	void Update () {
        speedOn = GameObject.FindGameObjectWithTag("Player").GetComponent<SwitchablePlayerController>().speedOn;
        var prop = propeller.main;

        if (isStillAlive) { 
            if (speedOn)
            {
                CurrentIntensityPropeller = CurrentIntensityPropeller + IncreaseIntensityPropeller;
                if (CurrentIntensityPropeller > MaxIntensityPropeller) CurrentIntensityPropeller = MaxIntensityPropeller;
                prop.startLifetime = MaxIntensityPropeller;
            }else
            {
                if (CurrentIntensityPropeller <= MinIntensityPropeller) { 
                    prop.startLifetime = MinIntensityPropeller;
                }else
                {
                    CurrentIntensityPropeller = CurrentIntensityPropeller - IncreaseIntensityPropeller;
                    prop.startLifetime = CurrentIntensityPropeller;
                }
            }
        }
    }
}
