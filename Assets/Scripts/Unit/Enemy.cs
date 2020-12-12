using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 
public class Enemy : BaseUnit
{
    public float ballFrontPos = 3;    //the postion that the opponent can hit, front
    public float ballBackPos = 2;    //the postion that the opponent can hit, back
    public float minXDis = 0.5f;    //the range in whitch the opponent can stop
    public float giveUpHight = 3;    //the height that the opponent wanna give up

    void Start()
    {
        //this is for the enemy
        myHitType = HitType.Enemy;  
        //the other side is player 
        otherhitType = HitType.Player;
        
        SetAnimationLayer();
    }

     
    public void AutoStartBall()
    {
        
        StartCoroutine(AutoStartBallCoroutine());
    }

     
    public IEnumerator AutoStartBallCoroutine()
    {
        //get the gesture before start
        PrepareStartBall();
        // wait for 3s
        yield return new WaitForSeconds(3);
        
        StartBall();
    }

    void Update()
    {
        //the enemy auto hit back
        if (GameController.hitType == HitType.Enemy)
        {
            //the enemy find a range to go
            var realX = Random.Range(ball.transform.position.x - ballFrontPos, ball.transform.position.x + ballBackPos);
            var nextPos = new Vector3(realX, transform.position.y, transform.position.z);

            //evaluate the ball's height
            var yDis = ball.transform.position.y - transform.position.y;

            //the enenmy needs to move or not 
            var isNeedMove = ball.transform.position.x > 0   
                         && nextPos.x > 0   
                         && yDis >= giveUpHight    
                         && Mathf.Abs(nextPos.x - transform.position.x) > minXDis;   

            if (!isNeedMove)
            {
   
                StopWalk(); 
             
            }
            else
            {
                 
                var direction = (nextPos - transform.position).normalized;
                Move(direction, moveSpeed);
            }

            //evaluate whether can hit the ball or not
            var center = new Vector3(transform.position.x, 2.3f, 0);     
            float radius = 4;     //arm (including racket)
            if (ball.transform.position.x > transform.position.x)   //hit before head
            {
                radius = 4.3f;
            }
            var dis = Vector3.Distance(center, ball.transform.position);     



            //within the radius, can hit
             if (dis <= radius && yDis >= giveUpHight)
             {
                HitBall();
             }


        }
        else
        {
            
            StopWalk();
        }
    }
}
