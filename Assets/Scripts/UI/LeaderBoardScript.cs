using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class LeaderBoardScript : MonoBehaviour
{

    // Use this for initialization
    public Text splashLeaderBoards;
    public Text time;
    public Text Letter;
    public string loadLevel;
    public bool PStart;
    public bool isLeaderBoardTime = true;
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
    private int ScorePosition = 0;
    IEnumerator Start()
    {
        splashLeaderBoards.canvasRenderer.SetAlpha(0.0f);
        GameOverText.canvasRenderer.SetAlpha(0.0f);
        if (SaveGameStatsScript.GameStats.isGameOver) {
            if (SaveGameStatsScript.GameStats.isGoodEnding) GameOverText.text = "THE END";
            else GameOverText.text = "THE END";
            if (CompareScore()) {
                //Congrats Name
                ReasingScore();

            }
            ShowScore();
            FadeInLeaderBoards();
            yield return new WaitForSeconds(3.0f);
            FadeOutLeaderBoards();
            yield return new WaitForSeconds(3.0f);
        }else
        {
            ShowScore();
            FadeInLeaderBoards();
            yield return new WaitForSeconds(3.0f);
            FadeOutLeaderBoards();
            yield return new WaitForSeconds(3.0f);
        }
        SceneManager.LoadScene(1);
        
    }
    void FadeInLeaderBoards()
    {
        splashLeaderBoards.CrossFadeAlpha(1.0f, 1.5f, false);
    }
    void FadeOutLeaderBoards()
    {
        splashLeaderBoards.CrossFadeAlpha(0.0f, 2.5f, false);
    }
    void ShowScore()
    {
        SaveGameStatsScript.GameStats.GetScore();
        ScoreNameA =   GameObject.FindGameObjectWithTag("ScoreNameA").GetComponent<Text>();
        ScoreNameB =   GameObject.FindGameObjectWithTag("ScoreNameB").GetComponent<Text>();
        ScoreNameC =   GameObject.FindGameObjectWithTag("ScoreNameC").GetComponent<Text>();
        ScoreNameD =   GameObject.FindGameObjectWithTag("ScoreNameD").GetComponent<Text>();
        ScoreNameE =   GameObject.FindGameObjectWithTag("ScoreNameE").GetComponent<Text>();
        ScoreResultA = GameObject.FindGameObjectWithTag("ScoreResultA").GetComponent<Text>();
        ScoreResultB = GameObject.FindGameObjectWithTag("ScoreResultB").GetComponent<Text>();
        ScoreResultC = GameObject.FindGameObjectWithTag("ScoreResultC").GetComponent<Text>();
        ScoreResultD = GameObject.FindGameObjectWithTag("ScoreResultD").GetComponent<Text>();
        ScoreResultE = GameObject.FindGameObjectWithTag("ScoreResultE").GetComponent<Text>();
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
                ScoreResultA = GameObject.FindGameObjectWithTag("ScoreResultA").GetComponent<Text>();
                ScoreResultA.text = SaveGameStatsScript.GameStats.playerScore.ToString();
                break;
            case 1:
                ScoreResultB = GameObject.FindGameObjectWithTag("ScoreResultB").GetComponent<Text>();
                ScoreResultB.text = SaveGameStatsScript.GameStats.playerScore.ToString();
                break;
            case 2:
                ScoreResultC = GameObject.FindGameObjectWithTag("ScoreResultC").GetComponent<Text>();
                ScoreResultC.text = SaveGameStatsScript.GameStats.playerScore.ToString();
                break;
            case 3:
                ScoreResultD = GameObject.FindGameObjectWithTag("ScoreResultD").GetComponent<Text>();
                ScoreResultD.text = SaveGameStatsScript.GameStats.playerScore.ToString();
                break;
            case 4:
                ScoreResultE = GameObject.FindGameObjectWithTag("ScoreResultE").GetComponent<Text>();
                ScoreResultE.text = SaveGameStatsScript.GameStats.playerScore.ToString();
                break;
        }
        SaveGameStatsScript.GameStats.SetScore("FFF", SaveGameStatsScript.GameStats.playerScore.ToString());
    }


    public bool CompareScore()
    {
        for (int i=0; i < 5; i++)
        {
            if (System.Int32.Parse(SaveGameStatsScript.GameStats.ScoreResults[i])
                <= SaveGameStatsScript.GameStats.playerScore)
            {
                ScorePosition = i;
                return true;
            }
        }
        return false;
    }
}
