using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeakPointScript : MonoBehaviour {

    private SecondBossStage sbs;
    public Material matOn,matOff, hitMat;
    private bool hit, materialHitOn;
    public float life = 100.0f;
	// Use this for initialization
	void Start () {
        sbs = transform.parent.GetComponent<SecondBossStage>();
	}
	
	// Update is called once per frame
	void Update () {
        if (life > 0.0f)
        {
            if (materialHitOn)
            {
                gameObject.GetComponent<Renderer>().material = matOn;
                materialHitOn = false;
            }
            if (hit)
            {
                hit = false;
                gameObject.GetComponent<Renderer>().material = hitMat;
                materialHitOn = true;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PlayerProjectile")
        {
            hit = true;
            if (life <= 0.0f)
            {
                sbs.WeakPointDone();
                gameObject.GetComponent<Renderer>().material = matOff;
                Destroy(this);
            }
            else {
                life -= other.GetComponent<Projectile>().damage;
            }
        }
    }
}
