using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class oscilateCamera : MonoBehaviour {

    //public GameObject target = null;
    float timeCounter = 0;

    float speed, width, height, y,z;
    bool moveCamera = true;
    //public Camera cam;
    // Use this for initialization
    void Start()
    {
        speed = 2;
        width = 26;
        height = 26;
        y = 26.0f;
        z = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (moveCamera)
        {
            timeCounter += Time.deltaTime * speed;
            float x = 0;//Mathf.Cos(timeCounter) * width;
            z = Mathf.Cos(timeCounter) * width;
            y = Mathf.Sin(timeCounter) * height;
            //transform.LookAt(target.transform);
            //            transform.RotateAround(target.transform.position, Vector3.up, Time.deltaTime * 15);
            if(z<=0.0f)transform.position = new Vector3(x, y, z);
            if (y < -3.0f)
            {

                moveCamera = false;

            }
        }
    }
}
