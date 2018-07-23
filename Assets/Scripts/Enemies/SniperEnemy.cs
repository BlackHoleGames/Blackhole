using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperEnemy : MonoBehaviour {
    public float life = 10.0f;
    public float shotCooldown = 5.0f;
    public float chargeTime = 1.0f;
    public GameObject enemyProjectile;
    public float spawnCooldown = 5.0f;
    private float rateCounter, shotTimeCounter;
    public Material matOn, matOff;
    private bool shielded, playCharging;
    private SquadManager squadManager;
    public GameObject explosionPS;
    private TimeBehaviour tb;
    private AudioSource audioSource, hitAudioSource;
    public AudioClip explosionClip, gunshot;
    // Use this for initialization
    void Start()
    {
        audioSource = GetComponents<AudioSource>()[0];
        hitAudioSource = GetComponents<AudioSource>()[1];
        tb = gameObject.GetComponent<TimeBehaviour>();
        shotTimeCounter = chargeTime;
        rateCounter = 0.0f;
        shielded = true;
        playCharging = false;
        gameObject.GetComponent<Renderer>().material = matOn;
        squadManager = GetComponentInParent<SquadManager>();
        transform.parent.GetComponentInChildren<ProtectorEnemy>().squadron.Add(gameObject);
        audioSource.Play();
        audioSource.clip = gunshot;
    }

    // Update is called once per frame
    void Update()
    {
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

                squadManager.DecreaseNumber(explosionClip);
                Destroy(gameObject);
            }
        }
    }
}
