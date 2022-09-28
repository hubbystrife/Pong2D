using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Racket : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animasi;
    public float speed;
    public string axis = "Vertical";

    // Start is called before the first frame update
    void Start()
    {
        animasi = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (axis == "Vertical2" && GameData.instance.isSinglePlayer)
        {
            return;
        }

        float v = Input.GetAxis(axis);
        rb.velocity = new Vector2(0, v) * speed;


        //Batas atas
        if (transform.position.y > 235.32)
        {
            transform.position = new Vector2(transform.position.x, 235.32f);
        }

        //Batas bawah
        if (transform.position.y < 228.27)
        {
            transform.position = new Vector2(transform.position.x, 228.27f);
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "Ball")
        {
            animasi.SetTrigger("Shoot");
        }
        
    }
}
