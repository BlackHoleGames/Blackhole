using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperEnemy : MonoBehaviour {
    public float life = 10.0f;
    public float shotCooldown = 5.0f;
    public float shotDuration = 1.0f;
    public GameObject enemyProjectile;
    private GameObject player;
    public float spawnCooldown = 5.0f;
    public float hitFeedbackDuration = 0.25f;

    private float rateCounter, hitFeedbackCounter, wingTimeCounter, shotDurationCounter;
    public Material matOn, matOff;
    private bool shielded, playCharging,hit, materialHitOn, increaseWings, decreaseWings;
    private SquadManager squadManager;
    public GameObject wings;
    private TimeBehaviour tb;
    private AudioSource audioSource, hitAudioSource;
    public AudioClip gunshot;
    private EnemyLookAt ela;
    // Use this for initialization
    void Start() {
        player = GameObject.Find("Parent");
        audioSource = GetComponents<AudioSource>()[0];
        hitAudioSource = GetComponents<AudioSource>()[1];
        tb = gameObject.GetComponent<TimeBehaviour>();
        rateCounter = 0.0f;
        shielded = false;
        playCharging = false;
        gameObject.GetComponent<Renderer>().material = matOff;
        squadManager = GetComponentInParent<SquadManager>();
        audioSource.Play();
        audioSource.clip = gunshot;
        //shotTimeCounter = chargeTime;
        shotDurationCounter = shotDuration;
        wingTimeCounter = 0.0f;

        ela = GetComponent<EnemyLookAt>();

        foreach (Transform child in transform) {
            if (child.gameObject.name == "AlienSniperWings") {
                wings = child.gameObject;
                break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnCooldown > 0.0f) spawnCooldown -= Time.deltaTime * tb.scaleOfTime;
        else ManageShot();
    }
    /*
    public void ManageShot() {
        if (rateCounter <= 0.0f)
        {
            if (!playCharging)
            {
                playCharging = true;
                audioSource.Play();
                targetPos = player.transform.position;
            }
            //shotTimeCounter -= Time.deltaTime * tb.scaleOfTime;
            if (shotTimeCounter <= 0)
            {
                playCharging = false;
                //shotTimeCounter = chargeTime;
                //rateCounter = shotCooldown;
                Instantiate(enemyProjectile, transform.position, transform.rotation);
            }
            if ((increaseWings || decreaseWings) && !(increaseWings && decreaseWings)) ManageHit();
        }
        else rateCounter -= Time.deltaTime * tb.scaleOfTime;
    }*/

    public void ManageShot()
    {
        if (rateCounter <= 0.0f)
        {
            if (!playCharging)
            {
                ela.enabled = false;
                playCharging = true;
                audioSource.Play();
                increaseWings = true;
                GameObject laser = Instantiate(enemyProjectile, transform.position, transform.rotation);
                laser.transform.parent = transform;

            }
            if (shotDurationCounter <= 0)
            {
                playCharging = false;
                rateCounter = shotCooldown;
                shotDurationCounter = shotDuration;
                ela.enabled = true;
            }
            else {
                shotDurationCounter -= Time.deltaTime;
                if (shotDurationCounter <= 0.5f && !decreaseWings) decreaseWings = true;
            }
            if ((increaseWings || decreaseWings) && !(increaseWings && decreaseWings)) ManageWings();
        }
        else rateCounter -= Time.deltaTime * tb.scaleOfTime;
    }


    public void ManageHit() {
        if (materialHitOn)
        {
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

    public void ManageWings() {
        if (increaseWings) {
            wingTimeCounter += Time.deltaTime / 2.0f;
            float lerpedScaleUnit = Mathf.Lerp(0.5f, 1.0f, wingTimeCounter);
            Vector3 lerpedScaleVector = new Vector3(lerpedScaleUnit, lerpedScaleUnit, lerpedScaleUnit);
            wings.transform.localScale = lerpedScaleVector;
            if (lerpedScaleUnit >= 1) {
                increaseWings = false;
                wingTimeCounter = 0.0f;
            }
        }
        if (decreaseWings) {
            wingTimeCounter += Time.deltaTime / 0.5f;
            float lerpedScaleUnit = Mathf.Lerp(1.0f, 0.5f, wingTimeCounter);
            Vector3 lerpedScaleVector = new Vector3(lerpedScaleUnit, lerpedScaleUnit, lerpedScaleUnit);
            wings.transform.localScale = lerpedScaleVector;
            if (lerpedScaleUnit >= 1) {
                decreaseWings = false;
                wingTimeCounter = 0.0f;
            }
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
                Destroy(transform.parent.gameObject);
            }
        }
    }
}
