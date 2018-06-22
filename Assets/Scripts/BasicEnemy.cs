using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : MonoBehaviour {

    public float life = 10.0f;
    public float shotCooldown = 5.0f;
    public float rateOfFire = 0.2f;
    public int numberOfShots = 3;
    public GameObject enemyProjectile;
    public float spawnCooldown = 5.0f;
    private float rateCounter, shotTimeCounter, shotCounter;
    public Material matOn, matOff;
    bool shielded;
    public SquadManager squadManager;
	// Use this for initialization
	void Start () {
        shotCounter = numberOfShots;
        shotTimeCounter = rateOfFire;
        rateCounter = 0.0f;
        shielded = true;
        gameObject.GetComponent<Renderer>().material = matOn;
	}
	
	// Update is called once per frame
	void Update () {
        if (spawnCooldown <= 0.0f) {
            if (rateCounter <= 0.0f) {
                if (shotCounter > 0)
                {
                    shotTimeCounter -= Time.deltaTime;
                    if (shotTimeCounter <= 0) {
                        shotTimeCounter = rateOfFire;
                        --shotCounter;
                        Instantiate(enemyProjectile, transform.position + transform.forward, transform.rotation);
                    }
                }
                else
                {
                    shotTimeCounter = rateOfFire;
                    shotCounter = numberOfShots;
                    rateCounter = shotCooldown;
                }
            }
            else rateCounter -= Time.deltaTime;
        }
        else spawnCooldown -= Time.deltaTime;
    }

    public void Unprotect() {
        shielded = false;
        gameObject.GetComponent<Renderer>().material = matOff;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PlayerProjectile")
        {
            if (!shielded)  life -= other.gameObject.GetComponent<Projectile>().damage;
            if (life <= 0.0f)
            {
                Destroy(gameObject);
                squadManager.DecreaseNumber();
            }
        }
    }
}
