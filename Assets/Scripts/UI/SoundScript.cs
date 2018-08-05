using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
[RequireComponent(typeof(Button))]
public class SoundScript : MonoBehaviour, ISelectHandler{
    public AudioClip soundPlay;
    public int Action;
    private float secondsFinishPlay = 0.5f;
    private float secondsFinishSettings = 0.5f;
    private float secondsFinishQuit = 1.0f;
    private float secondsToScroll = 0.5f;
    public GameObject mainMenu;
    public GameObject Settings;
    public bool activateScroll=true;
    //private GameObject mainMenu2;
    //private GameObject Settings2;
    private Button buttonPlay { get { return GetComponent<Button>(); } }
    private AudioSource sourcePlay { get { return GetComponent<AudioSource>(); } }
    public AudioClip soundChange;
    private AudioSource sourceChange { get { return GetComponent<AudioSource>(); } }
    // Use this for initialization
    void Start () {
        gameObject.AddComponent<AudioSource>();
        sourcePlay.clip = soundPlay;
        sourcePlay.playOnAwake = false;
        buttonPlay.onClick.AddListener(() => StartCoroutine(playFileStart()));
    }
    IEnumerator playFileStart()
    {
        sourcePlay.PlayOneShot(soundPlay);

        switch (Action)
        {
            case 0: //PlayGame
                yield return new WaitForSeconds(secondsFinishPlay);
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            break;
            case 1: //Settings Menu
                yield return new WaitForSeconds(secondsFinishSettings);
                mainMenu.SetActive(false);
                Settings.SetActive(true);
                Button back = GameObject.FindGameObjectWithTag("back").GetComponent<Button>();
                back.Select();
                break;
            case 2: //Close app
                yield return new WaitForSeconds(secondsFinishQuit);
                Application.Quit();
            break;
            case 3: //Back Settings
                yield return new WaitForSeconds(secondsFinishSettings);
                mainMenu.SetActive(true);
                Settings.SetActive(false);
                Button play = GameObject.FindGameObjectWithTag("play").GetComponent<Button>();
                play.Select();
                activateScroll = false;
                break;
        }
        
    }
    public void OnSelect(BaseEventData eventData)
    {
        StartCoroutine(playFileScroll());
    }
    IEnumerator playFileScroll()
    {
        if (activateScroll) { 
            sourceChange.PlayOneShot(soundChange);
            yield return new WaitForSeconds(secondsFinishSettings);
        }else activateScroll = true;

    }

}
