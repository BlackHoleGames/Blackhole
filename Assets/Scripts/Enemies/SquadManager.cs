using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquadManager : MonoBehaviour {

    public int numOfMembers;
    public EnemyManager Manager;
    public float speed = 2.0f;
	// Use this for initialization
	void Start () {
        Manager = GameObject.FindGameObjectsWithTag("EnemyManager")[0].GetComponent<EnemyManager>();
	}
	
	// Update is called once per frame
	void Update () {
        if (gameObject.transform.position.z > 5.0f) {
            gameObject.transform.Translate(new Vector3(0.0f,0.0f,-Time.deltaTime*speed));
        }
	}

    public void DecreaseNumber() {
        --numOfMembers;
        if (numOfMembers <= 0) {
            Manager.SpawnNext();
            Destroy(gameObject);
        }
    }
}
