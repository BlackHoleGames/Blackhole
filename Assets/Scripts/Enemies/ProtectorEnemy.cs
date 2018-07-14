using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtectorEnemy : MonoBehaviour {

    public float shield = 100.0f;
    public float life = 50.0f;
    public float multFactor = 0.0f;
    public float cooldown = 10.0f;
    public float sinFactor = 1.5f;
    public Material matOn,matOff;
    bool recovering;
    public List<GameObject> squadron;
    private float cooldownCounter;
    public SquadManager squadManager;
    public GameObject explosionPS;
    private TimeBehaviour tb;
	// Use this for initialization
	void Start () {
        recovering = false;
        cooldownCounter = cooldown;
        tb = gameObject.GetComponent<TimeBehaviour>();
        squadManager = GetComponentInParent<SquadManager>();
        foreach (BasicEnemy e in transform.parent.GetComponentsInChildren<BasicEnemy>()) {
            squadron.Add( e.gameObject);
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (!recovering)
        {
            if (shield < 100.0f) shield += Time.deltaTime * multFactor* tb.scaleOfTime;
            if (shield < 0.0f)
            {
                recovering = true;
                gameObject.GetComponent<Renderer>().material = matOff;
            }
        }
        else {
            if (life <= 0.0f) ActivateDeath();
            cooldownCounter -= Time.deltaTime * tb.scaleOfTime;
            if (cooldownCounter <= 0.0f)
            {
                cooldownCounter = cooldown;
                gameObject.GetComponent<Renderer>().material = matOff;
                shield = 100.0f;
                recovering = false;
            }
        }
	}

    public void ActivateDeath() {
        gameObject.GetComponent<Renderer>().material = matOff;
        foreach (GameObject g in squadron)
        {
            g.GetComponent<BasicEnemy>().Unprotect();
        }
        squadManager.DecreaseNumber();
        //Instantiate(explosionPS, new Vector3(0.0f,0.0f,0.0f), new Quaternion());
        Instantiate(explosionPS, gameObject.transform.position, gameObject.transform.rotation);
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PlayerProjectile")
        {
            if (!recovering) shield -= other.gameObject.GetComponent<Projectile>().damage;
            else life -= other.gameObject.GetComponent<Projectile>().damage;
            
        }
    }         
}
