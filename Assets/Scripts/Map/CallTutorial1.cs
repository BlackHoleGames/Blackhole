using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallTutorial1 : MonoBehaviour {

    public float counter = 4.0f;
    public GameObject player;

    private SwitchablePlayerController spc;
	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        spc = player.GetComponent<SwitchablePlayerController>();
    }

    // Update is called once per frame
    void Update () {
        if (player.activeInHierarchy)
        {
            counter -= Time.deltaTime;
            if (counter <= 0.0f)
            {
                spc.StopTimeTutorial1();
                Destroy(this);
            }
        }
	}
}
