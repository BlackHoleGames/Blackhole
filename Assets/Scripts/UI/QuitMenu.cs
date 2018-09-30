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

    void Update()
    {
        float axisY = Input.GetAxis("Vertical");
        //if (axisY > 0.0f) Action++;
        //else Action = 0;
        //if (Action > 1) Action = 0;
    }
    IEnumerator playFileScroll()
    {
        switch (Action)
        {
            case 0: //Quit NoConfirm
                Cursor = GameObject.FindGameObjectWithTag("Cursor").GetComponent<Transform>();
                Cursor.transform.localPosition = new Vector3(-180, -79f, 0.0f);
                break;
            case 1: //Quit Confirm
                Cursor = GameObject.FindGameObjectWithTag("Cursor").GetComponent<Transform>();
                Cursor.transform.localPosition = new Vector3(-180, -164f, 0.0f);

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

                quitMenu.SetActive(false);
                GameObject.FindGameObjectWithTag("Player").GetComponent<SwitchablePlayerController>().UnPauseGame();
            break;
            case 1: //Quit Confirm
//                tm.UnPauseGame();
                GameObject.FindGameObjectWithTag("UI_InGame").GetComponent<ChangeScene>().shutdown = true;
                GameObject.FindGameObjectWithTag("UI_InGame").GetComponent<ChangeScene>().quitGame = true;
                GameObject.FindGameObjectWithTag("Player").GetComponent<SwitchablePlayerController>().UnPauseGame();
                break;
        }
        yield return new WaitForSeconds(secondsReturnPlay);
    }
}
