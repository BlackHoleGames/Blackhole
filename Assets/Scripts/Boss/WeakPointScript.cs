using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeakPointScript : MonoBehaviour {

    private SecondBossStage sbs;
    public Material matOn,matOff, hitMat;
    private bool hit, materialHitOn, done;
    public float life = 10.0f;
    public float lifeCounter;

    // Use this for initialization
    void Start () {
        sbs = transform.parent.GetComponent<SecondBossStage>();
        lifeCounter = life;
        done = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (!done)
        {
            if (lifeCounter < life) Regen();
            if (lifeCounter > 0.0f)
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
    }

    public void Regen()
    {
        lifeCounter += Time.deltaTime*2.5f;
        transform.position += new Vector3(0.0f, 0.0f, -Time.deltaTime*2.5f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PlayerProjectile")
        {
            hit = true;
            if (lifeCounter <= 0.0f)
            {
                done = true;
                sbs.WeakPointDone();
                GetComponent<Renderer>().material = matOff;
                Destroy(this);
            }
            else
            {
                lifeCounter -= 1.0f;
                transform.position += new Vector3(0.0f, 0.0f, 1.0f);
            }
        }
    }
}
