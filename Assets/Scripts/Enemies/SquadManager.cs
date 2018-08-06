﻿using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class SquadManager : MonoBehaviour {

    // If this squad is a chain of enemies
    public bool isChainOfEnemies = false;
    public int subSquadCount = 0;
    public GameObject subSquadron;
    public float subSquadUnitDelayTime;

    // If it's only a squadron
    public int numOfMembers;
    public EnemyManager Manager;
    public float speed = 2.0f;
    public float timeToStay = -1.0f;
    private bool movingToPosition, stayIsDone, arrivedToStart;
    private EnemyManager.SpawnPoint entryPoint = EnemyManager.SpawnPoint.NOT_SET;
    //private EnemyManager.SpawnPoint exitPoint = EnemyManager.SpawnPoint.NOT_SET;
    private Vector3 center = new Vector3(0.0f,0.0f,5.0f);
    private Vector3 initialPos, target, exit;
    private float timeToMove;
	// Use this for initialization
	void Start () {
        Manager = GameObject.FindGameObjectsWithTag("EnemyManager")[0].GetComponent<EnemyManager>();
        timeToMove = 0;
        initialPos = transform.position;
        movingToPosition = true;
        target = center;
        stayIsDone = false;
        arrivedToStart = false;
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
            Manager.StartWait();
            Destroy(gameObject);
        }
    }

    public void ManageMovement() {

        if (movingToPosition)
        {
            timeToMove += Time.deltaTime / 3.0f;
            transform.position = Vector3.Lerp(initialPos, target, timeToMove);
            if (Vector3.Distance(transform.position,target)< 1.0 ) {
                movingToPosition = false;
                if (arrivedToStart)
                {
                    Manager.StartWait();
                    Destroy(gameObject);
                }
                else arrivedToStart = true;                
            }
        }
    }

    /*
                     if (timeToStay < 0.0f && numOfMembers > 0) {
                        Destroy(gameObject);
                    }
         */

    /*public void ManageEntry() {
        switch (entryPoint)
    {
        case EnemyManager.SpawnPoint.TOP:
            {
                if (gameObject.transform.position.z > 5.0f) gameObject.transform.Translate(new Vector3(0.0f, 0.0f, -Time.deltaTime * speed));
                break;
            }
        case EnemyManager.SpawnPoint.TOPRIGHT:
            {
                if (gameObject.transform.position.z < 5.0f) gameObject.transform.Translate(new Vector3(0.0f, 0.0f, Time.deltaTime * speed));
                if (gameObject.transform.position.z > 2.0f && gameObject.transform.position.y < 0.0f) gameObject.transform.Translate(new Vector3(0.0f, Time.deltaTime * speed / 2.0f, 0.0f));

                break;
            }
        case EnemyManager.SpawnPoint.TOPLEFT:
            {
                if (gameObject.transform.position.x < 0.0f) gameObject.transform.Translate(new Vector3(Time.deltaTime * speed, 0.0f, 0.0f));

                break;
            }
        case EnemyManager.SpawnPoint.RIGHTUP:
            {
                if (gameObject.transform.position.x > 0.0f) gameObject.transform.Translate(new Vector3(-Time.deltaTime * speed, 0.0f, 0.0f));

                break;
            }
        case EnemyManager.SpawnPoint.RIGHT:
            {
                if (gameObject.transform.position.y > 0.0f) gameObject.transform.Translate(new Vector3(0.0f, -Time.deltaTime * speed, 0.0f));

                break;
            }
        case EnemyManager.SpawnPoint.RIGHTDOWN:
            {
                if (gameObject.transform.position.y < 0.0f) gameObject.transform.Translate(new Vector3(0.0f, Time.deltaTime * speed, 0.0f));

                break;
            }
        case EnemyManager.SpawnPoint.BOTTOMRIGHT:
            {
                break;
            }
        case EnemyManager.SpawnPoint.BOTTOM:
            {
                break;
            }
        case EnemyManager.SpawnPoint.BOTTOMLEFT:
            {
                break;
            }
        case EnemyManager.SpawnPoint.LEFTDOWN:
            {
                break;
            }
        case EnemyManager.SpawnPoint.LEFT:
            {
                break;
            }
        case EnemyManager.SpawnPoint.LEFTUP:
            {
                break;
            }
    }
    }*/

    /*public void ManageExit() {
        switch (exitPoint)
        {
            case EnemyManager.SpawnPoint.TOP:
                {
                    if (gameObject.transform.position.z > 5.0f) gameObject.transform.Translate(new Vector3(0.0f, 0.0f, -Time.deltaTime * speed));
                    break;
                }
            case EnemyManager.SpawnPoint.TOPRIGHT:
                {
                    if (gameObject.transform.position.z < 5.0f) gameObject.transform.Translate(new Vector3(0.0f, 0.0f, Time.deltaTime * speed));
                    if (gameObject.transform.position.z > 2.0f && gameObject.transform.position.y < 0.0f) gameObject.transform.Translate(new Vector3(0.0f, Time.deltaTime * speed / 2.0f, 0.0f));

                    break;
                }
            case EnemyManager.SpawnPoint.TOPLEFT:
                {
                    if (gameObject.transform.position.x < 0.0f) gameObject.transform.Translate(new Vector3(Time.deltaTime * speed, 0.0f, 0.0f));

                    break;
                }
            case EnemyManager.SpawnPoint.RIGHTUP:
                {
                    if (gameObject.transform.position.x > 0.0f) gameObject.transform.Translate(new Vector3(-Time.deltaTime * speed, 0.0f, 0.0f));

                    break;
                }
            case EnemyManager.SpawnPoint.RIGHT:
                {
                    if (gameObject.transform.position.y > 0.0f) gameObject.transform.Translate(new Vector3(0.0f, -Time.deltaTime * speed, 0.0f));

                    break;
                }
            case EnemyManager.SpawnPoint.RIGHTDOWN:
                {
                    if (gameObject.transform.position.y < 0.0f) gameObject.transform.Translate(new Vector3(0.0f, Time.deltaTime * speed, 0.0f));

                    break;
                }
            case EnemyManager.SpawnPoint.BOTTOMRIGHT:
                {
                    break;
                }
            case EnemyManager.SpawnPoint.BOTTOM:
                {
                    break;
                }
            case EnemyManager.SpawnPoint.BOTTOMLEFT:
                {
                    break;
                }
            case EnemyManager.SpawnPoint.LEFTDOWN:
                {
                    break;
                }
            case EnemyManager.SpawnPoint.LEFT:
                {
                    break;
                }
            case EnemyManager.SpawnPoint.LEFTUP:
                {
                    break;
                }
        }
    }*/
}
