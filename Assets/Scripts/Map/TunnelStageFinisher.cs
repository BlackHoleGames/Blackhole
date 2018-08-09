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

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Managerspawning");

            mm.GoToNextStage();
            Destroy(transform.parent.gameObject);
        }
    }
}
