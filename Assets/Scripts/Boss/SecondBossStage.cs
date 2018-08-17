using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondBossStage : MonoBehaviour {


    public GameObject LeftWeakSpot, RightWeakSpot, Eye, player;
    public GameObject[] Turrets;
    public Vector3 targetAngle;
    private int WeakPointCounter;
    public Material Exposed;
    private bool start, rotateEye;
    private float lerpTime;
    private Vector3 initialRot;
    public float eyeTimeToMove = 5.0f;
    public float rotationSpeed = 3.0f;
    // Use this for initialization
    void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
        WeakPointCounter = 0;
        lerpTime = 0.0f;
        start = true;
        rotateEye = false;
        initialRot = Eye.transform.eulerAngles;
    }

    // Update is called once per frame
    void Update() {
        if (start) {
            foreach (GameObject g in Turrets) g.transform.LookAt(player.transform.position);
            if (rotateEye) {
                lerpTime += Time.unscaledDeltaTime / 1.0f;
                Eye.transform.eulerAngles = AngleLerp(initialRot, targetAngle, lerpTime * rotationSpeed);
                if (Mathf.Abs(Eye.transform.rotation.eulerAngles.x - targetAngle.x) < 0.01)
                {
                    //Eye.GetComponent<Renderer>().material = Exposed;
                    Eye.GetComponent<BossSecondPhaseWeakpoint>().ExposeWeakPoint();
                    rotateEye = false;
                }
            }
        }
    }

    public void WeakPointDone() {
        ++WeakPointCounter;
        if (WeakPointCounter >= 2) {
            GetComponentInParent<BossManager>().GoToNextPhase();

        }
    }

    public void StartBossPhase() {
        start = true;
        foreach (GameObject t in Turrets) t.GetComponent<BossTurretScript>().enabled = true;
    }

    public void StartBossPhase2() {
        lerpTime = 0.0f;
        rotateEye = true;
        Debug.Log("SecondPhase");
    }

    public void FinishBossPhase() {
        foreach (GameObject t in Turrets) t.GetComponent<BossTurretScript>().enabled = false;
        GetComponentInParent<BossManager>().GoToNextPhase();
    }

    Vector3 AngleLerp(Vector3 StartAngle, Vector3 FinishAngle, float t)
    {
        float xLerp = Mathf.LerpAngle(StartAngle.x, FinishAngle.x, t);
        float yLerp = Mathf.LerpAngle(StartAngle.y, FinishAngle.y, t);
        float zLerp = Mathf.LerpAngle(StartAngle.z, FinishAngle.z, t);
        Vector3 Lerped = new Vector3(xLerp, yLerp, zLerp);
        return Lerped;
    }
}
