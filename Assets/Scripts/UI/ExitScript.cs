using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
[RequireComponent(typeof(Button))]
public class ExitScript : MonoBehaviour, ISelectHandler
{
    public AudioClip soundPlay;
    public int Action;
    private float secondsFinishPlay = 0.5f;
    private float secondsFinishSettings = 0.5f;
    private float secondsFinishQuit = 1.0f;
    private float secondsToScroll = 0.5f;
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
                Quit.SetActive(false);
                GameObject.FindGameObjectWithTag("Player").GetComponent<SwitchablePlayerController>().UnPauseGame();
                GameObject.FindGameObjectWithTag("Player").GetComponent<SwitchablePlayerController>().gamePaused = false;
                break;
            case 1: //Settings Menu
                GameObject.FindGameObjectWithTag("Player").GetComponent<SwitchablePlayerController>().gamePaused = false;
                GameObject.FindGameObjectWithTag("Player").GetComponent<SwitchablePlayerController>().UnPauseGame();
                SceneManager.LoadScene(1);
                //                GameObject.FindGameObjectWithTag("UI_InGame").GetComponent<ChangeScene>().shutdown = true;
                //                GameObject.FindGameObjectWithTag("UI_InGame").GetComponent<ChangeScene>().quitGame = true;

                Quit.SetActive(false);
                break;
        }
        yield return new WaitForSeconds(secondsFinishSettings);
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
                Cursor.transform.localPosition = new Vector3(-159, -78f, 0.0f);

                break;
            case 1: //Settings Menu
                Cursor = GameObject.FindGameObjectWithTag("Cursor").GetComponent<Transform>();
                Cursor.transform.localPosition = new Vector3(-159, 7.2f, 0.0f);
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
