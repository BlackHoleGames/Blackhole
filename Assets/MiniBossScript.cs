using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniBossScript : MonoBehaviour {

    private bool start;

    private float testTime = 10.0f;
    private EnemyManager em;
	// Use this for initialization
	void Start () {
        em = GameObject.Find("Managers").GetComponentInChildren<EnemyManager>();
	}
	
	// Update is called once per frame
	void Update () {
        if (start) {
            testTime -= Time.unscaledDeltaTime;
            if (testTime <= 0.0f) {
                em.StartNewPhase();
                Destroy(gameObject);
            }
        }
	}

    public void InitiateBoss() {
        start = true;
    }
}
