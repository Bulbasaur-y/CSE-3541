using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//the super class
public class BaseUnit : MonoBehaviour
{
    public float leftMoveX = -2000f;   
    public float rightMoveX = 2000f;    

    public float minPower = 1549;   
    public float maxPower = 1653;  //the range that the character can hit

    public float moveSpeed = 13;    //character's speed 

    public Rigidbody ball;    
    public Animation aniController = null;  

    [HideInInspector] public HitType myHitType;      
    [HideInInspector] public HitType otherhitType;     

    [HideInInspector] public int score = 0;     

    static public float INIT_POS_DELTA_X = 5;    

     

    //start the ball
    public void PrepareStartBall()
    {
        //stop the ball
        ball.gameObject.SetActive(true);
        ball.velocity = Vector3.zero;
        ball.useGravity = false;   

        //set the ball at the racket
        ball.transform.position = transform.position + transform.forward * 2.8f + transform.up * 1.1f + transform.right * 0.55f;

        //set the character's gesture
        var aniClip = aniController.GetClip("StartBall");
        aniClip.SampleAnimation(gameObject, 0);

        //whose turn?
        GameController.hitType = myHitType;
    }

     
    public void StartBall()
    {
        
        if (GameController.hitType != myHitType) return;

         
        if (GameController.state != State.Prepare) return;

        GameController.state = State.Hit;

        //apply gravity to the ball
        ball.useGravity = true;

        //give the ball an angel of 45 degree
        var direction = (transform.forward + transform.up).normalized;
        AddForceToBall(direction, maxPower);

        //play startball animation
        aniController.Play("StartBall", PlayMode.StopSameLayer);
    }

    

    //hit the ball by adding a force
    public void AddForceToBall(Vector3 direction, float power)
    {
        GameController.hitType = otherhitType;
        ball.AddForce(direction * power);
    }

     
    public void HitBall()
    {
         
        aniController.Play("HitBall", PlayMode.StopSameLayer);
    }

     
    public void WinOneScore()
    {
 
        score++;
 
    }


     
    public void Jump()
    {
         //jump only when in hit state, otherwise the ball won't jump together.
            if (GameController.state == State.Hit)
        {
            aniController.Play("Jump", PlayMode.StopSameLayer);
        }

        
    }



    //play walk animation
    public void Walk()
    {
        aniController.Play("Walk", PlayMode.StopSameLayer);
    }


     
    public void Move(Vector3 dire, float speed)
    {
         
        if (GameController.state == State.Prepare)
        {
            if (transform.position.x < leftMoveX + INIT_POS_DELTA_X && dire.x < 0) return;
            if (transform.position.x > rightMoveX - INIT_POS_DELTA_X && dire.x > 0) return;
        }
         
        else
        {
            if (transform.position.x < leftMoveX && dire.x < 0) return;
            if (transform.position.x > rightMoveX && dire.x > 0) return;
        }
       
        
        transform.Translate(dire * Time.deltaTime * speed, Space.World);

       //move while walk
        Walk();
    }

    

    //stop moving
    public void StopWalk()
    {
        aniController.Stop("Walk");
        var aniClip = aniController.GetClip("Walk");
        aniClip.SampleAnimation(gameObject, 0.5f);
    }

    public void SetAnimationLayer()
    {
        aniController["HitBall"].layer = 1;
        aniController["StartBall"].layer = 2;
        aniController["Jump"].layer = 3;
        aniController["Walk"].layer = 4;
    }
}
