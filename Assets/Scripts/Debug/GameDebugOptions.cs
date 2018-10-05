using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDebugOptions : MonoBehaviour {

    public EnemyManager enemyManager;
    public MapManger mapManager;
    public SwitchablePlayerController player;
    private bool godModeEnabled = false;
	// Use this for initialization
	void Start () {
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
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            if (godModeEnabled) godModeEnabled = false;
            else godModeEnabled = true;
            player.SetPlayerGodMode(godModeEnabled);
        }
        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            player.GetComponent<TimeManager>().DebugSwitchGTL(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            player.GetComponent<TimeManager>().DebugSwitchGTL(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            player.GetComponent<TimeManager>().DebugSwitchGTL(2);
        }
       
    }
}
