using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAlert : MonoBehaviour {
    public GameObject psAlert;
    private IEnumerator AlertTimerSequence;
    private bool secure = false;
    private SwitchablePlayerController spc;
    private GameObject player;
    // Use this for initialization
    void Start () {
        spc = GetComponent<SwitchablePlayerController>();
        player = GameObject.FindGameObjectWithTag("Player");

    }
	
	// Update is called once per frame
	void Update () {
        if (player != null)
        {
            if (spc.isAlert
                && !spc.isDeath
                && !spc.isRestoring
                && !secure)
            {                
                secure = true;
                AlertTimerSequence = AlertSequence(3.0f);
                StartCoroutine(AlertTimerSequence);
            }
        }else
        {
            psAlert.SetActive(false);
            secure = false;
        }
	}
    IEnumerator AlertSequence(float waitToDeath)
    {
        //Destroy();
        psAlert.SetActive(true);
        psAlert.GetComponent<ParticleSystem>().Play();
        for (int i = 0; i < 6; i++)
        {
            yield return new WaitForSeconds(waitToDeath / 6);
            if (spc.isDeath)break;
        }
        psAlert.GetComponent<ParticleSystem>().Stop();
        psAlert.SetActive(false);
        spc.isAlert = false;
        secure = false;
    }
}
