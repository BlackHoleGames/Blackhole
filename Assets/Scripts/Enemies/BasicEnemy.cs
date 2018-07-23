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
    private bool shielded;
    private SquadManager squadManager;
    public GameObject explosionPS;
    public TimeBehaviour tb;
    private AudioSource audioSource, hitAudioSource;
    public AudioClip gunshot, explosion;
	// Use this for initialization
	void Start () {
        audioSource = GetComponents<AudioSource>()[0];
        hitAudioSource = GetComponents<AudioSource>()[1];
        tb = gameObject.GetComponent<TimeBehaviour>();
        shotCounter = numberOfShots;
        shotTimeCounter = rateOfFire;
        rateCounter = 0.0f;
        shielded = true;
        gameObject.GetComponent<Renderer>().material = matOn;
        squadManager = GetComponentInParent<SquadManager>();
        transform.parent.GetComponentInChildren<ProtectorEnemy>().squadron.Add(gameObject);
        audioSource.Play();
        audioSource.clip = gunshot;

    }

    // Update is called once per frame
    void Update () {
        if (spawnCooldown <= 0.0f) {
            if (rateCounter <= 0.0f) {
                if (shotCounter > 0)
                {
                    shotTimeCounter -= Time.deltaTime* tb.scaleOfTime;
                    if (shotTimeCounter <= 0) {
                        shotTimeCounter = rateOfFire;
                        --shotCounter;
                        Instantiate(enemyProjectile, transform.position, transform.rotation);
                        audioSource.Play();
                    }
                }
                else
                {
                    shotTimeCounter = rateOfFire;
                    shotCounter = numberOfShots;
                    rateCounter = shotCooldown;
                }
            }
            else rateCounter -= Time.deltaTime* tb.scaleOfTime;
        }
        else spawnCooldown -= Time.deltaTime* tb.scaleOfTime;
    }

    public void Unprotect() {
        shielded = false;
        gameObject.GetComponent<Renderer>().material = matOff;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PlayerProjectile")
        {
            hitAudioSource.Play();
            if (!shielded)  life -= other.gameObject.GetComponent<Projectile>().damage;
            if (life <= 0.0f)
            {
                Instantiate(explosionPS,gameObject.transform.position, gameObject.transform.rotation);
                squadManager.DecreaseNumber(explosion);
                Destroy(gameObject);
                ScoreScript.score = ScoreScript.score + (int)(100 * ScoreScript.multiplierScore);
            }
        }
    }
}
