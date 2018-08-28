using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeGhost : MonoBehaviour
{

    public bool firing;
    public float fireCooldown;
    public float speedFactor = 1.0f;
    public GameObject projectile,pShootGhost;
    public float XLimit = 10.0f;
    public float ZLimit = 5.0f;
    public float invul = 1.0f;
    public float startDelayCounter = 0.0f;
    private float firingCounter;
    public bool is_firing;
    private ParticleSystem partSys;
    //------------------------------------------

    const int MAX_FPS = 60;

    public Transform leader;
    public float lagSeconds = 0.5f;

    Vector3[] position_buffer;
    float[] time_buffer;
    int oldest_index, newest_index;
    public bool blocksProjectiles = true;

    float counter;
    // Use this for initialization
    void Start()
    {
        firingCounter = 0.0f;
        is_firing = GameObject.FindGameObjectWithTag("Player").GetComponent<SwitchablePlayerController>().IsFiring();

        int length = Mathf.CeilToInt(lagSeconds * MAX_FPS);
        position_buffer = new Vector3[length];
        time_buffer = new float[length];

        position_buffer[0] = leader.position;
        time_buffer[0] = Time.fixedTime;

        oldest_index = 0;
        newest_index = 1;
        counter = Time.time;
        if (blocksProjectiles) gameObject.tag = "ghost";
        else gameObject.tag = "Untagged";
        partSys = GetComponent<ParticleSystem>();
    }

    public void SetFiringCounter(float newFiringCounter)
    {
        firingCounter = newFiringCounter;
    }

    void Update()
    {
        if (is_firing)
        {
            Fire();
            firingCounter -= Time.deltaTime;
        }
    }

    private void LateUpdate()
    {
        if ((Input.GetAxis("Horizontal") != 0) || (Input.GetAxis("Vertical") != 0))
        {
            counter += Time.unscaledDeltaTime;
            int new_index = (newest_index + 1) % position_buffer.Length;
            if (new_index != oldest_index) newest_index = new_index;
            position_buffer[newest_index] = leader.position;
            time_buffer[newest_index] = counter; //Time.fixedTime;
            float newTime = counter - lagSeconds; //Time.fixedTime - lagSeconds;
            int next;
            while (time_buffer[next = (oldest_index + 1) % time_buffer.Length] < newTime) oldest_index = next;
            float span = time_buffer[next] - time_buffer[oldest_index];
            float delta = 0.0f;
            if (span > 0) delta = (newTime - time_buffer[oldest_index]) / span;
            transform.position = Vector3.Lerp(position_buffer[oldest_index], position_buffer[next], delta);

        }
    }

    public void StartFiring()
    {
        is_firing = true;
    }

    public void StopFiring()
    {
        is_firing = false;
        firingCounter = 0.0f;
    }

    public void Fire()
    {
        if (firingCounter <= 0.0f)
        {
            if (pShootGhost != null) pShootGhost.GetComponent<ParticleSystem>().Play();
            Transform t = gameObject.transform;
            Instantiate(projectile, t.position, t.rotation);
            firingCounter = fireCooldown;
        }
    }
    public void RotateGhosts()
    {
        foreach (Transform child in transform)
        {
            if (child.name == "PS_TimeGhost")
            {
                var main = child.GetComponent<ParticleSystem>().main;
                main.startRotationX = GameObject.FindGameObjectWithTag("Player").GetComponent<SwitchablePlayerController>().rotX.x;
                main.startRotationY = 0.0f;
                main.startRotationZ = GameObject.FindGameObjectWithTag("Player").GetComponent<SwitchablePlayerController>().rotZ.z;
            }

        }
        //var main = partSys.main;
        //main.startRotationXMultiplier = GameObject.FindGameObjectWithTag("Player").GetComponent<SwitchablePlayerController>().rotX.x;
        //main.startRotationYMultiplier = 0.0f;
        //main.startRotationZMultiplier = GameObject.FindGameObjectWithTag("Player").GetComponent<SwitchablePlayerController>().rotZ.z;
        //transform.eulerAngles = new Vector3(GameObject.FindGameObjectWithTag("Player").GetComponent<SwitchablePlayerController>().rotX.x, 
        //    0.0f, GameObject.FindGameObjectWithTag("Player").GetComponent<SwitchablePlayerController>().rotZ.z);
    }
    public void DisableGhosts()
    {
        foreach (Transform child in transform)
        {
            if (child.name == "PS_PlayerShoot") child.gameObject.SetActive(false);
            if (child.name == "PS_TimeGhost") child.gameObject.SetActive(false);
            if (child.name == "PS_TimeGhost_D") child.gameObject.SetActive(true);
        }
    }
}


/*

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
    private float firingCounter, delayCounter;
    public bool is_firing;
    //------------------------------------------

    const int MAX_FPS = 60;

    public Transform leader;
    public float lagSeconds = 0.5f;

    Vector3[] position_buffer;
    float[] time_buffer;
    int oldest_index, newest_index;
    public bool blocksProjectiles = true;

    float counter;
    // Use this for initialization
    void Start()
    {
        firingCounter = 0.0f;
        is_firing = GameObject.FindGameObjectWithTag("Player").GetComponent<SwitchablePlayerController>().is_firing; 

        int length = Mathf.CeilToInt(lagSeconds * MAX_FPS);
        position_buffer = new Vector3[length];
        time_buffer = new float[length];

        position_buffer[0] = leader.position;
        time_buffer[0] = Time.fixedTime;

        oldest_index = 0;
        newest_index = 1;
        counter = Time.time;
        delayCounter = startDelayCounter;
        if (blocksProjectiles) gameObject.tag = "ghost";
        else gameObject.tag = "Untagged";
    }

    public void SetFiringCounter(float newFiringCounter) {
        firingCounter = newFiringCounter;
    }

    void Update()
    {
        if (is_firing)
        {
            Fire();
            firingCounter -= Time.deltaTime;
        }
    }
        
    private void LateUpdate()
    {
        if ((Input.GetAxis("Horizontal") != 0) || (Input.GetAxis("Vertical") != 0))
        {
                counter += Time.unscaledDeltaTime;
                int new_index = (newest_index + 1) % position_buffer.Length;
                if (new_index != oldest_index) newest_index = new_index;
                position_buffer[newest_index] = leader.position;
                time_buffer[newest_index] = counter; //Time.fixedTime;
                float newTime = counter - lagSeconds; //Time.fixedTime - lagSeconds;
                int next;
                while (time_buffer[next = (oldest_index + 1) % time_buffer.Length] < newTime) oldest_index = next;
                float span = time_buffer[next] - time_buffer[oldest_index];
                float delta = 0.0f;
                if (span > 0) delta = (newTime - time_buffer[oldest_index]) / span;
                transform.position = Vector3.Lerp(position_buffer[oldest_index], position_buffer[next], delta);

        }
    }

    public void StartFiring() {
        is_firing = true;
    }

    public void StopFiring() {
        is_firing = false;
        firingCounter = 0.0f;
    }

    public void Fire()
    {
        if (firingCounter <= 0.0f) {
            Transform t = gameObject.transform;
            Instantiate(projectile, t.position, t.rotation);
            firingCounter = fireCooldown;
        }
    }
}

     
*/
