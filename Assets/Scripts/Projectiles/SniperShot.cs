using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperShot : MonoBehaviour {

    public float timeToLive = 2.0f;
    //public GameObject chargePS, shotPS;
    private Light light;
    private bool lightningUp, lightningDown;
    private Vector3 target;
    private TimeBehaviour tb;
    private float lightCounter = 0.0f;
    // Use this for initialization
    void Start()
    {
        tb = gameObject.GetComponent<TimeBehaviour>();
        target = GameObject.FindGameObjectWithTag("Player").transform.position; //(GameObject.FindGameObjectsWithTag("Player")[0].transform.position - gameObject.transform.position).normalized;
        gameObject.transform.LookAt(target);
        //if (chargePS) chargePS.GetComponent<ParticleSystem>().Play();
        lightningUp = true;
        light = GetComponentInChildren<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        if (timeToLive <= 0.0f)
        {
            //if (chargePS) chargePS.GetComponent<ParticleSystem>().Stop();
            if (timeToLive <= 0.0f) Destroy(gameObject);
        }
        else {
            timeToLive -= Time.deltaTime * tb.scaleOfTime;
            if (lightningUp) StartLight();
            if (timeToLive <= 0.5) {
                if (light.intensity == 60.0f) light.intensity = 30.0f;
                StopLight();
            }
            
        }
    }

    public void StartLight() {
        lightCounter += Time.deltaTime / 2.0f;
        float lightIntensity = Mathf.Lerp(0.0f, 30.0f, lightCounter);
        light.intensity = lightIntensity;
        if (lightIntensity >= 30) {
            lightCounter = 0.0f;
            lightningUp = false;
            light.intensity = 60.0f;
        }
    }

    public void StopLight() {
        lightCounter += Time.deltaTime / 0.5f;
        float lightIntensity = Mathf.Lerp(30.0f, 0.0f, lightCounter);
        light.intensity = lightIntensity;
        if (lightIntensity <= 0.0f)
        {
            lightCounter = 0.0f;
            lightningUp = false;
            light.intensity = 0.0f;
        }
    }

    public void SetTarget(Vector3 target) {
        //transform.LookAt(target);
        Debug.Log("lazooooor");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "ghost")
        {
            Destroy(gameObject);
        }
    }
}
