using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour {
    public AudioClip sound;
    public GameObject mainMenu;
    public GameObject settingsMenu;
    public GameObject Quit;
    private Button button { get { return GetComponent<Button>(); } }
    private AudioSource source1 { get { return GetComponent<AudioSource>(); } }
    // Use this for initialization

    public void PlayGame()
    {
        source1.PlayOneShot(sound);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
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
        Application.Quit();
    }
    void Update()
    {
        float axisY = Input.GetAxis("Vertical");

    }
}
