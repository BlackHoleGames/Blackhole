using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidStageFinisher : MonoBehaviour {

    private MapManger mm;
    public float delayBeforeDestroy = 2.0f;
    private bool count;
    // Use this for initialization
    void Start () {
        mm = GameObject.Find("Managers").GetComponent<MapManger>() ;
        count = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (count) {
            delayBeforeDestroy -= Time.deltaTime;
            if (delayBeforeDestroy < 0.0f) CallDestruction();
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !count) {
            mm.GoToNextStage();
            count = true;
            //Destroy(transform.parent.gameObject);
        }
    }

    public void CallDestruction() {
        Destroy(transform.root.gameObject);
    }
}
