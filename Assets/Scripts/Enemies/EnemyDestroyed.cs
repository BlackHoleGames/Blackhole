using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDestroyed : MonoBehaviour {

    public float timeBeforeDestroy = 1.5f;
    public float flickerTime = 0.1f;
    private bool matOn;
    private bool isFlickeringEye = true;
    private bool isFlickeringParts = true;
    private float flickerCounter;
    public Material matOff, matFlicker;
    private SquadManager squadManager;
    private GameObject eye;
    private List<RandomDestructible> listRandDes = new List<RandomDestructible>();
    // Use this for initialization
    void Start() {
        timeBeforeDestroy = 3.0f;
        squadManager = GetComponentInParent<SquadManager>();
        foreach (Transform child in transform)
        {
            if (child.name == "AlienEye")
            {
                eye = child.gameObject;
                Instantiate(Resources.Load("TimeBubble"), eye.transform.position, eye.transform.rotation);
                break;
            }
            else {
                float time = Random.Range(1.0f,2.0f);
                RandomDestructible rd = new RandomDestructible(child.gameObject,time);
                listRandDes.Add(rd);
            }
        }
        transform.parent = null;
        float randomYRot = Random.Range(0.0f,360.0f);
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, randomYRot, transform.eulerAngles.z);
    }
        // Update is called once per frame
    void Update () {
        timeBeforeDestroy -= Time.deltaTime;
        if (timeBeforeDestroy <= 1.5f) isFlickeringEye = false;
        if (timeBeforeDestroy < 0.0f && eye) {
            GameObject obj = Instantiate(Resources.Load("Explosion"), eye.transform.position, eye.transform.rotation) as GameObject;
            obj.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            CallDestroy();
            Destroy(eye);
        }
        if (listRandDes.Count > 0)
        {
            List<RandomDestructible> toDestroy = new List<RandomDestructible>();
            foreach (RandomDestructible rd in listRandDes)
            {
                float newTime = rd.GetRandTime();
                newTime -= Time.deltaTime;
                if (newTime < 0.0f)
                {
                    toDestroy.Add(rd);
                    GameObject obj = Instantiate(Resources.Load("Explosion"), rd.GetPiece().transform.position, rd.GetPiece().transform.rotation) as GameObject;
                    obj.transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);
                }
                else rd.SetRandTime(newTime);
                
                if (rd.GetRandTime()<1.0f) isFlickeringParts = false;
                else isFlickeringParts = true;
            }
            if (toDestroy.Count > 0)
            {
                foreach (RandomDestructible destroy in toDestroy)
                {
                    Destroy(destroy.GetPiece());
                    listRandDes.Remove(destroy);

                }
                toDestroy.Clear();
            }
        }
        if (listRandDes.Count == 0 && !eye) Destroy(gameObject);
        if (flickerCounter < 0.0f) {
            foreach (Transform child in transform)
            {
                if(child.name == "AlienEye")
                { 
                    if (!matOn && isFlickeringEye)
                    {
                        child.GetComponent<Renderer>().material = matFlicker;
                        matOn = true;
                    }
                    else
                    {
                        child.GetComponent<Renderer>().material = matOff;
                        matOn = false;
                    }
                }else
                {
                    if (!matOn && isFlickeringParts)
                    {
                        child.GetComponent<Renderer>().material = matFlicker;
                        matOn = true;
                    }
                    else
                    {
                        child.GetComponent<Renderer>().material = matOff;
                        matOn = false;
                    }
                }
            }
            flickerCounter = flickerTime;
        }
        else flickerCounter -= Time.deltaTime;
	}

    public void CallDestroy() {
        /*foreach (Transform child in transform)
        {
            GameObject obj = Instantiate(Resources.Load("Explosion"), child.position, child.rotation) as GameObject;
            if (child.name != "AlienEye") obj.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            Destroy(child.gameObject);
        }*/
        //squadManager.DecreaseNumber();
        //Destroy(gameObject);
    }

    public class RandomDestructible {

        public RandomDestructible(GameObject obj, float time) {
            piece = obj;
            randomTime = time;
        }

        public GameObject GetPiece() { return piece; }
        public void SetRandTime(float time) { randomTime = time; }
        public float GetRandTime() { return randomTime; }

        GameObject piece;
        float randomTime;
    }
}
