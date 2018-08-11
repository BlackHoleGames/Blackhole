using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniBossWeakpoint : MonoBehaviour {

    public float life = 10.0f;
    public float hitFeedbackDuration = 0.25f;
    public Material matOn, matOff;
    public GameObject explosionPS;
    public TimeBehaviour tb;
    private bool hit, materialHitOn;
    private float hitFeedbackCounter;
    private AudioSource audioSource, hitAudioSource;

    // Use this for initialization
    void Start () {
        audioSource = GetComponents<AudioSource>()[0];
        hitAudioSource = GetComponents<AudioSource>()[1];
    }
	
	// Update is called once per frame
	void Update () {
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

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "PlayerProjectile")
        {
            hitAudioSource.Play();
            life -= other.gameObject.GetComponent<Projectile>().damage;
            hit = true;            
            if (life <= 0.0f)
            {
                Destroy(transform.parent.gameObject);
                ScoreScript.score = ScoreScript.score + (int)(100 * ScoreScript.multiplierScore);
            }
        }
    }

    public void StartWeakPoint() {

    }

}
