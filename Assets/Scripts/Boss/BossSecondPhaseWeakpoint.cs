using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSecondPhaseWeakpoint : MonoBehaviour {

    public float life = 30.0f;
    public Material matOn, matOff;
    private bool hit, materialHitOn;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        ManageHit();
	}

    private void ManageHit()
    {
        if (materialHitOn)
        {
            gameObject.GetComponent<Renderer>().material = matOn;
            materialHitOn = false;
        }
        if (hit)
        {
            hit = false;
            gameObject.GetComponent<Renderer>().material = matOff;
            materialHitOn = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PlayerProjectile")
        {
            hit = true;
            if (life <= 0.0f)
            {
                transform.parent.GetComponent<SecondBossStage>().FinishBossPhase();
                GetComponent<Renderer>().material = matOff;
                Destroy(this);
            }
            else
            {
                life -= other.GetComponent<Projectile>().damage;
            }
        }
    }
}
