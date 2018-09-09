using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstBossStage : MonoBehaviour {

    public float limitX = 9.52f;  
    public float timeBetweenReactorShots = 3.0f;
    public float shotDuration = 5.0f;
    public float windowOfOportunityDuration = 1.0f;
    public float speed = 2.0f;
    public float downwardSpeed = 0.05f;
    public float heightLimit = 1.0f;
    public float timeToMoveUp = 3.0f;
    public float initialHeight = 5.0f;
    public float maxOffsetY = 5.0f;
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
        initialPos = transform.parent.transform.position;
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
                        ++activeReactorsIndex;
                    }
                    else {
                        if (shotDone) {
                            windowOfOportunityCounter = windowOfOportunityDuration;
                            timeBetweenShotsCounter = timeBetweenReactorShots;
                            shotDone = false;
                            openingHatch = false;
                            SwitchVulnerabilityOnReactors(false);

                        }
                    }
                }
            }
        }
	}

    public void ManageMovement() {
        if (readjustHeight) {
            lerpTime += Time.deltaTime / timeToMoveUp;
            transform.parent.transform.position = Vector3.Lerp(initialPos, new Vector3(transform.parent.transform.position.x, transform.parent.transform.position.y, initialHeight), lerpTime);
            if (Mathf.Abs(transform.parent.transform.position.z - initialHeight) < 0.01) readjustHeight = false;
        }
        else {
            if (direction > 0) {
                if (transform.parent.transform.position.x > limitX) direction = -1;
            }
            else {
                if (transform.parent.transform.position.x < -limitX) direction = 1;
            }
            float offsetY = 0;
            if (transform.position.z > heightLimit) offsetY = downwardSpeed * Time.deltaTime *10.0f* tb.scaleOfTime;
            if (transform.position.z < heightLimit && (transform.position.x <= 1.0f && transform.position.x >= -1.0f)) {
                readjustHeight = true;
                lerpTime = 0.0f;
                initialPos = transform.position;
            }
            else {
                transform.parent.transform.position += new Vector3(speed * Time.deltaTime * tb.scaleOfTime* direction, 0.0f, -offsetY);
            }
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
        if (reactorPoints[index]) {
            GameObject obj = Instantiate(reactorShot, reactorPoints[index].transform.position, Quaternion.Euler(new Vector3(-90.0f, 0.0f, 0.0f)));
            obj.transform.parent = reactorPoints[index].transform;
            obj.GetComponent<BoxCollider>().size = new Vector3(1.6f, 29.0f, 1.6f);
        }
        //obj.GetComponent<BossLaser>().StartBehaviour(shotDuration);
    }

    public void PlayOpeningHatch() {

    }

    public void SwitchVulnerabilityOnReactors(bool vulnerability) {
        if (activeReactorsIndex < reactorSequence1.Length) {
            GameObject reactor1 = reactorPoints[reactorSequence1[activeReactorsIndex]];
            GameObject reactor2 = reactorPoints[reactorSequence2[activeReactorsIndex]];
            if (vulnerability) {
                if (reactor1) reactor1.GetComponent<ReactorWeakPoint>().UnProtect();
                if (reactor2) reactor2.GetComponent<ReactorWeakPoint>().UnProtect();
            }
            else {
                if (reactor1) reactor1.GetComponent<ReactorWeakPoint>().Protect();
                if (reactor2) reactor2.GetComponent<ReactorWeakPoint>().Protect();
            }
        }
        else {
            foreach (GameObject g in reactorPoints) {
                if (g) {
                    if (vulnerability) g.GetComponent<ReactorWeakPoint>().UnProtect();
                    else g.GetComponent<ReactorWeakPoint>().Protect();
                }
            }
        }
    }

    public void PlayClosingHatch() {

    }

    public void StartBossPhase() {
        start = true;
        initialHeight = transform.position.z;
        heightLimit = transform.position.z - maxOffsetY;
    }

    public void ReactorDestroyed() {
        ++reactorDestroyedCount;
        if (reactorDestroyedCount >= 4) {
            GetComponentInParent<BossManager>().GoToNextPhase();
            start = false;
            Destroy(this);
        }
    }
}
