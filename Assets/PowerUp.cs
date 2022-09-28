using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public string NamePowerUp;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Ball")
        {
            SoundManager.instance.PowerUpsfx();
            if (NamePowerUp == "BonusGoal")
            {
                Debug.Log("BonusGoal");
                col.GetComponent<Ball>().bonusGoal=true;
                Destroy(gameObject);
            }

            if (NamePowerUp == "SpeedUp")
            {
                Debug.Log("SpeedUp");
                Ball ball=col.GetComponent<Ball>();
                ball.speed *= 2f;
                
            }

            if (NamePowerUp == "ChangeDirection")
            {
                Debug.Log("ChangeDirection");
                Ball ball=col.GetComponent<Ball>();
                if(ball.isLastHit1)
                {
                    ball.GetComponent<Rigidbody2D>().velocity = new Vector2 (-1, Random.Range(-1,1))* ball.speed;
                }
                else
                {
                    ball.GetComponent<Rigidbody2D>().velocity = new Vector2 (1, Random.Range(-1,1))* ball.speed;
                }
            }
            Destroy(gameObject);
        }
    }
}
