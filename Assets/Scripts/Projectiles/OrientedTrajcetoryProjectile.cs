using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrientedTrajcetoryProjectile : MonoBehaviour {

    public float speed = 2.0f;
    public float timeToLive = 20.0f;
    public float damage = 10.0f;
    private TimeBehaviour tb;
    private GameObject projectile;

    // Use this for initialization
    void Start()
    {
        tb = gameObject.GetComponent<TimeBehaviour>();
        Instantiate(Resources.Load("EnemyBasicProjectile New"), transform);

    }

    // Update is called once per frame
    void Update()
    {
        timeToLive -= Time.deltaTime * tb.scaleOfTime;
        if (timeToLive <= 0.0f)
        {
            Destroy(gameObject);
            GameObject obj = Instantiate(Resources.Load("Explosion"), transform.position, transform.rotation) as GameObject;
            obj.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
        }
        transform.position += transform.forward * speed * tb.scaleOfTime;


    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.tag =="ghost")
        {
            Destroy(gameObject);
        }
    }

}
