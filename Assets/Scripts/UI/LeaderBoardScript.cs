using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class LeaderBoardScript : MonoBehaviour
{

    // Use this for initialization
    public Text splashLeaderBoards;
    public string loadLevel;
    public bool PStart;
    public bool isLeaderBoardTime = true;
    // Use this for initialization


    IEnumerator Start()
    {
        splashLeaderBoards.canvasRenderer.SetAlpha(0.0f);
        FadeInLeaderBoards();
        yield return new WaitForSeconds(3.0f);
        FadeOutLeaderBoards();
        yield return new WaitForSeconds(3.0f);

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
}
