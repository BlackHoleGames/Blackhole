using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KamikazeEnemy : MonoBehaviour {

    public float speed = 0.5f;
    public float turbospeed = 10.0f;
    public float life = 50.0f;
    public float hitFeedbackDuration = 0.25f;

    private TimeBehaviour tb;
    private GameObject player, enemyDestroyed;
    private float direction, hitFeedbackCounter;
    private bool turbo, hit, materialHitOn, alive;
    private AudioSource audioSource, hitAudioSource;
    public Material matOn, matHit;
    private SquadManager squadManager;

    // Use this for initialization
    void Start()
    {
        alive = true;
        squadManager = GetComponentInParent<SquadManager>();

        audioSource = GetComponents<AudioSource>()[0];
        hitAudioSource = GetComponents<AudioSource>()[1];

        turbo = false;
        if (GameObject.FindGameObjectWithTag("Player") != null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }else
        {
            player = GameObject.FindGameObjectWithTag("PlayerDestroyed");
        }
        if (transform.position.x < 0.0f) direction = 1.0f;
        else direction = -1.0f;
        tb = gameObject.GetComponent<TimeBehaviour>();
        audioSource.Play();

        //transform.parent.GetComponentInChildren<ProtectorEnemy>().squadron.Add(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.z < -15.0f) {
            squadManager.DecreaseNumber();
            Destroy(gameObject);
        }
        if ((Mathf.Abs(transform.position.x - player.transform.position.x ) < 0.1f) && !turbo)
        {
            turbo = true;
            gameObject.transform.parent = null;
        }
        if (!turbo) transform.position += new Vector3(speed * Time.deltaTime * direction * tb.scaleOfTime, 0.0f, 0.0f);
        else transform.position += new Vector3(0.0f, 0.0f, -turbospeed * Time.deltaTime * tb.scaleOfTime);
        ManageHit();
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
            gameObject.GetComponent<Renderer>().material = matHit;
            materialHitOn = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PlayerProjectile" && alive)
        {
            hitAudioSource.Play();
            life -= other.gameObject.GetComponent<Projectile>().damage;
            if (life <= 0.0f)
            {
                alive = false;
                Instantiate(Resources.Load("Explosion"), transform.position, transform.rotation);
                squadManager.DecreaseNumber();

                if (enemyDestroyed) enemyDestroyed.SetActive(true);
                Destroy(gameObject);
                ScoreScript.score = ScoreScript.score + (int)(1500 * ScoreScript.multiplierScore);
            }          
        }
        if (other.gameObject.tag == "Player" && alive) {
            Instantiate(Resources.Load("Explosion"), transform.position, transform.rotation);

            squadManager.DecreaseNumber();
            Destroy(gameObject);
        }
    }
}
