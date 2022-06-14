using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneManager : MonoBehaviour
{

    //private GameObject enemy;
    private Rigidbody2D rb;
    private float speed = 6.5f;
    private GameObject cs;
    private float time = 0f;
    private float time2 = 0f;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        //enemy = GameObject.FindWithTag("CutsceneGiltazel");
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        cs = GameObject.FindGameObjectWithTag("Cutscene");
        cs.SetActive(false);
    }

    // Update is called once per frame

    void Update()
    {
        time2 += Time.deltaTime;
        if(time2 > 0.75f)
        {
            rb.velocity = new Vector2(-1 * speed, rb.velocity.y);
            anim.SetBool("running", true);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Book"))
        {
            cs.SetActive(true);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Book"))
        {
            time += Time.deltaTime;
            if (time >= 4f)
            {
                cs.SetActive(false);
                Destroy(gameObject);
                Destroy(collision.gameObject);
            }
        }
    }
}
