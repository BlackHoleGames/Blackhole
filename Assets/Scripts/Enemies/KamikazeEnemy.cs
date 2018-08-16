using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KamikazeEnemy : MonoBehaviour {

    public float speed = 0.5f;
    public float turbospeed = 10.0f;
    public float life = 50.0f;
    public float hitFeedbackDuration = 0.25f;

    private SquadManager squadManager;
    private TimeBehaviour tb;
    private GameObject player;
    private float direction, hitFeedbackCounter;
    private bool turbo, hit, materialHitOn;
    private AudioSource audioSource, hitAudioSource;
    public Material matOn, matHit;

    // Use this for initialization
    void Start()
    {
        audioSource = GetComponents<AudioSource>()[0];
        hitAudioSource = GetComponents<AudioSource>()[1];

        turbo = false;
        player = GameObject.FindGameObjectWithTag("Player");
        if (transform.position.x < 0.0f) direction = 1.0f;
        else direction = -1.0f;
        tb = gameObject.GetComponent<TimeBehaviour>();
        squadManager = GetComponentInParent<SquadManager>();
        audioSource.Play();

        //transform.parent.GetComponentInChildren<ProtectorEnemy>().squadron.Add(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(transform.position.x - player.transform.position.x) < 0.1f) turbo = true;
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
        if (other.gameObject.tag == "PlayerProjectile")
        {
            hitAudioSource.Play();
            life -= other.gameObject.GetComponent<Projectile>().damage;
            if (life <= 0.0f)
            {
                //squadManager.DecreaseNumber();
                Instantiate(Resources.Load("Explosion"), transform.position, transform.rotation);
                Destroy(gameObject);
            }
           
        }
        if (other.gameObject.tag == "Player") {
            //squadManager.DecreaseNumber();
            Instantiate(Resources.Load("Explosion"), transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
