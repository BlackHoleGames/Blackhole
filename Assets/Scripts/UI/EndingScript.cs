using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndingScript : MonoBehaviour {

    public Text TitleText;
    public Text TitleText2;
    public Text PressStartText;
    public Text Credits;
    public Image Curtain;
    public GameObject Cam1BH;
    public GameObject Cam1GO;
    //public GameObject Cam2BH;
    //public GameObject Cam2GO;
    //public GameObject Cam3BH;
    //public GameObject Cam3GO;
    public string loadLevel;
    public bool startCredits;
    public bool isLeaderBoardTime = true;
    // Use this for initialization
    private bool shutdown = false;

    IEnumerator Start()
    {

        TitleText.canvasRenderer.SetAlpha(0.0f);
        TitleText2.canvasRenderer.SetAlpha(0.0f);
        //PressStartText.canvasRenderer.SetAlpha(0.0f);
        Curtain.canvasRenderer.SetAlpha(1.0f);
        Curtain.CrossFadeAlpha(0.0f, 3.0f, false);
        yield return new WaitForSeconds(3.0f);
        /*InitCam1*/
        //TitleText.text = "GAME OVER \nTHERE'S NO HOPE FOR HUMANITY";
        FadeInTT();
        yield return new WaitForSeconds(3.0f);
        FadeOutTT();
        yield return new WaitForSeconds(3.0f);
        TitleText.text = "Unfortunately, nothing can escape a black hole...";
        FadeInTT();
        yield return new WaitForSeconds(3.0f);
        FadeInTT2();
        yield return new WaitForSeconds(3.0f);
        FadeOutTT();
        FadeOutTT2();
        yield return new WaitForSeconds(3.0f);
        TitleText.text = "You fade to black proud that the earth has a future";
        FadeInTT();
        yield return new WaitForSeconds(3.0f);
        FadeOutTT();
        yield return new WaitForSeconds(3.0f);
        startCredits = true;
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

        //        SceneManager.LoadScene(6);
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
    private void FadeOutTT2()
    {
        TitleText2.CrossFadeAlpha(0.0f, 1.0f, false);
    }

    private void FadeInTT2()
    {
        TitleText2.CrossFadeAlpha(1.0f, 1.0f, false);
    }
    void FadeInCurtain()
    {
        Curtain.CrossFadeAlpha(1.0f, 3.0f, false);
    }
    void FadeInText()
    {
        if(PressStartText!=null) PressStartText.CrossFadeAlpha(1.0f, 0.75f, false);
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
    IEnumerator ShutdownGame()
    {
        yield return new WaitForSeconds(3.0f);
        SceneManager.LoadScene(6);
    }
}
