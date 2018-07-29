using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEyeScript : MonoBehaviour {

    public float life = 30.0f;
    public float shotDecreaseTime = 0.25f;
    private float targetDistance;
    private bool approach = false;
    public Material matOn, matOff;
    public TimeBehaviour tb;
    private bool hit, materialHitOn, disabled;
    public float shotCooldown = 5.0f;
    public float rateOfFire = 0.2f;
    public int numberOfShots = 3;
    public GameObject enemyProjectile;
    private float rateCounter, shotTimeCounter, shotCounter;
    private ThirdBossStage tbs;
    // Use this for initialization
    void Start () {
        tb = gameObject.GetComponent<TimeBehaviour>();
        tbs = GetComponentInParent<ThirdBossStage>();
        disabled = false;
    }

    // Update is called once per frame
    void Update () {
        if (approach)
        {
            Debug.Log(Vector3.Distance(transform.position, transform.parent.transform.position));
            if (Vector3.Distance(transform.position, transform.parent.transform.position) > targetDistance)
            {
                if (transform.position.x != 0.0f)
                {
                    if (transform.position.x > 0.0f) transform.position += new Vector3(-Time.deltaTime, 0.0f, 0.0f);
                    if (transform.position.x < 0.0f) transform.position += new Vector3(Time.deltaTime, 0.0f, 0.0f);
                }
                if (transform.position.z != 0.0f)
                {
                    if (transform.position.z > 0.0f) transform.position += new Vector3(0.0f, 0.0f, -Time.deltaTime);
                    if (transform.position.z < 0.0f) transform.position += new Vector3(0.0f, 0.0f, Time.deltaTime);
                }
            }
            else
            {

                if (transform.position.x != 0.0f)
                {
                    if (transform.position.x > 0.0f) transform.position = new Vector3(targetDistance, transform.position.y, transform.position.z);
                    if (transform.position.x < 0.0f) transform.position = new Vector3(-targetDistance, transform.position.y, transform.position.z);
                }
                if (transform.position.z != 0.0f)
                {
                    if (transform.position.z > 0.0f) transform.position = new Vector3(transform.position.x, transform.position.y, targetDistance);
                    if (transform.position.z < 0.0f) transform.position = new Vector3(transform.position.x, transform.position.y, -targetDistance);
                }
                approach = false;
            }
        }
        if (rateCounter <= 0.0f)
        {
            if (shotCounter > 0)
            {
                shotTimeCounter -= Time.deltaTime * tb.scaleOfTime;
                if (shotTimeCounter <= 0)
                {
                    shotTimeCounter = rateOfFire;
                    --shotCounter;
                    Instantiate(enemyProjectile, transform.position, transform.rotation);
                }
            }
            else
            {
                shotTimeCounter = rateOfFire;
                shotCounter = numberOfShots;
                rateCounter = shotCooldown;
            }
        }
        else rateCounter -= Time.deltaTime * tb.scaleOfTime;
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

    public void DecreaseDistance(float distance) {
        approach = true;
        targetDistance = distance;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PlayerProjectile" && !disabled)
        {
            //hitAudioSource.Play();
            if (life > 0.0f)
            {
                life -= other.gameObject.GetComponent<Projectile>().damage;
                hit = true;
                if (life <= 0.0f) {
                    disabled = true;
                    gameObject.GetComponent<Renderer>().material = matOff;
                    tbs.EyeDefeated();
                    shotCooldown -= shotDecreaseTime;
                }
            }        
        }
    }
}
