using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDestroyed : MonoBehaviour {

    public float timeBeforeDestroy = 1.5f;
    public float flickerTime = 0.1f;
    private bool matOn;
    private float flickerCounter;
    public Material matOff, matFlicker;
    private SquadManager squadManager;
    private GameObject eye;
    // Use this for initialization
    void Start() {
        squadManager = GetComponentInParent<SquadManager>();
        foreach (Transform child in transform)
        {
            if (child.name == "AlienEye") {
                eye = child.gameObject;
                Instantiate(Resources.Load("TimeBubble"), eye.transform.position, eye.transform.rotation);
                break;
            }
        }
        transform.parent = null;
        float randomYRot = Random.Range(0.0f,360.0f);
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, randomYRot, transform.eulerAngles.z);
    }
        // Update is called once per frame
    void Update () {
        timeBeforeDestroy -= Time.deltaTime;
        if (timeBeforeDestroy < 0.0f) CallDestroy();
        if (flickerCounter < 0.0f) {
            foreach (Transform child in transform)
            {
                if (!matOn)
                {
                    child.GetComponent<Renderer>().material = matFlicker;
                    matOn = true;
                }
                else
                {
                    child.GetComponent<Renderer>().material = matOff;
                    matOn = false;
                }
            }
            flickerCounter = flickerTime;
        }
        else flickerCounter -= Time.deltaTime;
	}

    public void CallDestroy() {
        foreach (Transform child in transform)
        {
            GameObject obj = Instantiate(Resources.Load("Explosion"), child.position, child.rotation) as GameObject;
            if (child.name != "AlienEye") obj.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            Destroy(child.gameObject);
        }
        squadManager.DecreaseNumber();
        Destroy(gameObject);
    }
}
