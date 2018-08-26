using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactorWeakPoint : MonoBehaviour {


    public float life = 30.0f;
    private bool isVulnerable,alive, hit, materialHitOn;
    public Material matOn, matOff, matHit;
    public GameObject reactor, destroyedReactor;
	// Use this for initialization
	void Start () {
        isVulnerable = false;
        alive = true;
	}
	
	// Update is called once per frame
	void Update () {
        
    }

    private void ManageHit() {
        if (materialHitOn)
        {
            gameObject.GetComponent<Renderer>().material = matOff;
            materialHitOn = false;
        }
        if (hit)
        {
            hit = false;
            gameObject.GetComponent<Renderer>().material = matOn;
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
            if (life <= 0.0f) {
                // add a destroyed prefab
                hit = true;
                alive = false;
                gameObject.GetComponent<Renderer>().material = matOff;
                transform.parent.parent.GetComponent<FirstBossStage>().ReactorDestroyed();                
                destroyedReactor.SetActive(true);
                Instantiate(Resources.Load("Explosion"), destroyedReactor.transform);
                Destroy(gameObject);
            }
            else life -= other.GetComponent<Projectile>().damage;
            
        }
    }
}
