using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndingScript : MonoBehaviour {

    public Text TitleText_1;
    //    public Text TitleText2;
    public Text TitleText_2;
    public Text TitleText_3;
    public Text TitleText_4;
    public Text PressStartText;
    public Text Credits;
    public Image Curtain;
    public GameObject Cam1BH;
    public GameObject Cam1GO;
    public string loadLevel;
    public bool startCredits;
    public bool isLeaderBoardTime = true;
    // Use this for initialization
    private bool shutdown = false;

    IEnumerator Start()
    {

        TitleText_1.canvasRenderer.SetAlpha(0.0f);
        TitleText_2.canvasRenderer.SetAlpha(0.0f);
        TitleText_3.canvasRenderer.SetAlpha(0.0f);
        TitleText_4.canvasRenderer.SetAlpha(0.0f);                
        Curtain.canvasRenderer.SetAlpha(1.0f);
        Curtain.CrossFadeAlpha(0.0f, 3.0f, false);
        yield return new WaitForSeconds(3.0f);
        /*InitCam1*/
        //TitleText.text = "GAME OVER \nTHERE'S NO HOPE FOR HUMANITY";
        TitleText_1.CrossFadeAlpha(1.0f, 2.0f, false);
        yield return new WaitForSeconds(6.0f);
        //TitleText.text = "Unfortunately, nothing can escape a black hole...";
        TitleText_2.CrossFadeAlpha(1.0f, 2.0f, false);
        yield return new WaitForSeconds(6.0f);
        TitleText_3.CrossFadeAlpha(1.0f, 2.0f, false);
        yield return new WaitForSeconds(2.0f);
        TitleText_4.CrossFadeAlpha(1.0f, 2.0f, false);
        yield return new WaitForSeconds(6.0f);
        //TitleText.text = "You fade to black proud that the earth has a future";
        //FadeInTT();
        //yield return new WaitForSeconds(3.0f);
        //FadeOutTT();
        //yield return new WaitForSeconds(3.0f);
        startCredits = true;

    }
    void Update()
    {
        //Acabar en 4400
        if (startCredits) Credits.transform.position += new Vector3(0.0f, Time.deltaTime * 100, 0.0f);
        if (Credits.transform.position.y > 5000.0f && !shutdown)
        {
            shutdown = true;
            FadeInCurtain();
            StartCoroutine(ShutdownGame());
        }
    }
    //private void FadeOutTT2()
    //{
    //    TitleText2.CrossFadeAlpha(0.0f, 1.0f, false);
    //}

    //private void FadeInTT2()
    //{
    //    TitleText2.CrossFadeAlpha(1.0f, 1.0f, false);
    //}
    void FadeInCurtain()
    {
        Curtain.CrossFadeAlpha(1.0f, 3.0f, false);
    }
    void FadeInText()
    {
        if(PressStartText!=null) PressStartText.CrossFadeAlpha(1.0f, 0.75f, false);
    }
    //void FadeInTT()
    //{
    //    TitleText.CrossFadeAlpha(1.0f, 1.0f, false);
    //}
    //void FadeOutTT()
    //{
    //    TitleText.CrossFadeAlpha(0.0f, 1.0f, false);
    //}
    void FadeOut()
    {
        if (PressStartText != null) PressStartText.CrossFadeAlpha(0.0f, 0.75f, false);
    }
    IEnumerator ShutdownGame()
    {
        yield return new WaitForSeconds(3.0f);
        SceneManager.LoadScene(6);
    }
}
