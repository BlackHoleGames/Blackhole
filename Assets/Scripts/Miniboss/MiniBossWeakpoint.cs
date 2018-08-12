using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniBossWeakpoint : MonoBehaviour {

    public float life = 100.0f;
    public float hitFeedbackDuration = 0.25f;
    public Material matOn, matOff;
    public GameObject explosionPS;
    public TimeBehaviour tb;
    private bool hit, materialHitOn, alive;
    private float hitFeedbackCounter;
    private AudioSource  hitAudioSource;
    public GameObject destroyedBody, destroyedHead, body;
    public float destroyedTimer = 60.0f;
    private GameObject explosion;
    // Use this for initialization
    void Start () {
        hitAudioSource = GetComponent<AudioSource>();
        hit = false;
        materialHitOn = false;
        alive = true;
    }
	
	// Update is called once per frame
	void Update () {
        if (alive)
        {
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
        else {
            destroyedTimer -= Time.deltaTime;
            if (destroyedTimer < 1.0f && destroyedTimer > 0.0f) {
                foreach (Transform child in transform) {
                    Instantiate(Resources.Load("Explosion"), transform.position, transform.rotation);
                    Destroy(child);
                }
            }
            if (destroyedTimer <= 0.0f) Destroy(transform.parent.gameObject);

        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "PlayerProjectile" && alive)
        {
            hitAudioSource.Play();
            life -= other.gameObject.GetComponent<Projectile>().damage;
            hit = true;            
            if (life <= 0.0f) {
                SwitchToDestroy();
                alive = false;
                //Destroy(transform.parent.gameObject);
                ScoreScript.score = ScoreScript.score + (int)(100 * ScoreScript.multiplierScore);
            }
        }
    }

    public void SwitchToDestroy() {

        destroyedBody.SetActive(true);
        destroyedHead.SetActive(true);
        body.SetActive(false);
        //gameObject.SetActive(false);
        Destroy(GetComponent<SphereCollider>());
        GetComponent<Renderer>().enabled = false;
    }
}
