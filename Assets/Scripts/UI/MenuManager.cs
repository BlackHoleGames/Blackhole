using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{

    private void Start()
    {
        Time.timeScale = 0f;
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void ActivatePanel(GameObject panel)
    {
        panel.SetActive(true);

    }

    public void Back(GameObject actualPanel)
    {
        actualPanel.SetActive(false);
    }

    public void Play(GameObject player)
    {
        //player.SetActive(true);
        Time.timeScale = 1f;
    }
    //Check OnApplicationPause and how it works
    public void Pause()
    {
        Time.timeScale = 0f;
    }
    public void Resume()
    {
        Time.timeScale = 1f;
    }
}
