using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MainCanvas : MonoBehaviour
{
    public Text myScore;     
    public Text enemyScore;  
    public GameObject resultGo;    
    public Text labelResult;    
    public Button buttonRestart;     
    public Button buttonQuit;  
   
    public GameController gameController;

   
    void Awake()
    {
         
        buttonRestart.GetComponent<Button>().onClick.AddListener(ClickRestart);
        buttonQuit.GetComponent<Button>().onClick.AddListener(ClickQuit);
      

        //set score to 00
        myScore.text = "00";
        enemyScore.text = "00";
    }

    void OnDestroy()
    {
        buttonRestart.GetComponent<Button>().onClick.RemoveListener(ClickRestart);
        buttonQuit.GetComponent<Button>().onClick.RemoveListener(ClickQuit);
     
    }

    void Update()
    {
        
    }

    //initiliation
    public void ResetGame()
    {
        //UI
        resultGo.SetActive(false);

        //scores
        SetMyScore(0);
        SetEnemyScore(0);
    }

    //your score
    public void SetMyScore(int score)
    {
        myScore.text = "0" + score;
    }

    //opponent's score
    public void SetEnemyScore(int score)
    {
        enemyScore.text = "0" + score;
    }

    //show result
    public void ShowResult(bool playerwin)
    {
        if (playerwin)
        {
            labelResult.text = "Congratulations! You Win!";
        }
        else
        {
            labelResult.text = "Sorry! You Lose!";
        }
        resultGo.SetActive(true);
    }

    //restart
    void ClickRestart()
    {
        gameController.StartGame();
    }

    //quit
    void ClickQuit()
    {
        UnityEditor.EditorApplication.isPlaying = false;
    }

 
    
}
