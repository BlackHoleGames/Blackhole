using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InputCtrlUI : MonoBehaviour {
    public int scene = 0;
    
	// Use this for initialization
	void Start () {
       
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("enter") || 
            Input.GetKeyDown(KeyCode.Return) || Input.GetButtonDown("Start"))
        {
            switch (scene)
            {
                case 1:
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
                    scene = 0;
                break;
                case 2:
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                    scene = 0;
                break;
                case 3:
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                    scene = 0;
                break;
                default:
                    scene = 0;
                break;
            }
        }
    }

}
