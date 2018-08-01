using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactorWeakPoint : MonoBehaviour {


    public float life = 30.0f;
    private bool isVulnerable,alive, hit, materialHitOn;
    public Material matOn, matOff;
	// Use this for initialization
	void Start () {
        isVulnerable = false;
        alive = true;
	}
	
	// Update is called once per frame
	void Update () {
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
                transform.parent.GetComponent<FirstBossStage>().ReactorDestroyed();
            }
            else life -= other.GetComponent<Projectile>().damage;
            
        }
    }
}
