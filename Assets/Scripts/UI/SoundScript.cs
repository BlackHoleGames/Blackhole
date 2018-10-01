using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
[RequireComponent(typeof(Button))]
public class SoundScript : MonoBehaviour, ISelectHandler
{
    public AudioClip soundPlay;
    public int Action;
    private float secondsFinishPlay = 0.5f;
    private float secondsFinishSettings = 0.5f;
    private float secondsFinishQuit = 1.0f;
    private float secondsToScroll = 0.5f;
    public GameObject mainMenu;
    public GameObject Quit;
    private Transform Cursor;
    public bool activateScroll = true;
    private Button buttonPlay { get { return GetComponent<Button>(); } }
    private AudioSource sourcePlay { get { return GetComponent<AudioSource>(); } }
    public AudioClip soundChange;
    private AudioSource sourceChange { get { return GetComponent<AudioSource>(); } }
    // Use this for initialization
    void Start()
    {
        gameObject.AddComponent<AudioSource>();
        sourcePlay.clip = soundPlay;
        sourcePlay.playOnAwake = false;
        buttonPlay.onClick.AddListener(() => StartCoroutine(playFileStart()));


    }
    void Update()
    {

    }
    IEnumerator playFileStart()
    {
        sourcePlay.PlayOneShot(soundPlay);

        switch (Action)
        {
            case 0: //PlayGame
                yield return new WaitForSeconds(secondsFinishPlay);
                //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                SceneManager.LoadScene(3);
                break;
            case 1: //Close app
                mainMenu.SetActive(false);
                //Settings.SetActive(false);
                Quit.SetActive(true);
                Button QYes = GameObject.FindGameObjectWithTag("QYes").GetComponent<Button>();
                QYes.Select();
                break;
            case 2: //Quit NoConfirm
                yield return new WaitForSeconds(secondsFinishSettings);
                mainMenu.SetActive(true);
                //Settings.SetActive(false);
                Quit.SetActive(false);
                Button qplay = GameObject.FindGameObjectWithTag("play").GetComponent<Button>();
                qplay.Select();
                activateScroll = false;
                break;
            case 3: //Quit Confirm
                yield return new WaitForSeconds(secondsFinishSettings);
                Application.Quit();
                break;
        }

    }
    public void OnSelect(BaseEventData eventData)
    {
        StartCoroutine(playFileScroll());
    }
    IEnumerator playFileScroll()
    {
        switch (Action)
        {
            case 0: //PlayGame
                Cursor = GameObject.FindGameObjectWithTag("Cursor").GetComponent<Transform>();
                Cursor.transform.localPosition = new Vector3(-159, 7.2f, 0.0f);
                break;
            case 1: //Close app
                Cursor = GameObject.FindGameObjectWithTag("Cursor").GetComponent<Transform>();
                //Cursor.transform.localPosition = new Vector3(-159, -78f, 0.0f);
                //Cursor.transform.localPosition = new Vector3(-159, -258f, 0.0f);
                Cursor.transform.localPosition = new Vector3(-159, -162f, 0.0f);
                break;
            case 2: //Quit NoConfirm
                Cursor = GameObject.FindGameObjectWithTag("Cursor").GetComponent<Transform>();
                Cursor.transform.localPosition = new Vector3(-159, -164f, 0.0f);
                break;
            case 3: //Quit Confirm
                Cursor = GameObject.FindGameObjectWithTag("Cursor").GetComponent<Transform>();
                Cursor.transform.localPosition = new Vector3(-159, -79f, 0.0f);
                break;
        }
        if (activateScroll)
        {
            sourceChange.PlayOneShot(soundChange);
            yield return new WaitForSeconds(secondsFinishSettings);
        }
        else activateScroll = true;
    }

}
