using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDebugOptions : MonoBehaviour {

    public EnemyManager enemyManager;
    public MapManger mapManager;
    public SwitchablePlayerController player;
    //private TimeManager timeManager;
    private bool godModeEnabled = false;
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
    }
}
