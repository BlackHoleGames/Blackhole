﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InputCtrlUI : MonoBehaviour {
    
	// Use this for initialization
	void Start () {
       
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("enter"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
        }
    }
}
