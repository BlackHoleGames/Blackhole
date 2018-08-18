using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPropeller : MonoBehaviour {
    public ParticleSystem propeller;
    public float MinIntensityPropeller = 1.5f;
    public float MaxIntensityPropeller = 3.0f;
    public float CurrentIntensityPropeller = 1.0f;
    public float VerticalIntensityX = 1.2f;
    public float VerticalIntensityY = 1.0f;
    public float VerticalIntensityZ = 1.0f;
    public float IncreaseIntensityPropeller = 0.1f;
    public float IntensityZ = 0.5f;
    public bool speedOn = false;
    public bool StandByVertProp = false;
    public static bool isStillAlive = true;
	
	// Update is called once per frame
	void Update () {
        speedOn = GameObject.FindGameObjectWithTag("Player").GetComponent<SwitchablePlayerController>().speedOnProp;
        StandByVertProp = GameObject.FindGameObjectWithTag("Player").GetComponent<SwitchablePlayerController>().StandByVertProp;
        var prop = propeller.main;

        if (isStillAlive) { 
            if (speedOn && StandByVertProp)
            {
                CurrentIntensityPropeller = CurrentIntensityPropeller + IncreaseIntensityPropeller;
                if (CurrentIntensityPropeller > MaxIntensityPropeller) CurrentIntensityPropeller = MaxIntensityPropeller;
                prop.startLifetime = CurrentIntensityPropeller;
                propeller.transform.localScale = new Vector3(CurrentIntensityPropeller, CurrentIntensityPropeller, CurrentIntensityPropeller+ IntensityZ);
            }
            else
            {
                if (CurrentIntensityPropeller <= MinIntensityPropeller) {
                    CurrentIntensityPropeller = MinIntensityPropeller;
                    prop.startLifetime = CurrentIntensityPropeller;
                    if(StandByVertProp) propeller.transform.localScale = new Vector3(CurrentIntensityPropeller, CurrentIntensityPropeller, CurrentIntensityPropeller + IntensityZ);
                    else propeller.transform.localScale = new Vector3(VerticalIntensityX, VerticalIntensityY, VerticalIntensityZ);

                }
                else
                {
                    CurrentIntensityPropeller = CurrentIntensityPropeller - IncreaseIntensityPropeller;
                    prop.startLifetime = CurrentIntensityPropeller;
                    if (StandByVertProp) propeller.transform.localScale = new Vector3(CurrentIntensityPropeller, CurrentIntensityPropeller, CurrentIntensityPropeller + IntensityZ);
                    else propeller.transform.localScale = new Vector3(VerticalIntensityX, VerticalIntensityY, VerticalIntensityZ);
                }
            }
        }
    }
}
