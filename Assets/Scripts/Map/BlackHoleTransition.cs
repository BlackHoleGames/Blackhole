using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHoleTransition : MonoBehaviour {

    private MapManger mm;
    public float fadeToBlackCount = 2.0f;
    public bool startCount = false;
    private ChangeScene cs;
    private AudioManagerScript ams;
	// Use this for initialization
	void Start () {
        mm = GameObject.Find("Managers").GetComponent<MapManger>();
        cs = GameObject.FindGameObjectWithTag("UI_InGame").GetComponent<ChangeScene>();
        ams = GameObject.Find("AudioManager").GetComponent<AudioManagerScript>();
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
        if (!startCount)
        {
            startCount = true;
            ams.StopMusicInXSeconds(2.0f);
            cs.StartBlackHoleSequence();
            mm.NotifyEnteredBlackHole();
        }
    }
}
