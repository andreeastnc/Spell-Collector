using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    private Rigidbody2D rb;

    private float speed = 12;
    private string PLAYER_TAG = "Player";
    private Player playerLives;

    //private Transform enemy;
    public float dir;

    void Start()
    {
        //enemy = GameObject.FindWithTag("RangedDragon").transform;
        rb = GetComponent<Rigidbody2D>();
        playerLives = GameObject.FindWithTag("Player").GetComponent<Player>();
        Debug.Log(playerLives);
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
        if (collision.gameObject.CompareTag(PLAYER_TAG))
        {
            playerLives.Hit();
            Destroy(gameObject);
        }
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
    }
}