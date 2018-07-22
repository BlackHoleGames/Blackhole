using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KamikazeEnemy : MonoBehaviour {

    public float speed = 0.5f;
    public float turbospeed = 10.0f;
    public float life = 50.0f;
    private SquadManager squadManager;
    public GameObject explosionPS;
    private TimeBehaviour tb;
    private GameObject player;
    private float direction;
    private bool turbo;
    private AudioSource audioSource;
    public AudioClip explosionClip;
    // Use this for initialization
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        turbo = false;
        player = GameObject.FindGameObjectWithTag("Player");
        if (transform.position.x < 0.0f) direction = 1.0f;
        else direction = -1.0f;
        tb = gameObject.GetComponent<TimeBehaviour>();
        squadManager = GetComponentInParent<SquadManager>();
        audioSource.Play();

        //transform.parent.GetComponentInChildren<ProtectorEnemy>().squadron.Add(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(transform.position.x - player.transform.position.x) < 0.1f) turbo = true;
        if (!turbo) transform.position += new Vector3(speed * Time.deltaTime * direction * tb.scaleOfTime, 0.0f, 0.0f);
        else transform.position += new Vector3(0.0f, 0.0f, -turbospeed * Time.deltaTime * tb.scaleOfTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PlayerProjectile")
        {
            life -= other.gameObject.GetComponent<Projectile>().damage;
            if (life <= 0.0f)
            {
                Instantiate(explosionPS, gameObject.transform.position, gameObject.transform.rotation);

                squadManager.DecreaseNumber(explosionClip);
                Destroy(gameObject);
            }
           
        }
    }
}
