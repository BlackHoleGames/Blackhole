using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEyeScript : MonoBehaviour {

    public float life = 300.0f;
    public float shotDecreaseTime = 0.25f;
    public float afterDefeatSpeed = 35.0f;
    public float rotationSpeed = 5.0f;
    public Material matOn, matOff;
    public TimeBehaviour tb;
    private bool hit, materialHitOn, disabled, goToExitPoint, goToEntryPoint;
    public float shotCooldown = 1.0f;
    public float rateOfFire = 0.25f;
    public float shotOffset = 0.2f;
    public float initialShotDelay;
    public float waitBeforeCharge;
    public int numberOfShots = 1;
    public GameObject enemyProjectile, explosion;
    public float rateCounter, shotTimeCounter, shotCounter;
    private ThirdBossStage tbs;
    private Vector3 exitTargetPos;
    public Transform KamikazeEntry, KamikazeExit;
    private AudioSource audioSource;
    private GameObject player;
    private float delay = 15.0f;
    private bool vulnerable;
    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        audioSource = GetComponent<AudioSource>();
        tb = gameObject.GetComponent<TimeBehaviour>();
        tbs = GetComponentInParent<ThirdBossStage>();
        disabled = false;
        vulnerable = false;
        rateCounter = initialShotDelay;
    }

    // Update is called once per frame
    void Update () {
        if (!disabled) {
            transform.eulerAngles += new Vector3(0.0f,0.0f, rotationSpeed* Time.deltaTime);
            
            ManageHit();
        }
        else {
            ManageExit();
        }
        if (delay < 0.0f) {
            if (!goToEntryPoint && !goToExitPoint) ManageShot();
        }
        else delay -= Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PlayerProjectile" && !disabled && vulnerable)
        {
            audioSource.Play();
            if (life > 0.0f) {
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

    private void ManageShot() {
        if (rateCounter <= 0.0f) {
            if (shotCounter > 0) {
                shotTimeCounter -= Time.deltaTime * tb.scaleOfTime;
                if (shotTimeCounter <= 0) {
                    shotTimeCounter = rateOfFire;
                    --shotCounter;
                    Instantiate(enemyProjectile, transform.position, transform.rotation);
                }
            }
            else {
                shotTimeCounter = rateOfFire;
                shotCounter = numberOfShots;
                rateCounter = shotCooldown;
            }
        }
        else rateCounter -= Time.deltaTime * tb.scaleOfTime;
    }

    private void ManageHit() {
        if (materialHitOn) {
            gameObject.GetComponent<Renderer>().material = matOn;
            materialHitOn = false;
        }
        if (hit) {
            hit = false;
            gameObject.GetComponent<Renderer>().material = matOff;
            materialHitOn = true;
        }
    }

    private void ManageExit() {
        if (goToEntryPoint) {
            if (Vector3.Distance(transform.position, KamikazeEntry.position) < 0.1f) {
                goToEntryPoint = false;
                goToExitPoint = true;
                transform.eulerAngles += new Vector3(0.0f,180.0f,0.0f);
                gameObject.GetComponent<Renderer>().material = matOn;
            }
            else transform.position = Vector3.MoveTowards(transform.position, KamikazeEntry.position, Time.deltaTime * afterDefeatSpeed * tb.scaleOfTime);
        }
        if (goToExitPoint) {
            if (waitBeforeCharge > 0.0f)
            {
                waitBeforeCharge -= Time.unscaledDeltaTime;
                if (waitBeforeCharge <= 0.0f) {
                    exitTargetPos = player.transform.position;
                    transform.LookAt(exitTargetPos);
                }
            }
            else
            {
                if (Vector3.Distance(transform.position, exitTargetPos) < 0.1f)
                {
                    tbs.EyeDestroyed();
                    Instantiate(Resources.Load("ExplosionEye"), transform.position, transform.rotation);
                    Destroy(gameObject);
                }
                else transform.position = Vector3.MoveTowards(transform.position, exitTargetPos, Time.deltaTime * afterDefeatSpeed * tb.scaleOfTime);
            }
        }
    }

    public void DecreaseShotTime() {
        rateCounter -= shotOffset;
    }

    public void StartExit() {
        goToEntryPoint = true;
    }

    Vector3 AngleLerp(Vector3 StartAngle, Vector3 FinishAngle, float t) {
        float xLerp = Mathf.LerpAngle(StartAngle.x, FinishAngle.x, t);
        float yLerp = Mathf.LerpAngle(StartAngle.y, FinishAngle.y, t);
        float zLerp = Mathf.LerpAngle(StartAngle.z, FinishAngle.z, t);
        Vector3 Lerped = new Vector3(xLerp, yLerp, zLerp);
        return Lerped;
    }

    public void UnProtect() {
        vulnerable = true;
    }
}
