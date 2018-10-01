﻿using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class GameOverScript : MonoBehaviour {

    public Text TitleText;
    public Text PressStartText;
    public Image Curtain;
    public string loadLevel;
    public bool PStart;
    public bool isLeaderBoardTime = true;
    // Use this for initialization


    IEnumerator Start()
    {

        TitleText.canvasRenderer.SetAlpha(0.0f);
        //PressStartText.canvasRenderer.SetAlpha(0.0f);
        Curtain.canvasRenderer.SetAlpha(1.0f);
        Curtain.CrossFadeAlpha(0.0f, 3.0f, false);
        yield return new WaitForSeconds(3.0f);
        /*InitCam1*/
        //TitleText.text = "GAME OVER \nTHERE'S NO HOPE FOR HUMANITY";
        FadeInTT();
        yield return new WaitForSeconds(2.0f);
        for (int i = 0; i < 5; i++)
        {
            FadeInText();
            yield return new WaitForSeconds(0.75f);
            FadeOut();
            yield return new WaitForSeconds(0.75f);
        }
        Curtain.CrossFadeAlpha(1.0f, 3.0f, false);
        FadeOutTT();
        yield return new WaitForSeconds(3.0f);
        /*Ending Cam1*/
        ////Cam1BH = Cam2BH;
        //Cam1BH.SetActive(false);
        //Cam2BH.SetActive(true);
        ////Cam2BH = Camera.main;
        //Cam2GO.SetActive(true);
        //Cam1GO.SetActive(false);
        //Curtain.CrossFadeAlpha(0.0f, 3.0f, false);
        //yield return new WaitForSeconds(3.0f);
        ///*Init Cam2*/
        //TitleText.text = "...from which emerged an alien race that has nearly destroyed the earth...";
        //FadeInTT();
        //yield return new WaitForSeconds(2.0f);
        //for (int i = 0; i < 5; i++)
        //{
        //    FadeInText();
        //    yield return new WaitForSeconds(0.75f);
        //    FadeOut();
        //    yield return new WaitForSeconds(0.75f);
        //}
        //Curtain.CrossFadeAlpha(1.0f, 3.0f, false);
        //FadeOutTT();
        //yield return new WaitForSeconds(3.0f);
        ///*Ending Cam2*/
        ////Cam1BH = Cam3BH;
        //Cam2BH.SetActive(false);
        //Cam3BH.SetActive(true);
        //Cam3GO.SetActive(true);
        //Cam2GO.SetActive(false);
        //Curtain.CrossFadeAlpha(0.0f, 3.0f, false);
        //yield return new WaitForSeconds(3.0f);
        ///*Init Cam3*/
        //TitleText.text = "Scarlett, you are the last hope to save our world.";
        //FadeInTT();
        //yield return new WaitForSeconds(2.0f);
        //for (int i = 0; i < 5; i++)
        //{
        //    FadeInText();
        //    yield return new WaitForSeconds(0.75f);
        //    FadeOut();
        //    yield return new WaitForSeconds(0.75f);
        //}
        SceneManager.LoadScene(1);
    }

    void FadeInText()
    {
        if (PressStartText != null) PressStartText.CrossFadeAlpha(1.0f, 0.75f, false);
    }
    void FadeInTT()
    {
        TitleText.CrossFadeAlpha(1.0f, 1.0f, false);
    }
    void FadeOutTT()
    {
        TitleText.CrossFadeAlpha(0.0f, 1.0f, false);
    }
    void FadeOut()
    {
        if (PressStartText != null) PressStartText.CrossFadeAlpha(0.0f, 0.75f, false);
    }

    //// Use this for initialization
    //public Text splashText;
    //public Text splashLeaderBoards;
    //public string loadLevel;
    //public bool PStart;

    //// Use this for initialization
    //IEnumerator Start()
    //{        
    //    splashText.canvasRenderer.SetAlpha(0.0f);
    //    if (!TimerScript.isGameOver) splashText.text = "THE END";
    //    splashLeaderBoards.canvasRenderer.SetAlpha(0.0f);
    //    FadeInGameOver();
    //    yield return new WaitForSeconds(3.0f);
    //    FadeOutGameOver();
    //    yield return new WaitForSeconds(3.0f);
    //    FadeInLeaderBoards();
    //    yield return new WaitForSeconds(3.0f);
    //    FadeOutLeaderBoards();
    //    yield return new WaitForSeconds(3.0f);
    //    SceneManager.LoadScene(3);
    //}

    //// Update is called once per frame

    //void FadeInGameOver()
    //{
    //    splashText.CrossFadeAlpha(1.0f, 1.5f, false);
    //}
    //void FadeOutGameOver()
    //{
    //    splashText.CrossFadeAlpha(0.0f, 2.5f, false);
    //}
    //void FadeInLeaderBoards()
    //{
    //    splashLeaderBoards.CrossFadeAlpha(1.0f, 1.5f, false);
    //}
    //void FadeOutLeaderBoards()
    //{
    //    splashLeaderBoards.CrossFadeAlpha(0.0f, 2.5f, false);
    //}
}
