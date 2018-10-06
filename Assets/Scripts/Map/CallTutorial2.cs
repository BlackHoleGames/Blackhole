using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallTutorial2 : MonoBehaviour {
    public float counter = 4.0f;
    public GameObject player;
    private SwitchablePlayerController spc;
    private BasicEnemy be;
    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        spc = player.GetComponent<SwitchablePlayerController>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!be) {
            be = GetComponentInChildren<BasicEnemy>();
            if (be) be.Protect();
        }
        if (player.activeInHierarchy)
        {
            counter -= Time.deltaTime;
            if (counter <= 0.0f)
            {
                if(be) be.Unprotect();
                spc.StopTimeTutorial2();
                Destroy(this);
            }
        }
    }
}
