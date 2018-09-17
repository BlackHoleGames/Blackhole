using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactorShotFX : MonoBehaviour {

    public float timeToLive = 5.25f;
    private float lightCounter = 0.0f;
    private Light projlight;
    private bool lightningUp;
    private BoxCollider bc;
    private TimeBehaviour tb;
    private AudioSource audioSource;

    // Use this for initialization
    void Start () {
        audioSource = GetComponent<AudioSource>();
        bc = GetComponent<BoxCollider>();
        bc.enabled = false;
        tb = gameObject.GetComponentInParent<TimeBehaviour>();
        lightningUp = true;
        projlight = GetComponentInChildren<Light>();
    }
	
	// Update is called once per frame
	void Update () {
        timeToLive -= Time.deltaTime;
        if (timeToLive <= 0.0f) Destroy(gameObject);
        else
        {
            if (lightningUp) StartLight();
            if (timeToLive < 3.25f && timeToLive > 0.75f && !bc.enabled) bc.enabled = true;
            if (timeToLive <= 0.75)
            {
                if (bc.enabled) bc.enabled = false;
                if (projlight.intensity == 60.0f) projlight.intensity = 30.0f;
                StopLight();
            }
        }
    }

    public void StartLight()
    {
        lightCounter += Time.deltaTime / 2.0f;
        float lightIntensity = Mathf.Lerp(0.0f, 30.0f, lightCounter);
        projlight.intensity = lightIntensity;
        if (lightIntensity >= 30)
        {
            lightCounter = 0.0f;
            lightningUp = false;
            projlight.intensity = 60.0f;
            audioSource.Play();

        }
    }

    public void StopLight()
    {
        lightCounter += Time.deltaTime / 0.5f;
        float lightIntensity = Mathf.Lerp(30.0f, 0.0f, lightCounter);
        projlight.intensity = lightIntensity;
        if (lightIntensity <= 0.0f)
        {
            lightCounter = 0.0f;
            lightningUp = false;
            projlight.intensity = 0.0f;
        }
    }
}
