using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public bool firing;
    public float cooldown;
    private float firingCounter;
    public float speedFactor = 1.0f;
    public GameObject projectile;
    private bool is_firing;
    private TimeManager tm;
	// Use this for initialization
	void Start () {
        firingCounter = 0.0f;
        is_firing = false;
        tm = GetComponent<TimeManager>();
	}
	
	// Update is called once per frame
	void Update () {
        float axisX = Input.GetAxis("Horizontal");
        float axisY = Input.GetAxis("Vertical");
        gameObject.transform.Translate(new Vector3(axisX, axisY, 0.0f) * (Time.unscaledDeltaTime * speedFactor));
        if (Input.GetButtonDown("Fire1")) is_firing = true;
        if (Input.GetButtonUp("Fire1"))
        {
            is_firing = false;
            firingCounter = cooldown;
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
