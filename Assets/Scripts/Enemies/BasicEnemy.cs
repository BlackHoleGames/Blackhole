using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;

public class BasicEnemy : MonoBehaviour {

    public float life = 10.0f;
    public float shotCooldown = 5.0f;
    public float rateOfFire = 0.2f;
    public int numberOfShots = 3;
    public float hitFeedbackDuration = 0.25f;
    public float spawnDelay = 5.0f;
    public Material matOn, matOff;
    public GameObject enemyProjectile, explosionPS;
    public TimeBehaviour tb;
    public AudioClip gunshot;
    private GameObject player;
    private bool shielded, hit, materialHitOn;
    private SquadManager squadManager;
    private float rateCounter, shotTimeCounter, shotCounter, hitFeedbackCounter;
    private AudioSource audioSource, hitAudioSource;
	// Use this for initialization
	void Start () {
        player = GameObject.Find("Parent");
        audioSource = GetComponents<AudioSource>()[0];
        hitAudioSource = GetComponents<AudioSource>()[1];
        tb = gameObject.GetComponent<TimeBehaviour>();
        shotCounter = numberOfShots;
        shotTimeCounter = rateOfFire;
        rateCounter = 0.0f;
        shielded = false;
        gameObject.GetComponent<Renderer>().material = matOn;
        squadManager = GetComponentInParent<SquadManager>();
        ProtectorEnemy pe = transform.parent.GetComponentInChildren<ProtectorEnemy>();
        if (pe) pe.squadron.Add(gameObject);
        audioSource.Play();
        audioSource.clip = gunshot;
        gameObject.GetComponent<Renderer>().material = matOff;

    }

    // Update is called once per frame
    void Update () {
        transform.LookAt(player.transform.position);
        if (spawnDelay <= 0.0f) {
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
        else spawnDelay -= Time.deltaTime* tb.scaleOfTime;
        if (materialHitOn) {
            if (hitFeedbackCounter > 0.0f) hitFeedbackCounter -= Time.deltaTime;
            else {
                gameObject.GetComponent<Renderer>().material = matOff;
                materialHitOn = false;
                hitFeedbackCounter = hitFeedbackDuration;
            }
        }
        if (hit)
        {
            hit = false;
            gameObject.GetComponent<Renderer>().material = matOn;
            materialHitOn = true;
        }


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
            if (!shielded) {
                life -= other.gameObject.GetComponent<Projectile>().damage;
                hit = true;
            }
            if (life <= 0.0f)
            {
                Instantiate(explosionPS,transform.position, transform.rotation);
                Instantiate(Resources.Load("Life_PointsPowerup"), transform.position,transform.rotation);
                squadManager.DecreaseNumber();
                //Testing Plugin Vibrator GamePad.SetVibration(0, 0.0f, 2.0f);
                //Testing Plugin Vibrator GamePad.SetVibration(0, 0.0f, 0.0f);
                vibratorOn();
                Destroy(gameObject);
                ScoreScript.score = ScoreScript.score + (int)(100 * ScoreScript.multiplierScore);
                
            }
        }
    }

    public void SetSpawnDelay(float newDelay) {
        spawnDelay = newDelay;
    }

    IEnumerable vibratorOn()
    {
        GamePad.SetVibration(0, 0.0f, 2.0f);
        yield return new WaitForSeconds(1.0f);
        GamePad.SetVibration(0, 0.0f, 0.0f);
    }
}
