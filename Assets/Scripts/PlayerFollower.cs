using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollower : MonoBehaviour
{
    private Rigidbody2D rb;
    private Transform player;
    private Vector3 targetPosition;
    private float jumpForce = 14.5f;
    private float speed = 6.5f;
    

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        targetPosition = new Vector3(player.position.x, rb.position.y, 0);
        rb.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
    }

    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            Jump();
    }
}
