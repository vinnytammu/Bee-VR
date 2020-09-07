using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GameState
{
    GameStart, GamePlay, GameEnd
};

public enum GameResult
{
    GameWin, GameLose
};

public class GameManager : MonoBehaviour
{


    public static GameState gamestate = GameState.GameStart;

    GameResult gameResult;

    public PlayerMode playermode;
    public Canvas canvas_GameStart;
    public Canvas canvas_GamePlay;
    public Canvas canvas_GameEnd;

    public int hiveNectar = 0; // Nectar in hive now 
    public int goalNectar = 10; // Nectar goal for win
    public float timeLimit = 300.0f;
    public float warningTime = 30;
    float playTime = 0;

    public Text text_currentTime;
    public Text text_GoalNectar;
    public Text text_warning;
    public Text text_GameResult;

    public AudioManage audioManager;

    bool notOver = true;
    bool haveWon = false; 

    void Start()
    {



    }

    void Update()
    {

        switch (gamestate)
        {
            case GameState.GameStart:
                {
                    canvas_GamePlay.gameObject.SetActive(false);
                    //audioManager.Play("Intro");

                    //****TEST CODE : Press "Space" to start game ****
                    if  (OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger) > 0 && OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger) > 0 && OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger) > 0 && OVRInput.Get(OVRInput.Axis1D.SecondaryHandTrigger) > 0)   //(Input.GetKeyUp(KeyCode.Space))
                    {
                        gamestate = GameState.GamePlay;
                        audioManager.Play("HowToFly"); 
                        //FindObjectOfType<AudioManage>().Play("HowToFly"); 
                    }
                }
                break;

            case GameState.GamePlay:
                {
                    canvas_GameStart.gameObject.SetActive(false);
                    canvas_GamePlay.gameObject.SetActive(true);

                    //Time count and display
                    playTime += Time.deltaTime;

                    if (((int)(timeLimit - playTime) % 60) < 10)
                    {
                        text_currentTime.text = ((int)(timeLimit - playTime) / 60) + ":0" + ((int)(timeLimit - playTime) % 60);
                    }
                    else
                    {
                        text_currentTime.text = ((int)(timeLimit - playTime) / 60) + ":" + ((int)(timeLimit - playTime) % 60);
                    }
                    //display "hive nectar / total Nectar"
                    text_GoalNectar.text = hiveNectar + "/" + goalNectar;
                    



                    //Timer check
                    //Time ran out -> lose
                    if ((playTime > timeLimit) && notOver == true)
                    {   //todo:add Sound : game over remaining lines
                        audioManager.Play("GameOver");
                        notOver = false;
                        gamestate = GameState.GameEnd;
                        gameResult = GameResult.GameLose;
                    }
                    // Warning : when (warningTime) sec left
                    else if ((timeLimit - playTime) <= warningTime && (timeLimit - playTime) >= (warningTime - 2))
                    {
                        text_warning.gameObject.SetActive(true);
                        text_warning.text = warningTime + "sec left!";
                        StartCoroutine(CheckWarningMsg());
                    }


                    //Reached the goal -> win
                    if ((hiveNectar >= goalNectar) && haveWon == false)
                    {   //todo:add Sound : game win remaining lines
                        audioManager.Play("GameWin");
                        haveWon = true; 
                        gamestate = GameState.GameEnd;
                        gameResult = GameResult.GameWin;
                    }


                }
                break;
            case GameState.GameEnd:
                {

//                    canvas_GamePlay.gameObject.SetActive(false);
                    canvas_GameEnd.gameObject.SetActive(true);

                    playermode.playerMode = Mode.Landing;


                    if (gameResult == GameResult.GameLose)
                    {
                        text_GameResult.text = "Game Lose";
                        Debug.Log("Game end : Lose");
                    }

                    else
                    {
                        Debug.Log("Game end : Win");
                        text_GameResult.text = "Game Win";
                    }
                }
                break;
        }

    }

    IEnumerator CheckWarningMsg()
    {
        yield return new WaitForSeconds(2f);
        text_warning.gameObject.SetActive(false);
    }
}
