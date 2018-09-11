using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHoleTransition : MonoBehaviour {

    private MapManger mm;
    private float fadeToBlackCount = 2.0f;
    public bool startCount = false;
    private ChangeScene cs;
	// Use this for initialization
	void Start () {
        mm = GameObject.Find("Managers").GetComponent<MapManger>();
        cs = GameObject.FindGameObjectWithTag("UI_InGame").GetComponent<ChangeScene>();
	}
	
	// Update is called once per frame
	void Update () {
        if (startCount) {
            fadeToBlackCount -= Time.deltaTime;
            if (fadeToBlackCount <= 0.0f)
            {
                mm.NotifyGameBlackScreen();
                startCount = false;
                Destroy(gameObject);
            }
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        startCount = true;
        cs.StartBlackHoleSequence();
    }
}
