using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeRewindBody : MonoBehaviour {


    public bool rewinding = false;
    private List<PointInTime> points = new List<PointInTime>();
    private Rigidbody rb;
    public float recordingTime = 5.0f;
    public float timeBeforeRewind = 3.0f;
    public bool done = false;
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        if (timeBeforeRewind <= 0.0f)
        {
            rewinding = true;
            rb.isKinematic = true;
        }
        else
        {
            timeBeforeRewind -= Time.deltaTime;
            rb.isKinematic = false;
        }

    }

    private void FixedUpdate()
    {
        if (!done)
        {
            if (rewinding) Rewind();
            else Record();
        }
    }

    public void Rewind() {
        if (points.Count > 0) {
            PointInTime pointInTime = points[0];
            transform.position = pointInTime.position;
            transform.rotation = pointInTime.rotation;
            points.RemoveAt(0);
        }
        else {
            rewinding = false;
            done = true;
            //rb.isKinematic = false;
            //Call/Activate player

        }
    }

    public void Record() {
        if (points.Count > Mathf.Round(recordingTime / Time.fixedDeltaTime))
        {
            points.RemoveAt(points.Count - 1);
        }
        points.Insert(0, new PointInTime(transform.position, transform.rotation));
    }

    public class PointInTime
    {

        public Vector3 position;
        public Quaternion rotation;

        public PointInTime(Vector3 _position, Quaternion _rotation)
        {
            position = _position;
            rotation = _rotation;
        }

    }

}
