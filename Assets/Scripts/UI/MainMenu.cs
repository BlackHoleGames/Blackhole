using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour {

	// Use this for initialization
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Application.LoadLevel(Application.loadedLevel);
        //TimerScript TS = new TimerScript();
        //TS.startTime = Time.time;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
