using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadialShooter : MonoBehaviour {

    public float life = 10.0f;
    public float shotCooldown = 5.0f;
    public float rateOfFire = 0.2f;
    public int numberOfShots = 3;
    public float hitFeedbackDuration = 0.25f;

    public GameObject enemyProjectile;
    public float spawnCooldown = 5.0f;
    private float rateCounter, shotTimeCounter, hitFeedbackCounter; //, shotCounter;
    public float degreesPerProjectile;
    public Material matOn, matOff;
    bool shielded, hit, materialHitOn;

    public GameObject explosionPS;
    private TimeBehaviour tb;
    private AudioSource audioSource, hitAudioSource;
    public AudioClip gunshot;
    private SquadManager squadManager;
    // Use this for initialization
    void Start()
    {
        squadManager = GetComponentInParent<SquadManager>();
        audioSource = GetComponents<AudioSource>()[0];
        hitAudioSource = GetComponents<AudioSource>()[1];
        tb = gameObject.GetComponent<TimeBehaviour>();
        //shotCounter = numberOfShots;
        shotTimeCounter = rateOfFire;
        rateCounter = 0.0f;
        shielded = false;
        gameObject.GetComponent<Renderer>().material = matOn;
        ProtectorEnemy pe = transform.parent.GetComponentInChildren<ProtectorEnemy>();
        if (pe) pe.squadron.Add(gameObject);
        degreesPerProjectile = 360.0f / (float)numberOfShots;
        audioSource.Play();
        audioSource.clip = gunshot;
    }

    // Update is called once per frame
    void Update()
    {
        ManageShot();
        ManageHit();        
    }

    public void ManageShot() {
        if (spawnCooldown <= 0.0f)
        {
            if (rateCounter <= 0.0f)
            {
                audioSource.Play();
                for (int i = 0; i < numberOfShots; ++i)
                {
                    shotTimeCounter = rateOfFire;
                    Instantiate(enemyProjectile, transform.position, Quaternion.Euler(new Vector3(0.0f, degreesPerProjectile * i, 0.0f)));
                }
                rateCounter = shotCooldown;
            }
            else rateCounter -= Time.deltaTime * tb.scaleOfTime;
        }
        else spawnCooldown -= Time.deltaTime * tb.scaleOfTime;
    }

    public void ManageHit() {
        if (materialHitOn)
        {
            if (hitFeedbackCounter > 0.0f) hitFeedbackCounter -= Time.deltaTime;
            else
            {
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

    public void Unprotect()
    {
        shielded = false;
        gameObject.GetComponent<Renderer>().material = matOff;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PlayerProjectile")
        {
            hitAudioSource.Play();
            if (!shielded) life -= other.gameObject.GetComponent<Projectile>().damage;
            if (life <= 0.0f)
            {
                Instantiate(Resources.Load("Explosion"), transform.position, transform.rotation);
                squadManager.DecreaseNumber();

                Destroy(gameObject);
                ScoreScript.score = ScoreScript.score + (int)(1500 * ScoreScript.multiplierScore);
            }
        }
    }
}
