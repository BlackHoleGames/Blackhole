using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEyeScript : MonoBehaviour {

    public float life = 30.0f;
    public float shotDecreaseTime = 0.25f;
    public float afterDefeatSpeed = 2.0f;
    public float rotationSpeed = 1.0f;
    public Material matOn, matOff;
    public TimeBehaviour tb;
    private bool hit, materialHitOn, disabled, orienting, goToExitPoint, goToEntryPoint;
    public float shotCooldown = 5.0f;
    public float rateOfFire = 0.2f;
    public float shotOffset = 0.2f;
    public float waitBeforeCharge;
    public int numberOfShots = 3;
    public GameObject enemyProjectile, explosion, player;
    private float rateCounter, shotTimeCounter, shotCounter;
    private ThirdBossStage tbs;
    private Vector3 initialOrientation, orientationTarget;
    public Transform KamikazeEntry, KamikazeExit;
    // Use this for initialization
    void Start () {
        tb = gameObject.GetComponent<TimeBehaviour>();
        tbs = GetComponentInParent<ThirdBossStage>();
        disabled = false;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update () {
        if (!disabled) {
            transform.eulerAngles += new Vector3(0.0f,0.0f, rotationSpeed* Time.deltaTime);
            ManageShot();
            ManageHit();
        }
        else {
            ManageExit();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PlayerProjectile" && !disabled)
        {
            //hitAudioSource.Play();
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
                orienting = true;
                goToEntryPoint = false;
                goToExitPoint = true;
                transform.LookAt(KamikazeExit);
                transform.eulerAngles += new Vector3(0.0f,180.0f,0.0f);
                gameObject.GetComponent<Renderer>().material = matOn;
            }
            else transform.position = Vector3.MoveTowards(transform.position, KamikazeEntry.position, Time.deltaTime * afterDefeatSpeed);
        }
        if (goToExitPoint) {
            if (waitBeforeCharge > 0.0f) waitBeforeCharge -= Time.unscaledDeltaTime;
            else {
                if (Vector3.Distance(transform.position, KamikazeExit.position) < 0.1f) {
                    Instantiate(Resources.Load("Explosion"), transform.position, transform.rotation);
                    Destroy(gameObject);
                }
                else transform.position = Vector3.MoveTowards(transform.position, KamikazeExit.position, Time.deltaTime * afterDefeatSpeed);
            }
        }
    }

    public void DecreaseShotTime() {
        rateCounter -= shotOffset;
    }

    public void StartExit() {
        orienting = true;
        initialOrientation = transform.rotation.eulerAngles;
        orientationTarget =  Quaternion.LookRotation( KamikazeEntry.position - transform.position).eulerAngles;
        goToEntryPoint = true;
    }

    Vector3 AngleLerp(Vector3 StartAngle, Vector3 FinishAngle, float t) {
        float xLerp = Mathf.LerpAngle(StartAngle.x, FinishAngle.x, t);
        float yLerp = Mathf.LerpAngle(StartAngle.y, FinishAngle.y, t);
        float zLerp = Mathf.LerpAngle(StartAngle.z, FinishAngle.z, t);
        Vector3 Lerped = new Vector3(xLerp, yLerp, zLerp);
        return Lerped;
    }
}
