using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StructEnemyStageTunnel : MonoBehaviour {

    public float speed = 100.0f;
    public GameObject attachedTo;
    public BattleTunnelManager btm;
    private bool stopRespawn = false;
    private bool toReadjust = false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (transform.position.z <= -25.0f)
        {
            toReadjust = true;
            if (stopRespawn) Destroy(gameObject);
            else transform.position = attachedTo.transform.position + new Vector3(0.0f, 0.0f, 100.0f);
        }
        else {
            if (toReadjust && (Vector3.Distance(transform.position, attachedTo.transform.position) - 100.0f) > 0.1f)
            {
                transform.position = attachedTo.transform.position + new Vector3(0.0f, 0.0f, 100.0f);
                if ((Vector3.Distance(transform.position, attachedTo.transform.position) - 100.0f) <= 0.1f) toReadjust = false;
            }
            transform.Translate(new Vector3(0.0f, 0.0f, speed * Time.deltaTime* btm.actualScale));
        }
	}

    public void FinishSequence() {
        stopRespawn = true;
    }
}
