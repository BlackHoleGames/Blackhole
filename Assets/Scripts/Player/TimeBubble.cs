using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeBubble : MonoBehaviour {

    public float timeBubbleDuration = 1.5f;
    public float timeBubbleMaxRadius = 10.0f;
    public float timeToMaxSize = 0.4f;
    private float t;
    public GameObject tmPartSys;
    private bool toDestroy;
    private float sizeMultiplier = 1.0f;
    // Use this for initialization
    void Start () {
        toDestroy = false;
        Instantiate(tmPartSys, gameObject.transform.position, gameObject.transform.rotation);
        t = 0;
        if (Time.timeScale == 1.5f) sizeMultiplier = 1.5f;
        if (Time.timeScale >= 2.0f) sizeMultiplier = 3.0f;
    }

    // Update is called once per frame
    void Update () {

        if (gameObject.transform.localScale.x < timeBubbleMaxRadius)
        {
            t += Time.unscaledDeltaTime / timeToMaxSize;
            gameObject.transform.localScale = Vector3.Lerp(new Vector3(0.0f,0.0f,0.0f), (new Vector3(10.0f, 10.0f, 10.0f) * sizeMultiplier), t);            
        }
        else {
            if (timeBubbleDuration > 0.0f) timeBubbleDuration -= Time.unscaledDeltaTime;
            else
            {
                Destroy(gameObject);
            }
        }          
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((other.tag == "EnemyProjectile" || other.tag == "Enemy") && !toDestroy) {
            TimeBehaviour tb = other.GetComponent<TimeBehaviour>();
            if (tb) tb.SlowDown(0.2f, 0.5f);
        }
    }

    /*private void OnTriggerExit(Collider other)
    {
        if (other.tag == "EnemyProjectile" || other.tag == "Enemy")
        {
            Debug.Log("Exiting");
            TimeBehaviour tb = other.GetComponent<TimeBehaviour>();
            if (tb) tb.SpeedUp();
        }
    }*/
}
