using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {


    public float speed;
    public float timeToLive = 10.0f;
    public float damage = 10.0f; 
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        timeToLive -= Time.deltaTime;
        if (timeToLive <= 0.0f) {
            Destroy(gameObject);
        }
        if(Time.timeScale > 0.0f) gameObject.transform.Translate(0.0f, 0.0f, speed*Time.unscaledDeltaTime);
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "Player" && other.gameObject.tag != "ghost" && other.gameObject.tag != "timeBubble"
            && other.gameObject.tag != "powerUp" && other.gameObject.tag != "EnemyProjectile" && other.gameObject.tag != "SquadManager"
            && other.gameObject.tag != "DodgeSection" && other.gameObject.tag != "BubbleObject" && other.gameObject.tag != "Particles" && other.gameObject.tag != "DeathLaser") {
            Instantiate(Resources.Load("PS_ProjectileHit"), transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
