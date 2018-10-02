using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallTutorial1 : MonoBehaviour {

    public float counter = 4.0f;
    private SwitchablePlayerController spc;
	// Use this for initialization
	void Start () {
        spc = GameObject.FindGameObjectWithTag("Player").GetComponent<SwitchablePlayerController>();
    }

    // Update is called once per frame
    void Update () {
        counter -= Time.deltaTime;
        if (counter <= 0.0f) {
            spc.StopTimeTutorial1();
            Destroy(this);
        }
	}
}
