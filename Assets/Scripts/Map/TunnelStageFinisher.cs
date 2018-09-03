using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TunnelStageFinisher : MonoBehaviour {

    private MapManger mm;
    // Use this for initialization
    void Start()
    {
        mm = GameObject.Find("Managers").GetComponent<MapManger>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.z < -10.0f) {
            mm.GoToNextStage();
            Destroy(transform.parent.gameObject);
        }
    
    }

    /*private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !done)
        {
            done = true;
            mm.GoToNextStage();
            Destroy(transform.parent.gameObject);
        }
    }*/
}
