using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidsMovement : MonoBehaviour {

    public float speed = 50.0f;
    private bool start = false;
    private TimeBehaviour tb;
    private SwitchablePlayerController spc;
    private GameObject player;
    private bool isTutorialTime = false;
    // Use this for initialization
    void Start () {
        tb = GetComponent<TimeBehaviour>();
        player = GameObject.FindGameObjectWithTag("Player");
        if(player) spc = player.GetComponent<SwitchablePlayerController>();
    }
	
	// Update is called once per frame
	void Update () {
        if (!player)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            if (player) spc = player.GetComponent<SwitchablePlayerController>();
        }
        if (start) transform.Translate(new Vector3(0.0f,0.0f,-speed*Time.deltaTime*tb.scaleOfTime));
        if(transform.position.z < -700.0f && !isTutorialTime && player.activeInHierarchy)
        {
            isTutorialTime = true;
            spc.onTutorial = true;
            spc.StopTimeTutorial3();
        }
	}

    public void StartMovingAsteroids() {
        start = true;
    }

    public bool AsteroidsAreMoving() {
        return start;
    }
}
