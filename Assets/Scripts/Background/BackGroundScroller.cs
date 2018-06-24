using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundScroller : MonoBehaviour {

    public float scrollSpeed;//inferior a 1 para mover suave

    private Vector3 startPosition; //scala de y
    private float tileSize;//scala de y

    // Use this for initialization
    void Start () {
        startPosition = transform.position;
        tileSize = transform.localScale.y;
	}
	
	// Update is called once per frame
	void Update () {
        //Movimiento para la nueva posición de z
        float newPosition = Mathf.Repeat(Time.time * scrollSpeed, tileSize);//entre 0 y tilesize
        transform.position = startPosition + Vector3.forward * newPosition; //x = 0 , y =0 y z nueva posición

    }
}
