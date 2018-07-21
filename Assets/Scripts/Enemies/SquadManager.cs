using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class SquadManager : MonoBehaviour {

    public int numOfMembers;
    public EnemyManager Manager;
    public float speed = 2.0f;
    private AudioSource explosion;
    private bool waitUntilExplosionEnded;
    private EnemyManager.SpawnPoint sp = EnemyManager.SpawnPoint.NOT_SET;
	// Use this for initialization
	void Start () {
        Manager = GameObject.FindGameObjectsWithTag("EnemyManager")[0].GetComponent<EnemyManager>();
        explosion = GetComponent<AudioSource>();
        waitUntilExplosionEnded = false;
        
	}
	
	// Update is called once per frame
	void Update () {
        if (sp != EnemyManager.SpawnPoint.NOT_SET)
        {
            switch (sp)
            {
                case EnemyManager.SpawnPoint.FRONT:
                    { 
                        if (gameObject.transform.position.z > 5.0f) gameObject.transform.Translate(new Vector3(0.0f, 0.0f, -Time.deltaTime * speed));
                        break;
                    }
                case EnemyManager.SpawnPoint.BACK:
                    {
                        if (gameObject.transform.position.z < 5.0f) gameObject.transform.Translate(new Vector3(0.0f, 0.0f, Time.deltaTime * speed));
                        if (gameObject.transform.position.z > 2.0f && gameObject.transform.position.y < 0.0f) gameObject.transform.Translate(new Vector3(0.0f, Time.deltaTime * speed/2.0f, 0.0f));

                        break;
                    }
                case EnemyManager.SpawnPoint.LEFT:
                    {
                        if (gameObject.transform.position.x < 0.0f) gameObject.transform.Translate(new Vector3( Time.deltaTime * speed, 0.0f, 0.0f));

                        break;
                    }
                case EnemyManager.SpawnPoint.RIGHT:
                    {
                        if (gameObject.transform.position.x > 0.0f) gameObject.transform.Translate(new Vector3( -Time.deltaTime * speed, 0.0f, 0.0f));

                        break;
                    }
                case EnemyManager.SpawnPoint.TOP:
                    {
                        if (gameObject.transform.position.y > 0.0f) gameObject.transform.Translate(new Vector3(0.0f, -Time.deltaTime * speed, 0.0f));

                        break;
                    }
                case EnemyManager.SpawnPoint.BOTTOM:
                    {
                        if (gameObject.transform.position.y < 0.0f) gameObject.transform.Translate(new Vector3(0.0f, Time.deltaTime * speed, 0.0f));

                        break;
                    }
            }
        }       
        if (waitUntilExplosionEnded)
        {
            if (!explosion.isPlaying)
            {
                Destroy(gameObject);
            }
        }
    }

    public void SetStartPoint(EnemyManager.SpawnPoint start) {
        sp = start;
    }

    public void DecreaseNumber() {
        explosion.Play();
        --numOfMembers;
        if (numOfMembers == 0)
        {
            waitUntilExplosionEnded = true;
            Manager.SpawnNext();
        }
    }
}
