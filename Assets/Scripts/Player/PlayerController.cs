using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public bool firing;
    public float cooldown;
    private float firingCounter;
    public float speedFactor = 1.0f;
    public GameObject projectile;
    public float XLimit = 10.0f;
    public float ZLimit = 5.0f;
    private bool is_firing, spaceDown, accelDown;
    private TimeManager tm;
	// Use this for initialization
	void Start () {
        firingCounter = 0.0f;
        is_firing = false;
        spaceDown = false;
        accelDown = false;
        tm = GetComponent<TimeManager>();
	}
	
	// Update is called once per frame
	void Update () {
        float axisX = Input.GetAxis("Horizontal");
        float axisY = Input.GetAxis("Vertical");
        float nextPosX = ((axisX * speedFactor) * (Time.deltaTime / Time.timeScale))  ;
        float nextPosY = ((axisY * speedFactor) * (Time.deltaTime / Time.timeScale)) ;
        //gameObject.transform.Translate(new Vector3(axisX* Time.unscaledDeltaTime, axisY* Time.unscaledDeltaTime, 0.0f)  * speedFactor);
        if ((gameObject.transform.position.x + nextPosX > -XLimit) && (gameObject.transform.position.x + nextPosX < XLimit))        
            gameObject.transform.position += new Vector3(nextPosX, 0.0f, 0.0f);       
        if ((gameObject.transform.position.z + nextPosY > -ZLimit) && (gameObject.transform.position.z + nextPosY < ZLimit))
            gameObject.transform.position += new Vector3(0.0f,0.0f ,nextPosY);
        if (Input.GetButtonDown("Fire1")) is_firing = true;
        if (Input.GetButtonUp("Fire1"))
        {
            is_firing = false;
            firingCounter = cooldown;
        }
        if (Input.GetKeyDown(KeyCode.Space)){
            if (accelDown) {
                accelDown = false;
            }
            if (!spaceDown) {
                tm.StartSloMo();
                spaceDown = true;
                speedFactor = speedFactor * 2;
            }
            else {
                tm.RestoreTime();
                spaceDown = false;
                speedFactor = speedFactor / 2;
            }
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (!accelDown ) {
                tm.StartTimeDash();
                accelDown = true;
            }
            else {
                tm.RestoreTimeDash();
                accelDown = false;
            }
        }
        if (is_firing)
        {
            Fire();
            firingCounter -= Time.unscaledDeltaTime;
        }
        else is_firing = false;

    }

    public void Fire() {
        if (firingCounter <= 0) {
            Transform t = gameObject.transform;
            Instantiate(projectile, t.position, t.rotation);
            firingCounter = cooldown;
        }
    }
}
