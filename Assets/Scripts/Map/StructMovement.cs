using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StructMovement : MonoBehaviour {


    public float speed = 50.0f;
    private bool start = false;
    private MapManger mm;
	// Use this for initialization
	void Start () {
        mm = GameObject.Find("Managers").GetComponent<MapManger>();
    }

    // Update is called once per frame
    void Update () {
        if (start) {
            transform.Translate(new Vector3(0.0f, 0.0f, -speed * Time.deltaTime));
            if (transform.position.z < -5000.0f) {
                mm.GoToNextStage();
                Destroy(gameObject);
            }
        }
    }

    public void StartMovingStruct() {
        start = true;
    }

    public bool IsStructMoving() {
        return start;
    }
}
