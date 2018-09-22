using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class SquadManager : MonoBehaviour {

    // If this squad is a chain of enemies
    public bool isChainOfEnemies;
    public bool isSubsquad;
    public int subSquadCount = 0;
    public GameObject subSquadron;
    public float subSquadUnitDelayTime;
    public SquadManager squadManagerParent;
    // If it's only a squadron
    public int numOfMembers;
    public EnemyManager Manager;
    public float speed = 2.0f;
    public float timeToStay = -1.0f;
    public bool isMiniBoss;
    private bool movingToPosition, stayIsDone, arrivedToStart;
    private EnemyManager.SpawnPoint entryPoint = EnemyManager.SpawnPoint.NOT_SET;

    //private EnemyManager.SpawnPoint exitPoint = EnemyManager.SpawnPoint.NOT_SET;
    private Vector3 center = new Vector3(0.0f,0.0f,5.0f);
    private Vector3 initialPos, target, exit;
    private float timeToMove;
    private TimeBehaviour tb;
	// Use this for initialization
	void Start () {
        Manager = GameObject.FindGameObjectsWithTag("EnemyManager")[0].GetComponent<EnemyManager>();
        tb = GetComponent<TimeBehaviour>();
        timeToMove = 0;
        initialPos = transform.position;
        movingToPosition = true;
        if (isMiniBoss) center += new Vector3(0.0f, 0.0f, 2.0f);
        target = center;
        stayIsDone = false;
        arrivedToStart = false;
        if (isChainOfEnemies) numOfMembers = subSquadCount;
        
    }

    // Update is called once per frame
    void Update () {
        if (!isChainOfEnemies)
        {
            if (entryPoint != EnemyManager.SpawnPoint.NOT_SET)
            {
                ManageMovement();

            }
            if (!stayIsDone && arrivedToStart)
            {
                timeToStay -= Time.deltaTime;
                if (timeToStay <= 0.0f)
                {
                    timeToMove = 0.0f;
                    target = exit;
                    initialPos = transform.position;
                    movingToPosition = true;
                    stayIsDone = true;
                }
            }
        }
    }

    public void SetStartPoint(EnemyManager.SpawnPoint start) {
        entryPoint = start;
    }

    public void SetExitPoint(Vector3 destination)
    {
        exit = destination;
    }

    public void SetTimeToLive(float time) {
        timeToStay = time;
    }

    public void DecreaseNumber() {        
        --numOfMembers;
        if (numOfMembers == 0)
        {
            if (isSubsquad)
            {
                squadManagerParent.DecreaseNumber();
                Destroy(gameObject);
            }
            else
            {
                Manager.StartWait();
                Destroy(gameObject);
                ScoreScript.score = ScoreScript.score + (int)(500 * ScoreScript.multiplierScore);
            }
        }
    }

    public void ManageMovement() {

        if (movingToPosition)
        {
            timeToMove += Time.deltaTime*tb.scaleOfTime / 3.0f;
            transform.position = Vector3.Lerp(initialPos, target, timeToMove);
            if (Vector3.Distance(transform.position,target) < 1.0 ) {
                movingToPosition = false;
                if (stayIsDone)
                {
                    Manager.StartWait();
                    Destroy(gameObject);
                }
                else {
                    if (isMiniBoss)
                    {
                        MiniBossScript script = GetComponentInChildren<MiniBossScript>();                        
                        transform.parent.GetComponent<MapManger>().mbs = script;
                        script.InitiateBoss();
                        Manager.NotifyMiniBossArrivedToPosition();
                        Destroy(this);
                    }
                    arrivedToStart = true;
                }
            }
        }
    }

    public bool ArrivedToCenter() {
        return arrivedToStart;
    }
  
}
