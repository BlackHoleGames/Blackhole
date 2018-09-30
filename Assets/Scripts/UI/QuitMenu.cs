using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class QuitMenu : MonoBehaviour {

    public AudioClip sound;
    public AudioClip soundChange;
    public Image Curtain;
    public int Action;
    private float secondsReturnPlay = 0.5f;
    private float secondsFinishSettings = 0.5f;
    public AudioClip soundPlay;
    public Transform Cursor;
    public GameObject mainMenu;
    public GameObject settingsMenu;
    public GameObject Quit;
    public GameObject QuitConfirm;
    public GameObject quitMenu;
    private TimeManager tm;
    public bool activateScroll = true;
    private Button buttonPlay { get { return GetComponent<Button>(); } }
    private Button button { get { return GetComponent<Button>(); } }
    private AudioSource source1 { get { return GetComponent<AudioSource>(); } }
    private AudioSource sourcePlay { get { return GetComponent<AudioSource>(); } }
    private AudioSource sourceChange { get { return GetComponent<AudioSource>(); } }
    // Use this for initialization
    IEnumerator Start()
    {
        gameObject.AddComponent<AudioSource>();
        tm = GetComponent<TimeManager>();
        sourcePlay.clip = soundPlay;
        sourcePlay.playOnAwake = false;
        buttonPlay.onClick.AddListener(() => StartCoroutine(playFileStart()));
        Curtain.canvasRenderer.SetAlpha(0.0f);
        Curtain.CrossFadeAlpha(0.0f, 1.0f, false);
        yield return new WaitForSeconds(1.0f);
    }
    //public void PlayGame()
    //{
    //    source1.PlayOneShot(sound);
    //    SceneManager.LoadScene(3);
    //    //Application.LoadLevel(Application.loadedLevel);
    //    //TimerScript TS = new TimerScript();
    //    //TS.startTime = Time.time;
    //}
    //public void Settings()
    //{

    //    mainMenu.SetActive(false);
    //    //source1.PlayOneShot(sound);
    //    settingsMenu.SetActive(true);
    //}

    //public void QuitGame()
    //{
    //    //Application.Quit();
    //    mainMenu.SetActive(false);
    //    QuitConfirm.SetActive(true);
    //}
    void Update()
    {
        float axisY = Input.GetAxis("Vertical");

    }
    IEnumerator playFileScroll()
    {
        switch (Action)
        {
            case 0: //Quit NoConfirm
                Cursor = GameObject.FindGameObjectWithTag("Cursor").GetComponent<Transform>();
                Cursor.transform.localPosition = new Vector3(-159, -164f, 0.0f);
                break;
            case 1: //Quit Confirm
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
    IEnumerator playFileStart()
    {
        sourcePlay.PlayOneShot(soundPlay);

        switch (Action)
        {
            case 0: //Quit NoConfirm
                yield return new WaitForSeconds(secondsReturnPlay);
                tm.UnPauseGame();
                quitMenu.SetActive(false);
            break;
            case 1: //Quit Confirm
                yield return new WaitForSeconds(secondsFinishSettings);
                GameObject.FindGameObjectWithTag("UI_InGame").GetComponent<ChangeScene>().shutdown = true;
                GameObject.FindGameObjectWithTag("UI_InGame").GetComponent<ChangeScene>().quitGame = true;
                break;
        }

    }
}
