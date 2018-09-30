using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class QuitMenu : MonoBehaviour {

    public AudioClip sound;
    public Image Curtain;
    public Image Cursor;
    public GameObject mainMenu;
    public GameObject settingsMenu;
    public GameObject Quit;
    public GameObject QuitConfirm;
    private Button button { get { return GetComponent<Button>(); } }
    private AudioSource source1 { get { return GetComponent<AudioSource>(); } }
    // Use this for initialization
    IEnumerator Start()
    {
        Curtain.canvasRenderer.SetAlpha(0.0f);
        Curtain.CrossFadeAlpha(0.0f, 1.0f, false);
        yield return new WaitForSeconds(1.0f);
    }
    public void PlayGame()
    {
        source1.PlayOneShot(sound);
        SceneManager.LoadScene(3);
        //Application.LoadLevel(Application.loadedLevel);
        //TimerScript TS = new TimerScript();
        //TS.startTime = Time.time;
    }
    public void Settings()
    {

        mainMenu.SetActive(false);
        //source1.PlayOneShot(sound);
        settingsMenu.SetActive(true);
    }

    public void QuitGame()
    {
        //Application.Quit();
        mainMenu.SetActive(false);
        QuitConfirm.SetActive(true);
    }
    void Update()
    {
        float axisY = Input.GetAxis("Vertical");

    }
}
