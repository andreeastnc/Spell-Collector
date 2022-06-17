using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Rigidbody2D rb;

    private float speed = 12;
    private string MONSTER_TAG = "Monster";

    //private Transform player;

    public float dir;

    void Start()
    {
        //player = GameObject.FindWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (dir > 0f)
        {
            //transform.position += transform.right * Time.deltaTime * speed;
            rb.velocity = new Vector2(speed, rb.velocity.y);
        }
        else
        {
            //transform.position += -transform.right * Time.deltaTime * speed;
            rb.velocity = new Vector2(-speed, rb.velocity.y);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(MONSTER_TAG))
        {
            Destroy(collision.gameObject);
        }
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
    }
}
