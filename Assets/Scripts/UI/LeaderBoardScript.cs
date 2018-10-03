using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using System;

public class LeaderBoardScript : MonoBehaviour
{

    // Use this for initialization
    public Text LeaderBoardsTitle;
    
    public Text Letter;
    public Text congrats;
    public Text numPosition;
    public string loadLevel;
    public bool WaitingForName = false;
    public bool isLeaderBoardTime = false;
    private Text ScoreNameA;
    private Text ScoreNameB;
    private Text ScoreNameC;
    private Text ScoreNameD;
    private Text ScoreNameE;
    private Text ScoreResultA;
    private Text ScoreResultB;
    private Text ScoreResultC;
    private Text ScoreResultD;
    private Text ScoreResultE;
    public Text GameOverText;
    public Text Timer;
    public Text Initials;
    private int ScorePosition = 0;
    private int ObjectsToShow = 6;
    private IEnumerator ScoreSequence, InitialSequence;
    void Start()
    {
        Time.timeScale = 1.0f;
        LeaderBoardsTitle.canvasRenderer.SetAlpha(0.0f);
        GameOverText.canvasRenderer.SetAlpha(0.0f);
        GettingTags();
        ScoreNameA.canvasRenderer.SetAlpha(0.0f);
        ScoreNameB.canvasRenderer.SetAlpha(0.0f);
        ScoreNameC.canvasRenderer.SetAlpha(0.0f);
        ScoreNameD.canvasRenderer.SetAlpha(0.0f);
        ScoreNameE.canvasRenderer.SetAlpha(0.0f);
        ScoreResultA.canvasRenderer.SetAlpha(0.0f);
        ScoreResultB.canvasRenderer.SetAlpha(0.0f);
        ScoreResultC.canvasRenderer.SetAlpha(0.0f);
        ScoreResultD.canvasRenderer.SetAlpha(0.0f);
        ScoreResultE.canvasRenderer.SetAlpha(0.0f);
        numPosition.canvasRenderer.SetAlpha(0.0f);
        congrats.canvasRenderer.SetAlpha(0.0f);
        Timer.canvasRenderer.SetAlpha(0.0f);
        Initials.canvasRenderer.SetAlpha(0.0f);
        isLeaderBoardTime = false;
        if (SaveGameStatsScript.GameStats!=null && SaveGameStatsScript.GameStats.isGameOver) {            
            //if (SaveGameStatsScript.GameStats.isGoodEnding) GameOverText.text = "THE END";
            //else GameOverText.text = "Game Over";
            if (CompareScore()) {
                //Congrats Name
                GetComponent<InputNameScore>().IsInputName = true;
                setPosition();
                InitialSequence = InitialPresentation(0.5f);
                StartCoroutine(InitialSequence);
                //Timer.gameObject.SetActive(true);
                //Initials.gameObject.SetActive(true);
                //while (!GameObject.FindGameObjectWithTag("UIScore").GetComponent<InputNameScore>().WaitingForName){}
                //  SaveGameStatsScript.GameStats.SetScore("", SaveGameStatsScript.GameStats.playerScore.ToString());
                //ReasingScore();
            }
            else {
                Timer.gameObject.SetActive(false);
                Initials.gameObject.SetActive(false);
                ShowScore();
                ScoreSequence = ScorePresentation(0.5f);
                StartCoroutine(ScoreSequence);
            }
        }else{
            Timer.gameObject.SetActive(false);
            Initials.gameObject.SetActive(false);
            ShowScore();
            ScoreSequence = ScorePresentation(0.5f);
            StartCoroutine(ScoreSequence);
        }
    }

    private IEnumerator InitialPresentation(float timeToAppear)
    {
        Initials.CrossFadeAlpha(1.0f, timeToAppear, false);
        Timer.CrossFadeAlpha(1.0f, timeToAppear, false);
        congrats.CrossFadeAlpha(1.0f, timeToAppear, false);
        numPosition.CrossFadeAlpha(1.0f, timeToAppear, false);
        yield return new WaitForSeconds(timeToAppear);
    }

    private void setPosition()
    {
        switch (ScorePosition)
        {
            case 1:
                numPosition.text = "1ST";
            break;
            case 2:
                numPosition.text = "2ND";
                break;
            case 3:
                numPosition.text = "3RD";
                break;
            case 4:
                numPosition.text = "4TH";
                break;
            case 5:
                numPosition.text = "5TH";
                break;
        }

    }

    void Update()
    {
        if (isLeaderBoardTime)
        {
            isLeaderBoardTime = false;
            //SaveGameStatsScript.GameStats.SetScore(GameObject.FindGameObjectWithTag("UIScore").GetComponent<InputNameScore>().name, SaveGameStatsScript.GameStats.playerScore.ToString());
            ReasingScore();
            ShowScore();
            ScoreSequence = ScorePresentation(0.5f);
            StartCoroutine(ScoreSequence);
        }
    }
    IEnumerator ScorePresentation(float delimitedTimer)
    {
        for (int i = 0; i < ObjectsToShow; i++) {
            switch (i)
            {
                case 0:
                    LeaderBoardsTitle.CrossFadeAlpha(1.0f, 1.5f, false);
                    yield return new WaitForSeconds(delimitedTimer);
                break;
                case 1:
                    ScoreNameA.CrossFadeAlpha(1.0f, 1.5f, false);
                    ScoreResultA.CrossFadeAlpha(1.0f, 1.5f, false);
                    yield return new WaitForSeconds(delimitedTimer);
                break;
                case 2:
                    ScoreNameB.CrossFadeAlpha(1.0f, 1.5f, false);
                    ScoreResultB.CrossFadeAlpha(1.0f, 1.5f, false);
                    yield return new WaitForSeconds(delimitedTimer);
                    break;
                case 3:
                    ScoreNameC.CrossFadeAlpha(1.0f, 1.5f, false);
                    ScoreResultC.CrossFadeAlpha(1.0f, 1.5f, false);
                    yield return new WaitForSeconds(delimitedTimer);
                break;
                case 4:
                    ScoreNameD.CrossFadeAlpha(1.0f, 1.5f, false);
                    ScoreResultD.CrossFadeAlpha(1.0f, 1.5f, false);
                    yield return new WaitForSeconds(delimitedTimer);
                break;
                case 5:
                    ScoreNameE.CrossFadeAlpha(1.0f, 1.5f, false);
                    ScoreResultE.CrossFadeAlpha(1.0f, 1.5f, false);
                    yield return new WaitForSeconds(delimitedTimer);
                break;
            }            
        }
        //Ending
        //SaveGameStatsScript.GameStats.StatusUISequence = false;
        SaveGameStatsScript.GameStats.isGameOver = false;
        SaveGameStatsScript.GameStats.playerScore = 0;
        SaveGameStatsScript.GameStats.SetStats();
        yield return new WaitForSeconds(20.0f);
        SceneManager.LoadScene(2);
    }
    void GettingTags()
    {
        ScoreNameA = GameObject.FindGameObjectWithTag("ScoreNameA").GetComponent<Text>();
        ScoreNameB = GameObject.FindGameObjectWithTag("ScoreNameB").GetComponent<Text>();
        ScoreNameC = GameObject.FindGameObjectWithTag("ScoreNameC").GetComponent<Text>();
        ScoreNameD = GameObject.FindGameObjectWithTag("ScoreNameD").GetComponent<Text>();
        ScoreNameE = GameObject.FindGameObjectWithTag("ScoreNameE").GetComponent<Text>();
        ScoreResultA = GameObject.FindGameObjectWithTag("ScoreResultA").GetComponent<Text>();
        ScoreResultB = GameObject.FindGameObjectWithTag("ScoreResultB").GetComponent<Text>();
        ScoreResultC = GameObject.FindGameObjectWithTag("ScoreResultC").GetComponent<Text>();
        ScoreResultD = GameObject.FindGameObjectWithTag("ScoreResultD").GetComponent<Text>();
        ScoreResultE = GameObject.FindGameObjectWithTag("ScoreResultE").GetComponent<Text>();
    }
    void ShowScore()
    {
        //SaveGameStatsScript.GameStats.GetScore();
        //You can't invoke before menu
        if(SaveGameStatsScript.GameStats!=null) SaveGameStatsScript.GameStats.GetScore();
        GettingTags();
        LeaderBoard();
    }
    void LeaderBoard()
    {
        ScoreNameA.text = SaveGameStatsScript.GameStats.ScoreNames[0];
        ScoreNameB.text = SaveGameStatsScript.GameStats.ScoreNames[1];
        ScoreNameC.text = SaveGameStatsScript.GameStats.ScoreNames[2];
        ScoreNameD.text = SaveGameStatsScript.GameStats.ScoreNames[3];
        ScoreNameE.text = SaveGameStatsScript.GameStats.ScoreNames[4];

        ScoreResultA.text = SaveGameStatsScript.GameStats.ScoreResults[0].ToString();
        ScoreResultB.text = SaveGameStatsScript.GameStats.ScoreResults[1].ToString();
        ScoreResultC.text = SaveGameStatsScript.GameStats.ScoreResults[2].ToString();
        ScoreResultD.text = SaveGameStatsScript.GameStats.ScoreResults[3].ToString();
        ScoreResultE.text = SaveGameStatsScript.GameStats.ScoreResults[4].ToString();
    }
    void ReasingScore()
    {
        switch (ScorePosition)
        {
            case 0:
                ScoreNameA = GameObject.FindGameObjectWithTag("ScoreNameA").GetComponent<Text>();
                ScoreNameA.text = "";//SaveGameStatsScript.GameStats.playerScore.ToString();
                ScoreResultA = GameObject.FindGameObjectWithTag("ScoreResultA").GetComponent<Text>();
                ScoreResultA.text = SaveGameStatsScript.GameStats.playerScore.ToString();
                break;
            case 1:
                ScoreNameB = GameObject.FindGameObjectWithTag("ScoreNameB").GetComponent<Text>();
                ScoreNameB.text = "";
                ScoreResultB = GameObject.FindGameObjectWithTag("ScoreResultB").GetComponent<Text>();
                ScoreResultB.text = SaveGameStatsScript.GameStats.playerScore.ToString();
                break;
            case 2:
                ScoreNameC = GameObject.FindGameObjectWithTag("ScoreNameC").GetComponent<Text>();
                ScoreNameC.text = "";
                ScoreResultC = GameObject.FindGameObjectWithTag("ScoreResultC").GetComponent<Text>();
                ScoreResultC.text = SaveGameStatsScript.GameStats.playerScore.ToString();
                break;
            case 3:
                ScoreNameD = GameObject.FindGameObjectWithTag("ScoreNameD").GetComponent<Text>();
                ScoreNameD.text = "";
                ScoreResultD = GameObject.FindGameObjectWithTag("ScoreResultD").GetComponent<Text>();
                ScoreResultD.text = SaveGameStatsScript.GameStats.playerScore.ToString();
                break;
            case 4:
                ScoreNameE = GameObject.FindGameObjectWithTag("ScoreNameE").GetComponent<Text>();
                ScoreNameE.text = "";
                ScoreResultE = GameObject.FindGameObjectWithTag("ScoreResultE").GetComponent<Text>();
                ScoreResultE.text = SaveGameStatsScript.GameStats.playerScore.ToString();
                break;
        }        
    }


    public bool CompareScore()
    {
        for (int i=0; i < 5; i++)
        {
            if (System.Int32.Parse(SaveGameStatsScript.GameStats.ScoreResults[i])
                <= SaveGameStatsScript.GameStats.playerScore)
            {
                ScorePosition = i+1;
                return true;
            }
        }
        return false;
    }
}
