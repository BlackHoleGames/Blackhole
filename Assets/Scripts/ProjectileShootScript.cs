using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileShootScript : MonoBehaviour {

    public float timeToLive = 0.5f;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        timeToLive -= Time.deltaTime;
        if (timeToLive <= 0.0f) Destroy(gameObject);
    }
}
