using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HistoryScreen : MonoBehaviour {

    public Text TitleText;
    public Text TittleScarlett;
    public Image Curtain;
    public GameObject Cam1BH;
    public GameObject Cam1GO;
    public GameObject Cam2BH;
    public GameObject Cam2GO;
    public GameObject Cam3BH;
    public GameObject Cam3GO;
    public string loadLevel;
    public bool PStart;
    public bool isLeaderBoardTime = true;
    private float Active = 1.0f;
    private IEnumerator FlickAction;
    // Use this for initialization


    IEnumerator Start()
    {
        Time.timeScale = 1.0f;
        SaveGameStatsScript.GameStats.isGameOver = false;
        TitleText.canvasRenderer.SetAlpha(0.0f);
        TittleScarlett.canvasRenderer.SetAlpha(0.0f);
        Curtain.canvasRenderer.SetAlpha(Active);
        Curtain.CrossFadeAlpha(0.0f, 1.0f, false);
        yield return new WaitForSeconds(1.0f);
        /*InitCam1*/
        FadeInTT();
        yield return new WaitForSeconds(6.0f);
        Curtain.CrossFadeAlpha(1.0f, 1.0f, false);
        yield return new WaitForSeconds(2.0f);
        FadeOutTT();
        yield return new WaitForSeconds(2.0f);
        /*Ending Cam1*/
        EnableCam2();
        Curtain.CrossFadeAlpha(0.0f, 2.0f, false);
        yield return new WaitForSeconds(2.0f);
        /*Init Cam2*/
        TitleText.text = "...from which emerged an alien race that has nearly destroyed the earth...";
        FadeInTT();
        yield return new WaitForSeconds(4.0f);
        Curtain.CrossFadeAlpha(Active,1.5f, false);
        FadeOutTT();
        yield return new WaitForSeconds(1.3f);
        /*Ending Cam2*/
        /*Init Cam3*/
        EnableCam3();
        Curtain.CrossFadeAlpha(0.0f, 2.0f, false);
        yield return new WaitForSeconds(2.0f);
        TittleScarlett.CrossFadeAlpha(Active, 1.5f, false);
        yield return new WaitForSeconds(1.5f);
        TitleText.text = "you are the last hope to save our world!";
        FadeInTT();
        yield return new WaitForSeconds(3.0f);
        Curtain.CrossFadeAlpha(1.0f, 4.0f, false);
        yield return new WaitForSeconds(4.0f);
        SceneManager.LoadScene(2);
    }

    void FadeInTT()
    {
        TitleText.CrossFadeAlpha(1.0f, 1.0f, false);
    }
    void FadeOutTT()
    {
        TitleText.CrossFadeAlpha(0.0f, 1.0f, false);
    }

    public void EnableCam2()
    {
        Cam1BH.SetActive(false);
        Cam2BH.SetActive(true);
        Cam2GO.SetActive(true);
        Cam1GO.SetActive(false);
    }
    public void EnableCam3()
    {
        Cam2BH.SetActive(false);
        Cam3BH.SetActive(true);
        Cam3GO.SetActive(true);
        Cam2GO.SetActive(false);
    }

}
