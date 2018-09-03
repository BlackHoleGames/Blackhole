using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDebugOptions : MonoBehaviour {

    public EnemyManager enemyManager;
    public MapManger mapManager;
    public SwitchablePlayerController player;
    //private Camera cam, debugCam;
    //private TimeManager timeManager;
    private bool godModeEnabled = false;
    private bool debugCamEnabled = false;
	// Use this for initialization
	void Start () {
        //timeManager = player.gameObject.GetComponent<TimeManager>();
	}

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            mapManager.DebugGoToNextStage();
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            if (mapManager.GetStage() == MapManger.Stages.BOSS) {

            }
            else enemyManager.DebugSpawnNextWave();
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            if (godModeEnabled) godModeEnabled = false;
            else godModeEnabled = true;
            player.SetPlayerGodMode(godModeEnabled);
        }
        /*if (Input.GetKeyDown(KeyCode.K))
        {
            if (!debugCamEnabled)
            {
                debugCam.transform.position = cam.transform.position;
                debugCam.transform.rotation = cam.transform.rotation;
                debugCamEnabled = true;
                cam.enabled = false;
                debugCam.enabled = true;
            }
            else {
                debugCamEnabled = false;
                cam.enabled = true;
                debugCam.enabled = false;
               
            }
        }*/
    }
}
