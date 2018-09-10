﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHoleTransition : MonoBehaviour {

    private MapManger mm;
    private float fadeToBlackCount = 0.5f;
    private bool startCount = false;
	// Use this for initialization
	void Start () {
        mm = GameObject.Find("Managers").GetComponent<MapManger>();
	}
	
	// Update is called once per frame
	void Update () {
        if (startCount) {
            fadeToBlackCount -= Time.deltaTime;
            if (fadeToBlackCount <= 0.0f)
            {
                mm.NotifyGameBlackScreen();
                Destroy(gameObject);
            }
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        startCount = true;
    }
}
