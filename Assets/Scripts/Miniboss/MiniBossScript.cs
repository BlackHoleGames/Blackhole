using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniBossScript : MonoBehaviour {

    private bool start;


    private EnemyManager em;
    public float life = 100.0f;
    public float shotCooldown = 5.0f;
    public float rateOfFire = 0.2f;
    public int numberOfShots = 3;
    public float hitFeedbackDuration = 0.05f;
    public float spawnDelay = 5.0f;
    public Material matOn, matOff;
    public GameObject enemyProjectile, destroyedBody;
    public AudioClip gunshot;
    private TimeBehaviour tb;
    private bool hit, materialHitOn, alive, secondPhase;
    private float rateCounter, shotTimeCounter, shotCounter, hitFeedbackCounter;
    private AudioSource audioSource, hitAudioSource;
    private float destructionDelayDuration = 1.5f;

    // Use this for initialization
    void Start () {
        em = GameObject.Find("Managers").GetComponentInChildren<EnemyManager>();
        audioSource = GetComponents<AudioSource>()[0];
        hitAudioSource = GetComponents<AudioSource>()[1];
        alive = true;
        secondPhase = false;
        tb = GetComponent<TimeBehaviour>();
        hit = false;
        materialHitOn = false;
        InitiateBoss();
    }
         
    // Update is called once per frame
    void Update () {
        if (start)
        {
            if (alive) {
                if (spawnDelay <= 0.0f) ManageShot();
                else spawnDelay -= Time.deltaTime * tb.scaleOfTime;
                ManageHit();
            }
            else {
                if (destructionDelayDuration <= 0) {
                    foreach (Transform child in destroyedBody.transform) {
                        Instantiate(Resources.Load("ExplosionBig"), child.position, child.rotation);
                        Destroy(child.gameObject);
                    }
                }
                else destructionDelayDuration -= Time.deltaTime;
            }
        }        
    }

    public void InitiateBoss() {
        start = true;
    }

    public bool MiniBossStarted() {
        return start;
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
                    GameObject fx = Instantiate(Resources.Load("PS_EnemyShoot"), transform) as GameObject;
                    fx.transform.eulerAngles += new Vector3(0.0f, 180.0f, 0.0f);
                    fx.transform.position += new Vector3(0.0f, 1.0f, -1.0f);
                    fx.transform.localScale = new Vector3(50.0f, 50.0f, 50.0f);
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

    public void ManageHit() {
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
            gameObject.GetComponent<Renderer>().material = matOff;
            materialHitOn = true;
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "PlayerProjectile" && alive) {
            hitAudioSource.Play();
            life -= other.gameObject.GetComponent<Projectile>().damage;
            hit = true;            
            if (life <= 0.0f) {
                GameObject goBody = Instantiate(Resources.Load("ExplosionMiniboss"), transform.position, transform.rotation) as GameObject;
                destroyedBody.SetActive(true);
                Destroy(GetComponent<SphereCollider>());
                GetComponent<Renderer>().enabled = false;
                GetComponent<MeshCollider>().enabled = false;
                alive = false;
                em.StartNextPhase();
                ScoreScript.score = ScoreScript.score + (int)(100 * ScoreScript.multiplierScore);
            }
        }
    }

    public bool IsSecondPhase() {
        return secondPhase;
    }


    public void StartSecondPhase() {
        secondPhase = true;
        transform.parent.gameObject.GetComponentInChildren<MiniBossWeakpoint>().StartMoveDown();

    }
}
