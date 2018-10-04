using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleTunnelManager : MonoBehaviour {

    public float actualScale = 1.0f;
    private TimeBehaviour tb;
	// Use this for initialization
	void Start () {
        tb = GetComponent<TimeBehaviour>();
	}
	
	// Update is called once per frame
	void Update () {
        actualScale = tb.scaleOfTime;
	}
}
