using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneManager : MonoBehaviour
{

    private Player player;
    private Rigidbody2D rb;
    private float speed = 6.5f;
    private GameObject cs;
    private float time = 0f;
    private float time2 = 0f;
    private Animator anim;
    AudioSource audioSource;
    private bool play = true;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        cs = GameObject.FindGameObjectWithTag("Cutscene");
        cs.SetActive(false);
    }

    // Update is called once per frame

    void Update()
    {
        time2 += Time.deltaTime;
        player.froze = true;
        if(time2 > 0.75f)
        {
            rb.velocity = new Vector2(-1 * speed, rb.velocity.y);
            anim.SetBool("running", true);
            if (!audioSource.isPlaying && play)
                audioSource.Play();
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Book"))
        {
            cs.SetActive(true);
            audioSource.Stop();
            play = false;

        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Book"))
        {
            time += Time.deltaTime;
            player.froze = true;
            if (time >= 4f)
            {
                cs.SetActive(false);
                Destroy(gameObject);
                Destroy(collision.gameObject);
                player.froze = false;
            }
            if (Input.GetKey(KeyCode.Q))
            {
                cs.SetActive(false);
                Destroy(gameObject);
                Destroy(collision.gameObject);
                player.froze = false;
            }
        }
    }
}
