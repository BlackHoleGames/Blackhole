using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniBossScript : MonoBehaviour {

    private bool start;

    private float testTime = 10.0f;
    private EnemyManager em;
    private MapManger mm;

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
    private bool hit, materialHitOn, alive, secondPhase;
    private float rateCounter, shotTimeCounter, shotCounter, hitFeedbackCounter;
    private AudioSource audioSource, hitAudioSource;



    // Use this for initialization
    void Start () {
        em = GameObject.Find("Managers").GetComponentInChildren<EnemyManager>();
        player = GameObject.Find("Parent");
        audioSource = GetComponents<AudioSource>()[0];
        hitAudioSource = GetComponents<AudioSource>()[1];
        alive = true;
        secondPhase = false;
    }
         
    // Update is called once per frame
    void Update () {
        transform.LookAt(player.transform.position);
        if (start)
        {
            if (spawnDelay <= 0.0f)
            {
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
            else spawnDelay -= Time.deltaTime * tb.scaleOfTime;
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
	}

    public void InitiateBoss() {
        start = true;
    }

    public bool MiniBossStarted() {
        return start;
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "PlayerProjectile" && alive) {
            hitAudioSource.Play();
            life -= other.gameObject.GetComponent<Projectile>().damage;
            hit = true;            
            if (life <= 0.0f) {
                alive = false;
                em.StartNewPhase();
                ScoreScript.score = ScoreScript.score + (int)(100 * ScoreScript.multiplierScore);
            }
        }
    }

    public bool IsSecondPhase() {
        return secondPhase;
    }


    public void StartSecondPhase() {
        secondPhase = true;
        transform.parent.gameObject.GetComponent<MiniBossWeakpoint>().StartWeakPoint();
    }
}
