using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this script is for the racket
public class Beat : MonoBehaviour
{
    private BaseUnit baseUnit;   
    void Start()
    {
        baseUnit = transform.GetComponentInParent<BaseUnit>();
        
    }

    //when the racket touches the ball
    void OnTriggerEnter(Collider colliderInfo)
    {
         
        if (colliderInfo.gameObject == baseUnit.ball.gameObject)
        {
            //if it's not the right racket
            if (GameController.hitType != baseUnit.myHitType) return;
            
            if (GameController.state != State.Hit) return;

            //stop the ball
            baseUnit.ball.velocity = Vector3.zero;
            //give a random force
            float force = Random.Range(baseUnit.minPower, baseUnit.maxPower);
            //if the racket hit or not
            var aniState = baseUnit.aniController["HitBall"];
            if (aniState.time > 0)
            {
                //hit the ball
                baseUnit.AddForceToBall(transform.up, force); 
            }

             
            else
            {
                //let the ball fall without cross the racket
                baseUnit.AddForceToBall(transform.up, 0.1f * force);
            }

             
        }
    }
}
