using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TunnelStageFinisher : MonoBehaviour {

    private MapManger mm;
    private bool done;
    // Use this for initialization
    void Start()
    {
        mm = GameObject.Find("Managers").GetComponent<MapManger>();
        done = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !done)
        {
            done = true;
            mm.GoToNextStage();
            Destroy(transform.parent.gameObject);
        }
    }
}
