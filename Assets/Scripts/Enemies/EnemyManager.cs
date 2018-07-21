using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {

    public enum SpawnPoint { FRONT, BACK, LEFT, RIGHT, TOP, BOTTOM, NOT_SET };
    public Transform[] spawns;
    public GameObject[] squadrons;
    public SpawnPoint[] squadronSpawnPoints;
    private int squadronIndex;
    private Dictionary<SpawnPoint, Transform> spawnToTransform;

	// Use this for initialization
	void Start () {
        squadronIndex = 0;
        spawnToTransform = new Dictionary<SpawnPoint, Transform>();
        spawnToTransform.Add(SpawnPoint.FRONT, spawns[0]);
        spawnToTransform.Add(SpawnPoint.BACK, spawns[1]);
        spawnToTransform.Add(SpawnPoint.LEFT, spawns[2]);
        spawnToTransform.Add(SpawnPoint.RIGHT, spawns[3]);
        spawnToTransform.Add(SpawnPoint.TOP, spawns[4]);
        spawnToTransform.Add(SpawnPoint.BOTTOM, spawns[5]);
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
            ++squadronIndex;
        }else
        {
            TimerScript.gameover = true;
        }
    }
}
