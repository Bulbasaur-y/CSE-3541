using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : BaseUnit
{
    void Start()
    {
        //this script is for player
        myHitType = HitType.Player;
        //the other side is teh enemy
        otherhitType = HitType.Enemy;
         
        SetAnimationLayer();
    }

    void Update()
    {


         //hit W to jump
        if (Input.GetKeyDown(KeyCode.W))
        {
            Jump();
        }

        //hit S to stroke/start
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (GameController.hitType == HitType.Player && GameController.state == State.Prepare)
            {
                StartBall();
            }
            else 
            {
                HitBall();
            }
        }

        


        //hit A to move forward
        if (Input.GetKey(KeyCode.A))
        {
            Move(Vector3.left, moveSpeed);

            //the ball moves together
            if (GameController.state == State.Prepare && GameController.hitType == myHitType)
            {
                PrepareStartBall();
            }
        }
        //hit D to move forward
        else if (Input.GetKey(KeyCode.D))
        {
            Move(Vector3.right, moveSpeed);
            
            //the ball moves together
            if (GameController.state == State.Prepare && GameController.hitType == myHitType)
            {
                PrepareStartBall();
            }
        }
        else
        {
            StopWalk(); //if no movement, stand still
        }

        
    }
}
