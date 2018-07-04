using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeGhost : MonoBehaviour {

    public bool firing;
    public float fireCooldown;
    public float speedFactor = 1.0f;
    public GameObject projectile;
    public float XLimit = 10.0f;
    public float ZLimit = 5.0f;
    public float invul = 1.0f;
    public float startDelayCounter = 0.0f;
    private float firingCounter;
    private bool is_firing, start;
    // Use this for initialization
    void Start()
    {
        firingCounter = 0.0f;
        is_firing = false;
        start = false;
    }

    IEnumerator Move() {
        float axisX = Input.GetAxis("Horizontal");
        float axisY = Input.GetAxis("Vertical");
        Debug.Log("I should be moving");
        yield return new WaitForSeconds(0.3f);
        MoveCharacter(axisX,axisY);
    }

    // Update is called once per frame
    void Update()
    {
        if (start)
        {
            if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
            {
                //float axisX = Input.GetAxis("Horizontal");
                //float axisY = Input.GetAxis("Vertical");
                StartCoroutine(Move());
            }   
                if (Input.GetButtonDown("Fire1") && !is_firing) is_firing = true;
                if (Input.GetButtonUp("Fire1") && is_firing)
                {
                    is_firing = false;
                    firingCounter = fireCooldown;
                }
                if (is_firing)
                {
                    Fire();
                    firingCounter -= Time.unscaledDeltaTime;
                }
                else is_firing = false;
            
        }
        else {
            startDelayCounter -= Time.unscaledDeltaTime;
            if (startDelayCounter < 0.0f) start = true;
        }
    }

    public void MoveCharacter(float axisX, float axisY) {
        float nextPosX = ((axisX * speedFactor) * (Time.deltaTime / Time.timeScale));
        float nextPosY = ((axisY * speedFactor) * (Time.deltaTime / Time.timeScale));
        if ((gameObject.transform.position.x + nextPosX > -XLimit) && (gameObject.transform.position.x + nextPosX < XLimit))
            gameObject.transform.position += new Vector3(nextPosX, 0.0f, 0.0f);
        if ((gameObject.transform.position.z + nextPosY > -ZLimit) && (gameObject.transform.position.z + nextPosY < ZLimit))
            gameObject.transform.position += new Vector3(0.0f, 0.0f, nextPosY);
    }

    public void Fire()
    {
        if (firingCounter <= 0.0f)
        {
            Transform t = gameObject.transform;
            Instantiate(projectile, t.position, t.rotation);
            firingCounter = fireCooldown;
        }
    }
}
