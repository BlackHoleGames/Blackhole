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
    public float[] squadTime, delayTime;
    private int squadronIndex;
    private Dictionary<SpawnPoint, Transform> spawnToTransform;
    private bool waitForDelay, instantiatingSubSquads, startSpawning;
    private float waitingTime, subSquadDelay;
    private int subSquadEnemyCounter;
    private GameObject ChainSquadManager;
    //subSquadUnitDelayTime
    // Use this for initialization
    void Start () {
        startSpawning = false;
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

        //SpawnNext();
        waitForDelay = false;
    }

    // Update is called once per frame
    void Update () {
        if (startSpawning) {
            if (waitForDelay) {
                waitingTime -= Time.deltaTime;
                if (waitingTime < 0.0f) {
                    waitForDelay = false;
                    SpawnNext();
                }
            }
            if (instantiatingSubSquads) ManageChainSpawn();
        }
    }

    public void SpawnNext() {
        if (!waitForDelay) {
            if (squadronIndex < squadrons.Length) {
                if (squadrons[squadronIndex].GetComponent<SquadManager>().isChainOfEnemies && squadrons[squadronIndex].GetComponent<SquadManager>().subSquadron) {
                    ChainSquadManager = Instantiate(squadrons[squadronIndex], spawnToTransform[squadronSpawnPoints[squadronIndex]].position, spawnToTransform[squadronSpawnPoints[squadronIndex]].rotation);
                    instantiatingSubSquads = true;
                    subSquadEnemyCounter = squadrons[squadronIndex].GetComponent<SquadManager>().subSquadCount;
                    subSquadDelay = squadrons[squadronIndex].GetComponent<SquadManager>().subSquadUnitDelayTime;                    
                }
                else
                {
                    GameObject obj = Instantiate(squadrons[squadronIndex], spawnToTransform[squadronSpawnPoints[squadronIndex]].position, spawnToTransform[squadronSpawnPoints[squadronIndex]].rotation);
                    obj.transform.parent = transform.parent;
                    obj.GetComponent<SquadManager>().SetStartPoint(squadronSpawnPoints[squadronIndex]);
                    obj.GetComponent<SquadManager>().SetExitPoint(spawns[(int)squadronExitPoints[squadronIndex]].position);
                    obj.GetComponent<SquadManager>().SetTimeToLive(squadTime[squadronIndex]);
                    ++squadronIndex;
                    if (squadronIndex > 1) ScoreScript.score = ScoreScript.score + (int)(500 * ScoreScript.multiplierScore);
                }
            }
            else {
                TimerScript.gameover = true;
            }
        }
    }

    public void StartWait() {
        if (!instantiatingSubSquads)
        {
            waitForDelay = true;
            waitingTime = delayTime[squadronIndex-1];
        }
    }

    public void ManageChainSpawn() {
        subSquadDelay -= Time.deltaTime;
        if (subSquadDelay < 0.0f) {
            subSquadDelay = ChainSquadManager.GetComponent<SquadManager>().subSquadUnitDelayTime;
            GameObject obj = Instantiate(ChainSquadManager.GetComponent<SquadManager>().subSquadron, spawnToTransform[squadronSpawnPoints[squadronIndex]].position, spawnToTransform[squadronSpawnPoints[squadronIndex]].rotation);
            obj.transform.parent = transform.parent;
            obj.GetComponent<SquadManager>().SetStartPoint(squadronSpawnPoints[squadronIndex]);
            obj.GetComponent<SquadManager>().SetExitPoint(spawns[(int)squadronExitPoints[squadronIndex]].position);
            obj.GetComponent<SquadManager>().SetTimeToLive(squadTime[squadronIndex]);
            --subSquadEnemyCounter;
            if (subSquadEnemyCounter <= 0) {
                instantiatingSubSquads = false;
                Destroy(ChainSquadManager);
                StartWait();
                ++squadronIndex;
                if (squadronIndex > 1) ScoreScript.score = ScoreScript.score + (int)(500 * ScoreScript.multiplierScore);
            }
        }
    }

    public void StartManager() {
        startSpawning = true;
        SpawnNext();
    }

    public void StopManager() {
        startSpawning = false;
    }

    public bool IsManagerSpawning() {
        return startSpawning;
    }

    public void StartNewPhase() {
        GetComponentInParent<MapManger>().GoToNextStage();
        StopManager();
    }
}
