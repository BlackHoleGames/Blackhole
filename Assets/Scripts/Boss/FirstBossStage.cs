﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstBossStage : MonoBehaviour {

    public float limitX = 10.0f;  
    public float timeBetweenReactorShots = 3.0f;
    public float shotDuration = 5.0f;
    public float windowOfOportunityDuration = 1.0f;
    public float speed = 2.0f;
    public float downwardSpeed = 0.05f;
    public float heightLimit = 1.0f;
    public float timeToMoveUp = 3.0f;
    public float initialHeight = 5.0f;
    public GameObject reactorShot;
    public GameObject[] reactorPoints;

    private bool start, shotSpawned, shotDone, openingHatch, closingHatch, readjustHeight;
    private TimeBehaviour tb;
    private int[] reactorSequence1 = { 1, 0, 0, 1 }; // Remember all will shoot after the last one in this sequence
    private int[] reactorSequence2 = { 3, 2, 3, 2 }; // Remember all will shoot after the last one in this sequence
    private int reactorShotIndex, direction, activeReactorsIndex, reactorDestroyedCount;
    private float timeBetweenShotsCounter, shotDurationCounter, windowOfOportunityCounter, lerpTime;
    private Vector3 initialPos;
    // Use this for initialization
    void Start () {
        initialPos = transform.position;
        lerpTime = 0.0f;
        direction = 1;
        tb = GetComponent<TimeBehaviour>();
        start = false;
        shotSpawned = false;
        shotDone = false;
        openingHatch = false;
        closingHatch = false;
        readjustHeight = false;
        reactorShotIndex = 0;
        activeReactorsIndex = 0;
        timeBetweenShotsCounter = timeBetweenReactorShots;
        shotDurationCounter = shotDuration;
        windowOfOportunityCounter = windowOfOportunityDuration;
	}
	
	// Update is called once per frame
	void Update () {
        if (start) {
            ManageMovement();
            timeBetweenShotsCounter -= Time.unscaledDeltaTime;
            if (timeBetweenShotsCounter <= 0.0f) {
                windowOfOportunityCounter -= Time.deltaTime;
                if (!openingHatch) {
                    openingHatch = true;
                    PlayOpeningHatch();
                    SwitchVulnerabilityOnReactors(true);
                }
                if (windowOfOportunityCounter <= 0.0f) {
                    if (!shotDone) ManageShots();
                    if (closingHatch) {
                        closingHatch = false;
                        SwitchVulnerabilityOnReactors(false);
                        ++activeReactorsIndex;
                    }
                    else {
                        if (shotDone) {
                            windowOfOportunityCounter = windowOfOportunityDuration;
                            timeBetweenShotsCounter = timeBetweenReactorShots;
                            shotDone = false;
                            openingHatch = false;
                        }
                    }
                }
            }
        }
	}

    public void ManageMovement() {
        if (readjustHeight) {
            lerpTime += Time.deltaTime / timeToMoveUp;
            transform.position = Vector3.Lerp(initialPos, new Vector3(), lerpTime);
            if (Mathf.Abs(transform.position.y - initialHeight) < 0.01) readjustHeight = false;
        }
        else {
            if (direction > 0) {
                if (transform.position.x > limitX) direction = -1;
            }
            else {
                if (transform.position.x < -limitX) direction = 1;
            }
            float offsetY = 0;
            /*if (transform.position.y > heightLimit) offsetY = downwardSpeed * Time.deltaTime * tb.scaleOfTime;
            if (transform.position.y < heightLimit && (transform.position.x <= 1.0f && transform.position.x >= -1.0f)) {
                readjustHeight = true;
                lerpTime = 0.0f;
                initialPos = transform.position;
            }
            else {*/
            transform.position += new Vector3(speed * Time.deltaTime * tb.scaleOfTime* direction, -offsetY, 0.0f);
            //}
        }
    }

    public void ManageShots() {
        if (!shotSpawned) {
            shotSpawned = true;
            if (reactorShotIndex < reactorSequence1.Length) {
                SpawnShot(reactorSequence1[reactorShotIndex]);
                SpawnShot(reactorSequence2[reactorShotIndex]);
                ++reactorShotIndex;
            }
            else {
                reactorShotIndex = 0;
                SpawnShot(0);
                SpawnShot(1);
                SpawnShot(2);
                SpawnShot(3);
            }
        }
        shotDurationCounter -= Time.unscaledDeltaTime;
        if (shotDurationCounter <= 0.0f) {
            shotSpawned = false;
            shotDone = true;
            closingHatch = true;
            shotDurationCounter = shotDuration;
            windowOfOportunityCounter = windowOfOportunityDuration;
            PlayClosingHatch();
        }
    }

    public void SpawnShot(int index) {

        GameObject obj = Instantiate(reactorShot,reactorPoints[index].transform.position+ new Vector3(0.0f,0.0f,-15.0f), reactorPoints[index].transform.rotation);
        obj.transform.parent = reactorPoints[index].transform;
        obj.GetComponent<BossLaser>().StartBehaviour(shotDuration);
    }

    public void PlayOpeningHatch() {

    }

    public void SwitchVulnerabilityOnReactors(bool vulnerability) {
        if (activeReactorsIndex < reactorSequence1.Length) {
            if (vulnerability) {
                reactorPoints[reactorSequence1[activeReactorsIndex]].GetComponent<ReactorWeakPoint>().UnProtect();
                reactorPoints[reactorSequence2[activeReactorsIndex]].GetComponent<ReactorWeakPoint>().UnProtect();
            }
            else {
                reactorPoints[reactorSequence1[activeReactorsIndex]].GetComponent<ReactorWeakPoint>().Protect();
                reactorPoints[reactorSequence2[activeReactorsIndex]].GetComponent<ReactorWeakPoint>().Protect();
            }
        }
        else {
            foreach (GameObject g in reactorPoints) {
                if (vulnerability) g.GetComponent<ReactorWeakPoint>().UnProtect();
                else g.GetComponent<ReactorWeakPoint>().Protect();
            }
        }
    }

    public void PlayClosingHatch() {

    }

    public void StartBossPhase() {
        start = true;
    }

    public void ReactorDestroyed() {
        ++reactorDestroyedCount;
        if (reactorDestroyedCount >= 4) {
            // Finish sequence
        }
    }
}
