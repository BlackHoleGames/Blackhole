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
    private float firingCounter, t, rtimeZ , rtimeX, alertModeTime, rotationTargetZ, rotationTargetX;
    private bool   readjustPosition, inWarp, startRotatingRoll, startRotatingPitch, restorePitch ;
    private TimeManager tm;
    private List<GameObject> ghostArray;
    public GameObject projectile, sphere, ghost, parentAxis;
    public static bool camMovementEnemies;
    public AudioSource timebomb, slomo;
    public Vector3 readjustInitialPos, initialRot, rotX, rotZ;
    // Use this for initialization
    void Start() {
        actualLife = shield;
        life.value = actualLife;
        rotationTargetZ = 0.0f;
        rotationTargetX = 0.0f;
        firingCounter = 0.0f;
        alertModeTime = 0.0f;
        rtimeZ = 0.0f;
        rtimeX = 0.0f;
        initialRot = transform.rotation.eulerAngles;
        startRotatingRoll = false;
        startRotatingPitch = false;
        restorePitch = false;
        is_firing = false;
        is_vertical = true;
        tm = GetComponent<TimeManager>();
        ghostArray = new List<GameObject>();
        camMovementEnemies = false;

        //parentAxis = gameObject;
    }

    // Update is called once per frame
    void Update() {
        float axisX = Input.GetAxis("Horizontal");
        float axisY = Input.GetAxis("Vertical");
        Move(axisX,axisY);
        ManagePitchRotation(axisY);
        ManageRollRotation(axisX);
        if (startRotatingRoll || startRotatingPitch) Rotate();        
        ManageInput();
        if (alertModeTime > 0.0f) alertModeTime -= Time.unscaledDeltaTime;
        if (readjustPosition) ReadjustPlayer();
        if (is_firing) {
            Fire();
            firingCounter -= Time.deltaTime;
        }
        if (actualLife < shield) Regen();
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
            Transform t = transform;
            Instantiate(projectile, t.position, t.rotation);
            firingCounter = fireCooldown;
        }
    }

    public void SwitchAxis() {
        Debug.Log("Switching");
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
            coordAD = parentAxis.transform.position.x;
            coordWS = parentAxis.transform.position.z;
        }
        else {
            coordAD = parentAxis.transform.position.x;
            coordWS = parentAxis.transform.position.y;
        }
        if ((coordAD + nextPosX > -XLimit) && (coordAD + nextPosX < XLimit)){
            if (is_vertical) parentAxis.transform.position += new Vector3(nextPosX, 0.0f, 0.0f);
            else parentAxis.transform.position += new Vector3(nextPosX, 0.0f, 0.0f);
        }
        if ((coordWS + nextPosYZ > -ZLimit) && (coordWS + nextPosYZ < ZLimit)){
            if (is_vertical) parentAxis.transform.position += new Vector3(0.0f, 0.0f, nextPosYZ);
            else parentAxis.transform.position += new Vector3(0.0f, nextPosYZ, 0.0f);
        }
        
    }

    //parentAxis
    public void ManageRollRotation(float Xinput) {

        if (Xinput == 0 && (transform.rotation.eulerAngles.z != 0) ) {
            rotationTargetZ = 0.0f;
            rtimeZ = 0;
            initialRot = transform.rotation.eulerAngles;
            startRotatingRoll = true;
        }
        else if (Xinput != 0) {
            if (transform.rotation.eulerAngles.z < RollLimit && transform.rotation.eulerAngles.z > -RollLimit)
            {
                float actualRot = rotationTargetZ;
                rotationTargetZ = RollLimit;
                if (Xinput > 0.0f) rotationTargetZ = -RollLimit;
                if (startRotatingRoll && rotationTargetZ != actualRot) {
                    initialRot = transform.rotation.eulerAngles;
                    rtimeZ = 0;
                }
                startRotatingRoll = true;
            }
        }
        if (startRotatingRoll) {
            rtimeZ += Time.unscaledDeltaTime / 1.0f;
            rotZ = AngleLerp(initialRot,new Vector3(0.0f, 0.0f, rotationTargetZ),rtimeZ*rotationSpeed);

            //transform.eulerAngles = targetRotationZ;
            if (Mathf.Abs(transform.rotation.eulerAngles.z - rotationTargetZ) < 0.01) {
                startRotatingRoll = false;
            }

        }
    }

    public void ManagePitchRotation( float YZinput) {
        if (!is_vertical && !readjustPosition)
        {
            if (YZinput == 0 && (transform.rotation.eulerAngles.x != 0))
            {
                rotationTargetX = 0.0f;
                rtimeX = 0;
                initialRot = transform.rotation.eulerAngles;
                startRotatingPitch = true;
            }
            if (YZinput != 0)
            {
                if (transform.rotation.eulerAngles.x < RollLimit && transform.rotation.eulerAngles.x > -RollLimit)
                {
                    float actualRot = rotationTargetX;
                    rotationTargetX = RollLimit;
                    if (YZinput > 0.0f) rotationTargetX = -RollLimit;
                    if (startRotatingPitch && rotationTargetX != actualRot) {
                        initialRot = transform.rotation.eulerAngles;
                        rtimeX = 0;
                    }
                    startRotatingPitch = true;                                                   
                }                     
            }
            if (startRotatingPitch) {
                rtimeX += Time.unscaledDeltaTime / 1.0f;
                rotX = AngleLerp(initialRot, new Vector3(rotationTargetX, 0.0f, 0.0f), rtimeX * rotationSpeed);
                //parentAxis.transform.eulerAngles = targetRotationX;
                if (Mathf.Abs(parentAxis.transform.rotation.eulerAngles.x - rotationTargetX) < 0.01)
                {
                    startRotatingPitch = false;
                }
            }
        }
    }

    public void Rotate() {
        parentAxis.transform.eulerAngles = new Vector3(rotX.x, 0.0f, rotZ.z);
    }
    
    public void ReadjustPlayer() {
        if (is_vertical){
            if (parentAxis.transform.rotation.eulerAngles.x != 0) {
                if (!restorePitch) {
                    rotationTargetX = 0.0f;
                    rtimeX = 0;
                    initialRot = transform.rotation.eulerAngles;
                    restorePitch = true;
                }
                else {
                    rtimeX += Time.unscaledDeltaTime / 1.0f;
                    rotX = AngleLerp(initialRot, new Vector3(rotationTargetX, 0.0f, 0.0f), rtimeX * rotationSpeed);
                    //parentAxis.transform.eulerAngles = targetRotationX;
                    if (Mathf.Abs(parentAxis.transform.rotation.eulerAngles.x - rotationTargetX) < 0.01) {
                        parentAxis.transform.eulerAngles = new Vector3(0.0f, 0.0f, parentAxis.transform.eulerAngles.z);
                        restorePitch = false;
                        rtimeX = 0;
                    }
                }
            }
            t += Time.unscaledDeltaTime / 1.0f;
            parentAxis.transform.position = Vector3.Lerp(readjustInitialPos, new Vector3(parentAxis.transform.position.x,0.0f, parentAxis.transform.position.z), t);
            if ((Mathf.Abs(parentAxis.transform.position.y) < 0.01)) readjustPosition = false;
        }
        else {
            t += Time.unscaledDeltaTime / 1.0f;
            parentAxis.transform.position = Vector3.Lerp(readjustInitialPos, new Vector3(parentAxis.transform.position.x, parentAxis.transform.position.y, -3.5f), t);
            if ((Mathf.Abs(Mathf.Abs(parentAxis.transform.position.z) - 3.5f) < 0.01)) readjustPosition = false;
        }
    }

    public void SpawnGhost() {
        if (ghostArray.Count < 2) {
            GameObject obj = Instantiate(ghost, transform.position, transform.rotation);
            if (ghostArray.Count > 0) obj.GetComponent<TimeGhost>().leader = ghostArray[(ghostArray.Count - 1)].transform;
            else obj.GetComponent<TimeGhost>().leader = transform;
            obj.transform.rotation = Quaternion.identity;
            obj.GetComponent<TimeGhost>().SetFiringCounter(firingCounter);
            ghostArray.Add(obj);
        }
    }

    private void OnTriggerEnter(Collider other) {
        /*if (other.tag == "Enemy" || other.tag == "EnemyProjectile") {

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
        }*/
    }

    Vector3 AngleLerp(Vector3 StartAngle, Vector3 FinishAngle, float t)
    {
        float xLerp = Mathf.LerpAngle(StartAngle.x, FinishAngle.x, t);
        float yLerp = Mathf.LerpAngle(StartAngle.y, FinishAngle.y, t);
        float zLerp = Mathf.LerpAngle(StartAngle.z, FinishAngle.z, t);
        Vector3 Lerped = new Vector3(xLerp, yLerp, zLerp);
        return Lerped;
    }
}
