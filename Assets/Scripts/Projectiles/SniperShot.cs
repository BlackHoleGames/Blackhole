using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperShot : MonoBehaviour {

    public float timeToLive = 2.0f;
    //public GameObject chargePS, shotPS;
    private Light projlight;
    private bool lightningUp, lightningDown;
    public Vector3 target;
    private TimeBehaviour tb;
    private float lightCounter = 0.0f;
    private BoxCollider bc;
    // Use this for initialization
    void Start()
    {
        bc = GetComponent<BoxCollider>();
        bc.enabled = false;
        tb = gameObject.GetComponent<TimeBehaviour>();
        if (GameObject.FindGameObjectWithTag("Player") != null)
        {
            target = GameObject.FindGameObjectWithTag("Player").transform.position; //(GameObject.FindGameObjectsWithTag("Player")[0].transform.position - gameObject.transform.position).normalized;
        }
        else
        {
            target = GameObject.FindGameObjectWithTag("PlayerDestroyed").transform.position;
        }
        //transform.LookAt(target);
        //transform.position = transform.forward;
        Vector3 dir = target- transform.position ;
        transform.rotation = Quaternion.FromToRotation(Vector3.up,dir);
        //if (chargePS) chargePS.GetComponent<ParticleSystem>().Play();
        lightningUp = true;
        projlight = GetComponentInChildren<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        if (timeToLive <= 0.0f)
        {
            if (timeToLive <= 0.0f) Destroy(gameObject);
        }
        else {
            timeToLive -= Time.deltaTime * tb.scaleOfTime;
            if (lightningUp) StartLight();
            if (timeToLive < 2.0f && timeToLive > 0.5f && !bc.enabled) bc.enabled = true;
            if (timeToLive <= 0.5) {
                if (bc.enabled) bc.enabled = false;
                if (projlight.intensity == 60.0f) projlight.intensity = 30.0f;
                StopLight();
            }
            
        }
    }

    public void StartLight() {
        lightCounter += Time.deltaTime / 2.0f;
        float lightIntensity = Mathf.Lerp(0.0f, 30.0f, lightCounter);
        projlight.intensity = lightIntensity;
        if (lightIntensity >= 30) {
            lightCounter = 0.0f;
            lightningUp = false;
            projlight.intensity = 60.0f;
        }
    }

    public void StopLight() {
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


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "ghost")
        {
            //Destroy(gameObject);
        }
    }
}
