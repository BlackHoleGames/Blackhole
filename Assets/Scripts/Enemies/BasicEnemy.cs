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
    public Material matOn, matOff, matFlicker;
    public GameObject enemyProjectile, enemyDestroyed;
    public TimeBehaviour tb;
    public AudioClip gunshot;
    private bool shielded, hit, materialHitOn;
    private float rateCounter, shotTimeCounter, shotCounter, hitFeedbackCounter;
    private AudioSource audioSource, hitAudioSource;
	// Use this for initialization
	void Start () {
        audioSource = GetComponents<AudioSource>()[0];
        hitAudioSource = GetComponents<AudioSource>()[1];
        tb = gameObject.GetComponent<TimeBehaviour>();
        shotCounter = numberOfShots;
        shotTimeCounter = rateOfFire;
        rateCounter = 0.0f;
        shielded = false;
        gameObject.GetComponent<Renderer>().material = matOn;
        //ProtectorEnemy pe = transform.parent.GetComponentInChildren<ProtectorEnemy>();
        //if (pe) pe.squadron.Add(gameObject);
        audioSource.Play();
        audioSource.clip = gunshot;
        gameObject.GetComponent<Renderer>().material = matOn;

    }

    // Update is called once per frame
    void Update () {
        //transform.LookAt(player.transform.position);
        if (spawnDelay <= 0.0f) ManageShot();
        else spawnDelay -= Time.deltaTime * tb.scaleOfTime;
        ManageEnemyHit();
    }

    public void Unprotect() {
        shielded = false;
        //gameObject.GetComponent<Renderer>().material = matOff;
    }

    public void ManageShot() {
        if (rateCounter <= 0.0f)
        {
            if (shotCounter > 0)
            {
                shotTimeCounter -= Time.deltaTime * tb.scaleOfTime;
                if (shotTimeCounter <= 0)
                {
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
        else rateCounter -= Time.deltaTime * tb.scaleOfTime;
    }

    public void ManageEnemyHit() {
        if (materialHitOn)
        {
            if (hitFeedbackCounter > 0.0f) hitFeedbackCounter -= Time.deltaTime;
            else
            {
                gameObject.GetComponent<Renderer>().material = matOn;
                materialHitOn = false;
                hitFeedbackCounter = hitFeedbackDuration;
            }
        }
        if (hit)
        {
            hit = false;
            gameObject.GetComponent<Renderer>().material = matFlicker;
            materialHitOn = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PlayerProjectile")
        {
            if (!hitAudioSource.enabled) hitAudioSource.enabled = true;
            hitAudioSource.Play();
            if (!shielded) {
                life -= other.gameObject.GetComponent<Projectile>().damage;
                hit = true;
            }
            if (life <= 0.0f)
            {
                GameObject obj  = Instantiate(Resources.Load("Explosion"), transform.position, transform.rotation) as GameObject;
                if (gameObject.name != "AlienArmored") obj.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                //Testing Plugin Vibrator GamePad.SetVibration(0, 0.0f, 2.0f);
                //Testing Plugin Vibrator GamePad.SetVibration(0, 0.0f, 0.0f);
                vibratorOn();
                ScoreScript.score = ScoreScript.score + (int)(100 * ScoreScript.multiplierScore);
                if (enemyDestroyed) enemyDestroyed.SetActive(true);
                Destroy(gameObject);
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
