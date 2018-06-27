using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeBubble : MonoBehaviour {

    public float timeBubbleDuration = 1.0f;
    public float timeBubbleMaxRadius = 10.0f;
    public float timeBubbleIncreaseStep = 0.4f;
    public GameObject tmPartSys;
    private bool toDestroy;
    // Use this for initialization
    void Start () {
        toDestroy = false;
        Instantiate(tmPartSys, gameObject.transform.position, gameObject.transform.rotation);
    }

    // Update is called once per frame
    void Update () {
        if (toDestroy) Destroy(gameObject);
        if (gameObject.transform.localScale.x < timeBubbleMaxRadius)
        {
            Vector3 newScale = gameObject.transform.localScale + new Vector3(2.5f, 2.5f, 2.5f) * (50.0f* Time.deltaTime);
            gameObject.transform.localScale = newScale;
        }
        else
        {
            if (timeBubbleDuration > 0.0f) timeBubbleDuration -= Time.deltaTime;
            else
            {               
                gameObject.transform.localScale = new Vector3(0.05f,0.05f,0.05f);
                toDestroy = true;
            }
        }          
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((other.tag == "EnemyProjectile" || other.tag == "Enemy") && !toDestroy) {
            TimeBehaviour tb = other.GetComponent<TimeBehaviour>();
            if (tb) tb.scaleOfTime = 0.2f;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "EnemyProjectile" || other.tag == "Enemy")
        {
            Debug.Log("Exiting");
            TimeBehaviour tb = other.GetComponent<TimeBehaviour>();
            if (tb) tb.SpeedUp();
        }
    }
}
