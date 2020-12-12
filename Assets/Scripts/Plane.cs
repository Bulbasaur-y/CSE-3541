using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plane : MonoBehaviour
{
    //when ball fall
    void OnCollisionEnter(Collision colliderInfo)
    {
        //when ball fall
        if (colliderInfo.gameObject.name == "Ball")
        {
            GameController gameController = GameObject.Find("GameController").GetComponent<GameController>();
            if (GameController.state == State.Hit)
            {
                //check the ball is on whose side
                if (colliderInfo.transform.position.x > 0)
                {
                    gameController.EndOneGame(HitType.Enemy);
                }
                else
                {
                    gameController.EndOneGame(HitType.Player);
                }
            }
        }
    }
}
