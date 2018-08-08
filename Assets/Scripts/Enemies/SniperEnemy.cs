using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperEnemy : MonoBehaviour {
    public float life = 10.0f;
    public float shotCooldown = 5.0f;
    public float chargeTime = 1.0f;
    public GameObject enemyProjectile, player;
    public float spawnCooldown = 5.0f;
    public float hitFeedbackDuration = 0.25f;

    private float rateCounter, shotTimeCounter, hitFeedbackCounter;
    public Material matOn, matOff;
    private bool shielded, playCharging,hit, materialHitOn;
    private SquadManager squadManager;
    public GameObject explosionPS;
    private TimeBehaviour tb;
    private AudioSource audioSource, hitAudioSource;
    public AudioClip gunshot;
    // Use this for initialization
    void Start() {
        player = GameObject.Find("Parent");
        audioSource = GetComponents<AudioSource>()[0];
        hitAudioSource = GetComponents<AudioSource>()[1];
        tb = gameObject.GetComponent<TimeBehaviour>();
        shotTimeCounter = chargeTime;
        rateCounter = 0.0f;
        shielded = false;
        playCharging = false;
        gameObject.GetComponent<Renderer>().material = matOff;
        squadManager = GetComponentInParent<SquadManager>();
        ProtectorEnemy pe = transform.parent.GetComponentInChildren<ProtectorEnemy>();

        if (pe) pe.squadron.Add(gameObject);
        audioSource.Play();
        audioSource.clip = gunshot;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(player.transform.position);
        if (spawnCooldown <= 0.0f)
        {
            if (rateCounter <= 0.0f)
            {
                if (!playCharging) {
                    playCharging = true;
                    audioSource.Play();
                }
                shotTimeCounter -= Time.deltaTime * tb.scaleOfTime;
                if (shotTimeCounter <= 0)
                {
                    playCharging = false;
                    shotTimeCounter = chargeTime;
                    rateCounter = shotCooldown;
                    Instantiate(enemyProjectile, transform.position, transform.rotation);
                }
            }
            else rateCounter -= Time.deltaTime * tb.scaleOfTime;
        }
        else spawnCooldown -= Time.deltaTime * tb.scaleOfTime;
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
                Instantiate(explosionPS, gameObject.transform.position, gameObject.transform.rotation);

                squadManager.DecreaseNumber();
                Destroy(gameObject);
            }
        }
    }
}
