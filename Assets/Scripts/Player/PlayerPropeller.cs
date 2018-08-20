using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPropeller : MonoBehaviour {
    public ParticleSystem propeller;
    public float MinIntensityPropeller = 0.6f;
    public float MaxIntensityPropeller = 1.0f;
    public float CurrentIntensityPropeller = 0.6f;
    public float VerticalIntensityX = 1.2f;
    public float VerticalIntensityY = 1.0f;
    public float VerticalIntensityZ = 1.0f;
    public float IncreaseIntensityPropeller = 0.5f;
    public float IntensityZ = 0.5f;
    private bool speedOn = false;
    private bool StandByVertProp = false;
    public bool enableStartLifeTime = true;
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
                if(enableStartLifeTime) prop.startLifetime = CurrentIntensityPropeller;
                propeller.transform.localScale = new Vector3(CurrentIntensityPropeller, CurrentIntensityPropeller, CurrentIntensityPropeller+ IntensityZ);
            }
            else
            {
                if (CurrentIntensityPropeller <= MinIntensityPropeller) {
                    CurrentIntensityPropeller = MinIntensityPropeller;
                    if (enableStartLifeTime) prop.startLifetime = CurrentIntensityPropeller;
                    if(StandByVertProp) propeller.transform.localScale = new Vector3(CurrentIntensityPropeller, CurrentIntensityPropeller, CurrentIntensityPropeller + IntensityZ);
                    else propeller.transform.localScale = new Vector3(VerticalIntensityX, VerticalIntensityY, VerticalIntensityZ);

                }
                else
                {
                    CurrentIntensityPropeller = CurrentIntensityPropeller - IncreaseIntensityPropeller;
                    if (enableStartLifeTime) prop.startLifetime = CurrentIntensityPropeller;
                    if (StandByVertProp) propeller.transform.localScale = new Vector3(CurrentIntensityPropeller, CurrentIntensityPropeller, CurrentIntensityPropeller + IntensityZ);
                    else propeller.transform.localScale = new Vector3(VerticalIntensityX, VerticalIntensityY, VerticalIntensityZ);
                }
            }
        }
    }
}
