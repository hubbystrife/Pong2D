using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer sptr;
    public float speed;
    public bool isBounce;
    public bool bonusGoal;
    public bool isLastHit1;
    private int split;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sptr = GetComponent<SpriteRenderer>();
        int random = Random.Range(0,2);
        Debug.Log(random);
        if (random == 0)
        {
            rb.velocity = Vector2.right * speed;
            sptr.flipX= true;
        }
        else 
        {
            rb.velocity = Vector2.left * speed;
            sptr.flipX= false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.x > 205 || transform.position.x < 175 || transform.position.y > 240 || transform.position.y < 223)
        {
            MainMenuManager.instance.SpawnBall();
            Destroy(gameObject);
        }
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        SoundManager.instance.BallBouncesfx();

        if(col.gameObject.tag == "BatasAtas")
        {
            sptr.flipY= false;
        }

        if(col.gameObject.tag == "BatasBawah")
        {
            sptr.flipY= true;
        }

        if(col.gameObject.tag == "Player2" && !isBounce)
        {
            Vector2 dir = new Vector2(1,0).normalized;
            rb.velocity = dir * speed;
            StartCoroutine("DelayBounce");
            sptr.flipX= false;
            
            isLastHit1 = true;
        }

        if(col.gameObject.tag == "Player1" && !isBounce)
        {
            Vector2 dir = new Vector2(-1,0).normalized;
            rb.velocity = dir * speed;
            StartCoroutine("DelayBounce");
            sptr.flipX= true;
            isLastHit1 = false;
        }
        
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        SoundManager.instance.Goalsfx();
        if(col.gameObject.tag == "Goal 1")
        {
            MainMenuManager.instance.p2score++;
            if(bonusGoal)
            {
                MainMenuManager.instance.p2score++;
            }
            MainMenuManager.instance.SpawnBall();
            Destroy(gameObject);
            if (MainMenuManager.instance.goldenGoal)
            {
                MainMenuManager.instance.GameOver();
            }
        }

        if(col.gameObject.tag == "Goal 2")
        {
            MainMenuManager.instance.p1score++;
            if(bonusGoal)
            {
                MainMenuManager.instance.p1score++;
            }
            MainMenuManager.instance.SpawnBall();
            Destroy(gameObject);
            if (MainMenuManager.instance.goldenGoal)
            {
                MainMenuManager.instance.GameOver();
            }
        }
    }

    private IEnumerator DelayBounce()
    {
        isBounce = true;
        yield return new WaitForSeconds(1f);
        isBounce = false;
    }
}
