using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactorWeakPoint : MonoBehaviour {


    public float life = 30.0f;
    private bool isVulnerable,alive, hit, materialHitOn;
    public Material matOn, matOff, matHit;
    public GameObject reactor, destroyedReactor;
    public float hitFeedbackDuration = 0.25f;
    private float hitFeedbackCounter;
    // Use this for initialization
    void Start () {
        isVulnerable = false;
        alive = true;
	}
	
	// Update is called once per frame
	void Update () {
        ManageHit();
    }

    private void ManageHit() {
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

    public void Protect() {
        isVulnerable = false;
    }

    public void UnProtect() {
        isVulnerable = true;
    }

    public bool IsAlive() {
        return alive;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PlayerProjectile" && alive && isVulnerable)
        {
            if (life <= 0.0f)
            {
                // add a destroyed prefab                
                alive = false;
                gameObject.GetComponent<Renderer>().material = matOff;
                transform.parent.parent.GetComponent<FirstBossStage>().ReactorDestroyed();
                float rotZ = Random.Range(0.0f, 360.0f);
                destroyedReactor.SetActive(true);
                destroyedReactor.transform.eulerAngles = new Vector3(-180.0f, 0.0f, rotZ);
                Instantiate(Resources.Load("Explosion"), destroyedReactor.transform);
                Destroy(gameObject);
            }
            else {
                hit = true;
                life -= other.GetComponent<Projectile>().damage;
            }
        }
    }
}
