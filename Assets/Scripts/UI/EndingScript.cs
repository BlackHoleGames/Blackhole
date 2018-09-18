using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndingScript : MonoBehaviour {

    public Text TitleText;
    public Text PressStartText;
    public Image Curtain;
    public GameObject Cam1BH;
    public GameObject Cam1GO;
    //public GameObject Cam2BH;
    //public GameObject Cam2GO;
    //public GameObject Cam3BH;
    //public GameObject Cam3GO;
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
        TitleText.text = "C O N G R A T U L A T I O N S ! ! !\n You Save The Humanity";
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
        SceneManager.LoadScene(3);
    }

    void FadeInText()
    {
        PressStartText.CrossFadeAlpha(1.0f, 0.75f, false);
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
        PressStartText.CrossFadeAlpha(0.0f, 0.75f, false);
    }
}
