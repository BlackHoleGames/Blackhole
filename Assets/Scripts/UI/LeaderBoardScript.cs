using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class LeaderBoardScript : MonoBehaviour
{

    // Use this for initialization
    public Text splashLeaderBoards;
    
    public Text Letter;
    public string loadLevel;
    public bool WaitingForName = false;
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
        //COMENTAT ARA!        if (SaveGameStatsScript.GameStats.isGameOver) {
        //COMENTAT ARA!            if (SaveGameStatsScript.GameStats.isGoodEnding) GameOverText.text = "THE END";
        //COMENTAT ARA!            else GameOverText.text = "THE END";
        //COMENTAT ARA!            if (CompareScore()) {
        //COMENTAT ARA!                //Congrats Name
        //COMENTAT ARA!                while (!WaitingForName)
        //COMENTAT ARA!                {
        //COMENTAT ARA!                    //if (time.text == "0") break;
        //COMENTAT ARA!                }
        //COMENTAT ARA!                SaveGameStatsScript.GameStats.SetScore("", SaveGameStatsScript.GameStats.playerScore.ToString());
        //COMENTAT ARA!                ReasingScore();
        //COMENTAT ARA!
        //COMENTAT ARA!            }
        //COMENTAT ARA!            ShowScore();
        //COMENTAT ARA!            FadeInLeaderBoards();
        //COMENTAT ARA!            yield return new WaitForSeconds(3.0f);
        //COMENTAT ARA!            FadeOutLeaderBoards();
        //COMENTAT ARA!            yield return new WaitForSeconds(3.0f);
        //COMENTAT ARA!        }else
        //COMENTAT ARA!        {
        //COMENTAT ARA!            ShowScore();
        //COMENTAT ARA!            FadeInLeaderBoards();
        //COMENTAT ARA!            yield return new WaitForSeconds(3.0f);
        //COMENTAT ARA!            FadeOutLeaderBoards();
        //COMENTAT ARA!            yield return new WaitForSeconds(3.0f);
        //COMENTAT ARA!       }
        yield return new WaitForSeconds(5.0f);
        //COMENTAT ARA! SceneManager.LoadScene(1);

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
        //SaveGameStatsScript.GameStats.GetScore();
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
                ScorePosition = i;
                return true;
            }
        }
        return false;
    }
}
