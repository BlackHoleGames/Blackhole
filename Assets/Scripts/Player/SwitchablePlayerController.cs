using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;

public class SwitchablePlayerController : MonoBehaviour {


    public bool is_vertical, is_firing;
    public float fireCooldown;
    public float speedFactor = 1.0f;
    public float rotationSpeed = 2.0f;
    public float XLimit = 10.0f;
    public float ZLimit = 5.0f;
    public float RollLimit = 30.0f;
    public float PitchLimit = 30.0f;
    public float invul = 1.0f;
    public float sloMo = 2.0f;
    public float alertModeDuration = 3.0f;
    public float invulnerabilityDuration = 1.0f;
    public static float shield = 10.0f;
    public float shieldRegenPerSec = 1.0f;
    public Slider life;
    public float actualLife;
    private float firingCounter, t, rtime, alertModeTime, rotationTarget;
    private bool   readjustPosition, inWarp, startRotating ;
    private TimeManager tm;
    private List<GameObject> ghostArray;
    public GameObject projectile, sphere, ghost;
    public static bool camMovementEnemies;
    public AudioSource timebomb, slomo;
    public Vector3 readjustInitialPos, initialRot;
    // Use this for initialization
    void Start() {
        actualLife = shield;
        life.value = actualLife;
        rotationTarget = 0.0f;
        firingCounter = 0.0f;
        alertModeTime = 0.0f;
        rtime = 0.0f;
        initialRot = transform.rotation.eulerAngles;
        startRotating = false;
        is_firing = false;
        is_vertical = true;
        tm = GetComponent<TimeManager>();
        ghostArray = new List<GameObject>();
        camMovementEnemies = false;
    }

    // Update is called once per frame
    void Update() {
        float axisX = Input.GetAxis("Horizontal");
        float axisY = Input.GetAxis("Vertical");
        Move(axisX,axisY);
        ManageRotation(axisX, axisY);
        ManageInput();
        if (alertModeTime > 0.0f) alertModeTime -= Time.unscaledDeltaTime;
        if (readjustPosition) ReadjustPlayer();
        if (is_firing) {
            Fire();
            firingCounter -= Time.deltaTime;
        }
        if (actualLife < shield) Regen();
        if (inWarp) {
            if (!tm.isMaxGTLReached) {
                SwitchAxis();
                inWarp = false;
            }
        }
    }

    public void ManageInput() {
        if (Input.GetButtonDown("Fire1") && !is_firing)
        {
            is_firing = true;
            foreach (GameObject g in ghostArray) g.GetComponent<TimeGhost>().StartFiring();
        }
        if (Input.GetButtonUp("Fire1") && is_firing)
        {
            is_firing = false;
            firingCounter = 0.0f;
            foreach (GameObject g in ghostArray) g.GetComponent<TimeGhost>().StopFiring();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            timebomb.Play();
            Instantiate(sphere, gameObject.transform.position, gameObject.transform.rotation);
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            if (tm.HasCharges())
            {
                SwitchAxis();
                inWarp = true;
                tm.StartTimeWarp();
            }
        }
    }

    public void Regen() {
        actualLife += shieldRegenPerSec * Time.unscaledDeltaTime;
        life.value = actualLife;
    }

    public void Fire() {
        if (firingCounter <= 0.0f) {
            Transform t = gameObject.transform;
            Instantiate(projectile, t.position, t.rotation);
            firingCounter = fireCooldown;
        }
    }

    public void SwitchAxis() {
        readjustInitialPos = transform.position;
        readjustPosition = true;
        t = 0;
        is_vertical = !is_vertical;
        camMovementEnemies = true;
    }

    public void Move(float Xinput, float YZinput) {
        float nextPosX = ((Xinput * speedFactor) * (Time.unscaledDeltaTime)); // / Time.timeScale));
        float nextPosYZ = ((YZinput * speedFactor) * (Time.unscaledDeltaTime));  // / Time.timeScale));
        float coordAD, coordWS;
        if (is_vertical) {
            coordAD = gameObject.transform.position.x;
            coordWS = gameObject.transform.position.z;
        }
        else {
            coordAD = gameObject.transform.position.x;
            coordWS = gameObject.transform.position.y;
        }
        if ((coordAD + nextPosX > -XLimit) && (coordAD + nextPosX < XLimit)){
            if (is_vertical) gameObject.transform.position += new Vector3(nextPosX, 0.0f, 0.0f);
            else gameObject.transform.position += new Vector3(nextPosX, 0.0f, 0.0f);
        }
        if ((coordWS + nextPosYZ > -ZLimit) && (coordWS + nextPosYZ < ZLimit)){
            if (is_vertical) gameObject.transform.position += new Vector3(0.0f, 0.0f, nextPosYZ);
            else gameObject.transform.position += new Vector3(0.0f, nextPosYZ, 0.0f);
        }
        
    }

    public void ManageRotation(float Xinput, float YZinput) {
        if (is_vertical) {
            if (YZinput == 0) {

            }
        }
        if (Xinput == 0 && (transform.rotation.eulerAngles.z != 0) ) {
            rotationTarget = 0.0f;
            startRotating = true;
        }
        else if (Xinput != 0) {
            if (transform.rotation.eulerAngles.z < 30.0f && transform.rotation.eulerAngles.z > 330.0f)
            {
                float actualRot = rotationTarget;
                rotationTarget = RollLimit;
                if (Xinput < 0.0f) rotationTarget = 360.0f - RollLimit;
                if (startRotating && rotationTarget != actualRot) initialRot = transform.rotation.eulerAngles;
                startRotating = true;
                transform.rotation = Quaternion.Euler(Vector3.Lerp(initialRot, new Vector3(0.0f,0) , t));
            }
        }
        if (startRotating) {
            transform.rotation = Quaternion.Euler(Vector3.Lerp(initialRot, new Vector3(0.0f, 0.0f, rotationTarget), 0.05f));
            if (Mathf.Abs(transform.rotation.eulerAngles.z - rotationTarget) < 0.01) startRotating = false;
        }
    }

    public void ReadjustPlayer() {
        if (is_vertical){
            t += Time.unscaledDeltaTime / 1.0f;
            transform.position = Vector3.Lerp(readjustInitialPos, new Vector3(transform.position.x,0.0f, transform.position.z), t);
            if ((Mathf.Abs(transform.position.y) < 0.01)) readjustPosition = false;
        }
        else {
            t += Time.unscaledDeltaTime / 1.0f;
            transform.position = Vector3.Lerp(readjustInitialPos, new Vector3(transform.position.x, transform.position.y, -3.5f), t);
            if ((Mathf.Abs(Mathf.Abs(transform.position.z) - 3.5f) < 0.01)) readjustPosition = false;
        }
    }

    public void SpawnGhost() {
        if (ghostArray.Count < 2) {
            GameObject obj = Instantiate(ghost, transform.position, transform.rotation);
            if (ghostArray.Count > 0) obj.GetComponent<TimeGhost>().leader = ghostArray[(ghostArray.Count - 1)].transform;
            else obj.GetComponent<TimeGhost>().leader = transform;
            obj.GetComponent<TimeGhost>().SetFiringCounter(firingCounter);
            ghostArray.Add(obj);
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Enemy" || other.tag == "EnemyProjectile") {

            if (alertModeTime > 0.0f) {
                if (alertModeTime < (alertModeDuration - invulnerabilityDuration))
                {
                    actualLife = actualLife - (shield / 2.0f);
                    alertModeTime = alertModeDuration;
                    actualLife = actualLife - (shield / 2.0f);
                    life.value = actualLife;
                    if (actualLife < 0.0f) { } // Death                
                }
            }
            else alertModeTime = alertModeDuration;
            if (!tm.slowDown) {
                slomo.Play();
                tm.StartSloMo();
                alertModeTime = alertModeDuration;
                while (ghostArray.Count > 0) {
                    Destroy(ghostArray[0]);
                    ghostArray.Remove(ghostArray[0]);
                }
                ghostArray.Clear();
            }
        }
    }
}
