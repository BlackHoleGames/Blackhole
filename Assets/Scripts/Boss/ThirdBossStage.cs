using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdBossStage : MonoBehaviour {


    public GameObject parentAxis;
    public float offsetDistance;
    public GameObject[] eyes;
    public bool defeated;
    public BossManager bm;

    private int defeatedEyeCounter;
    private int destroyedEyeCounter;

    private Vector3 axis = Vector3.up;
    private Vector3 desiredPosition;
    public float radius = 7.5f;
    public float radiusSpeed = 0.5f;
    public float rotationSpeed = 80.0f;
    public bool start;
    private TimeBehaviour tb;
    // Use this for initialization
    void Start() {
        //transform.position = parentAxis.transform.position;
        defeatedEyeCounter = 0;
        destroyedEyeCounter = 0;
        start = true;
        defeated = false;
        tb = GetComponent<TimeBehaviour>();
    }

    // Update is called once per frame
    void Update() {
        if (start)
        {
            if (!defeated) {
                if (radius > 7.5f) {
                    radius -= Time.deltaTime*2.0f;
                    if (radius < 7.5f) {
                        radius = 7.5f;
                        foreach (GameObject e in eyes) {
                            e.GetComponentInChildren<BossEyeScript>().UnProtect();
                        }
                    }
                }
                ManageMovement();
            }
        }
    }

    /*public void ManageMovement() {
        transform.position = parentAxis.transform.position;
        foreach (GameObject g in eyes)
        {
            g.transform.RotateAround(transform.position, axis, rotationSpeed * Time.deltaTime);
            desiredPosition = (g.transform.position - transform.position).normalized * radius + transform.position;
            g.transform.position = Vector3.MoveTowards(g.transform.position, desiredPosition, Time.deltaTime * radiusSpeed);
        }
    }*/

    public void ManageMovement()
    {
        //transform.position = Vector3.Lerp(parentAxis.transform.position, (parentAxis.transform.position-transform.position) , Time.deltaTime * tb.scaleOfTime);
        foreach (GameObject g in eyes)
        {
            g.transform.RotateAround(transform.position, axis, rotationSpeed * Time.deltaTime * tb.scaleOfTime);
            desiredPosition = (g.transform.position - transform.position).normalized * radius + transform.position;
            g.transform.position = Vector3.MoveTowards(g.transform.position, desiredPosition, Time.deltaTime * radiusSpeed * tb.scaleOfTime);
        }
    }

    public void EyeDefeated() {
        ++defeatedEyeCounter;
        if (defeatedEyeCounter >= 4)
        {
            foreach (GameObject g in eyes) g.GetComponentInChildren<BossEyeScript>().StartExit();
            defeated = true;
        }
        else
        {
            foreach (GameObject g in eyes) g.GetComponentInChildren<BossEyeScript>().DecreaseShotTime();
            radius += -offsetDistance;
        }
    }

    public void StartBossPhase() {
        start = true;
    }

    public void EyeDestroyed(){
        ++destroyedEyeCounter;
        if (destroyedEyeCounter >= 4) {
            bm.BossDone();
            Destroy(gameObject);
        }
    }
}
