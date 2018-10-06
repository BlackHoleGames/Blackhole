using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTurretScript : MonoBehaviour {

    public float shotCooldown = 6.0f;
    public float rateOfFire = 0.2f;
    public int numberOfShots = 3;
    public GameObject enemyProjectile;
    public float rateCounter;
    private float shotTimeCounter, shotCounter;
    private GameObject player;

    // Use this for initialization
    void Start () {
        shotCounter = numberOfShots;
        shotTimeCounter = rateOfFire;
        player = GameObject.FindGameObjectWithTag("Player");
    }
	
	// Update is called once per frame
	void Update () {
        transform.LookAt(player.transform.position);
        ManageShot();
    }

    public void ManageShot() {
        if (rateCounter <= 0.0f) {
            if (shotCounter > 0) {
                shotTimeCounter -= Time.deltaTime;
                if (shotTimeCounter <= 0) {
                    shotTimeCounter = rateOfFire;
                    --shotCounter;
                    Instantiate(enemyProjectile, transform.position, transform.rotation);
                }
            }
            else {
                shotTimeCounter = rateOfFire;
                shotCounter = numberOfShots;
                rateCounter = shotCooldown;
            }
        }
        else rateCounter -= Time.deltaTime;
    }

    public void SetRateCounter(float newValue) {
        rateCounter = newValue;
    }
}
