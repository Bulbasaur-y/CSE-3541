using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 
public enum State
{
    None = 0,//none state 
    Prepare = 1,//prepare state 
    Hit = 2,//hit state 
    Over = 3,
}

//check it's whose turn
public enum HitType
{
    None = 0,
    Player = 1,
    Enemy = 2,
}

 
public class GameController : MonoBehaviour
{
    static public State state = State.None;     
    static public HitType hitType = HitType.None;    
     
    public Player player;    
    public Enemy enemy; //the opponent
    public Rigidbody ball;   
    public MainCanvas mainCanvas;   //UI

    [HideInInspector] public bool isStartOneGame = false;     
    [HideInInspector] public bool startType = false;     

    
    void Start()
    {
        StartGame();    
    }

     
    public void StartGame()
    {
        //set the gravity
        Physics.gravity = new Vector3(0, -20f, 0);

        //initial state
        ball.useGravity = false;
        ball.gameObject.SetActive(false);

        //set the scores to be 0
        player.score = 0;
        enemy.score = 0;

        //set the score board
        mainCanvas.ResetGame();

         
        //player stats the game
        StartCoroutine(StartOneGame(HitType.Player));
    }

    
    public IEnumerator StartOneGame(HitType startType)
    {
         
        yield return new WaitForSeconds(0.5f);

        //set initial position
        player.transform.position = new Vector3(player.leftMoveX + BaseUnit.INIT_POS_DELTA_X, player.transform.position.y, player.transform.position.z);    
        enemy.transform.position = new Vector3(enemy.rightMoveX - BaseUnit.INIT_POS_DELTA_X, enemy.transform.position.y, enemy.transform.position.z);

        //prepare to start the ball
        GameController.state = State.Prepare;
        if (startType == HitType.Player)
        {
            player.PrepareStartBall();
        }
        else if(startType == HitType.Enemy)
        {
            enemy.AutoStartBall();
        }
    }

     
    public void EndOneGame(HitType loseType)
    {
        //check the ball's on whose floor
        GameController.state = State.Over;
        var nextStartType = HitType.None;
        if (loseType == HitType.Enemy)
        {
            
            player.WinOneScore();
             
            mainCanvas.SetMyScore(player.score);
            nextStartType = HitType.Player;
        }
        else if (loseType == HitType.Player)
        {
              
            enemy.WinOneScore();
            
            mainCanvas.SetEnemyScore(enemy.score);
            nextStartType = HitType.Enemy;
        }
        
        

         
        if (!isGameOver)
        {
            //if not 5 score
            StartCoroutine(StartOneGame(nextStartType));
        }
        else
        {
            // total round ends
            mainCanvas.ShowResult(player.score >= 5);
        }
    }

     
    public bool isGameOver
    {
        get { return player.score >= 5 || enemy.score >= 5; }
    }
}
