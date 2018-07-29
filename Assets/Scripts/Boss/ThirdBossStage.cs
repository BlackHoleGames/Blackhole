using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdBossStage : MonoBehaviour {


    public GameObject parentAxis;
    public float offsetDistance;
    public GameObject[] eyes;
    public GameObject[] kamikazeEntrance;
    private float LerpTime;
    private bool Readjust;
    private Vector3 initialPos, targetPos;
    private int defeatedEyeCounter;
    public float initialDistance = 7.5f;
	// Use this for initialization
	void Start () {
        transform.position = parentAxis.transform.position;
        LerpTime = 0.0f;
        Readjust = false;
        defeatedEyeCounter = 0;
    }
	
	// Update is called once per frame
	void Update () {
        /*if (Readjust) AdjustBossLocation();
        if (Vector3.Distance(parentAxis.transform.position, transform.position) > 1  && !Readjust) {
            Readjust = true;
            initialPos = transform.position;
            targetPos = parentAxis.transform.position;
            LerpTime = 0.0f;
        }
        else {*/
            transform.position = parentAxis.transform.position;
            transform.Rotate(new Vector3(0.0f, 1.0f, 0.0f));
        //}
	}

    void AdjustBossLocation() {
        LerpTime += Time.deltaTime / 1.0f;
        transform.position = Vector3.Lerp(initialPos, parentAxis.transform.position, LerpTime);
        if (Vector3.Distance(parentAxis.transform.position, transform.position) < 0.5) Readjust = false;
    }

    public void EyeDefeated() {
        ++defeatedEyeCounter;
        if (defeatedEyeCounter >= 4){
            //DoSomething
        }
        else {
            initialDistance += -offsetDistance;
            foreach (GameObject g in eyes) {
                g.GetComponent<BossEyeScript>().DecreaseDistance(initialDistance);
            }
        }
    }
}
