using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {

    public enum SpawnPoint {  TOPRIGHT, RIGHTUP, RIGHT, RIGHTDOWN, BOTTOMRIGHT, BOTTOM, BOTTOMLEFT,
    LEFTDOWN,LEFT,LEFTUP, TOPLEFT, TOP, NOT_SET };
    public Transform[] spawns;
    public GameObject[] squadrons;
    public SpawnPoint[] squadronSpawnPoints;
    public SpawnPoint[] squadronExitPoints;
    private int squadronIndex;
    private Dictionary<SpawnPoint, Transform> spawnToTransform;

	// Use this for initialization
	void Start () {
        squadronIndex = 0;
        spawnToTransform = new Dictionary<SpawnPoint, Transform>();
        spawnToTransform.Add(SpawnPoint.TOPRIGHT, spawns[0]);
        spawnToTransform.Add(SpawnPoint.RIGHTUP, spawns[1]);
        spawnToTransform.Add(SpawnPoint.RIGHT, spawns[2]);
        spawnToTransform.Add(SpawnPoint.RIGHTDOWN, spawns[3]);
        spawnToTransform.Add(SpawnPoint.BOTTOMRIGHT, spawns[4]);
        spawnToTransform.Add(SpawnPoint.BOTTOM, spawns[5]);
        spawnToTransform.Add(SpawnPoint.BOTTOMLEFT, spawns[6]);
        spawnToTransform.Add(SpawnPoint.LEFTDOWN, spawns[7]);
        spawnToTransform.Add(SpawnPoint.LEFT, spawns[8]);
        spawnToTransform.Add(SpawnPoint.LEFTUP, spawns[9]);
        spawnToTransform.Add(SpawnPoint.TOPLEFT, spawns[10]);
        spawnToTransform.Add(SpawnPoint.TOP, spawns[11]);

        SpawnNext();
        
    }

    // Update is called once per frame
    void Update () {
       /* if (Input.GetKeyDown(KeyCode.E)) {
            Instantiate(squadrons[squadronIndex], spawn);
        }*/
    }

    public void SpawnNext() {
        if (squadronIndex < squadrons.Length)
        {
            GameObject obj = Instantiate(squadrons[squadronIndex], spawnToTransform[squadronSpawnPoints[squadronIndex]].position, spawnToTransform[squadronSpawnPoints[squadronIndex]].rotation);
            obj.transform.parent = transform.parent;

            obj.GetComponent<SquadManager>().SetStartPoint(squadronSpawnPoints[squadronIndex]);
            obj.GetComponent<SquadManager>().SetExitPoint(spawns[(int)squadronExitPoints[squadronIndex]].position);
            obj.GetComponent<SquadManager>().SetTimeToLive(15.0f);
            ++squadronIndex;
            if(squadronIndex>1)ScoreScript.score = ScoreScript.score+ (int)(500 * ScoreScript.multiplierScore);
        }
        else
        {
            TimerScript.gameover = true;
        }
    }
}
