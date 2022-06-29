using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sprite;

    private float dirX = 0f;
    [SerializeField] private float speed = 7f;
    [SerializeField] private float jumpForce = 14f;

    private string TRAP_TAG = "Trap";
    private string MONSTER_TAG = "Monster";
    private string DOUBLEJUMP_TAG = "DoubleJump";
    private string ENDGAME_TAG = "EndGame";
    private string LEVEL2_TAG = "Level2";
    private string GILTAZEL_TAG = "Giltazel";
    private string LIFE_TAG = "LifeRefill";
    private string IMUNITY_TAG = "Imunity";

    [SerializeField] private GameObject[] lives; //vector containing UI heart images
    private int noLives = 3; //the player has 3 lives by default

    private float time = 0f;
    private float imunityTime = 0f;

    private bool canDoubleJump = false;

    public static bool option = false;
    private bool spoke = false;
    public int totalNoOfPages = 16;
    public int myPages = 0;
    private int jumps = 0;

    public Projectile projectile;
    public GameObject firePoint;

    private float attackCooldown = 0.25f;
    private float cooldownTimer = Mathf.Infinity;

    private GameObject userInterface;

    [SerializeField] AudioSource walkSound;
    [SerializeField] AudioSource jumpSound;
    [SerializeField] AudioSource attackSound;
    [SerializeField] AudioSource hitSound;
    [SerializeField] AudioSource dieSound;
    [SerializeField] AudioSource collectSound;

    public int level = 1;
    bool gotLevel = false;

    GameObject dialogue;
    GameObject dialogue2;
    private bool imunity = false;
    //public int charIndex;

    public bool froze = false;

    public int charIndex; 
    [SerializeField] private Text pagesText;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        //audioSource = GetComponent<AudioSource>();

        userInterface = GameObject.FindWithTag("UI");
        if(SceneManager.GetActiveScene().name == "Level1")
        {
            dialogue = GameObject.FindWithTag("Dialogue");
            dialogue.SetActive(false);
            dialogue2 = GameObject.FindWithTag("Dialogue2");
            dialogue2.SetActive(false);
        }

        lives[0] = userInterface.transform.GetChild(0).gameObject;
        lives[1] = userInterface.transform.GetChild(1).gameObject;
        lives[2] = userInterface.transform.GetChild(2).gameObject;

        firePoint = GameObject.Find("Fire Point");

        /*GameManager game = new GameManager();
        charIndex = game.CharIndex;
        Debug.Log(charIndex);*/
        charIndex = GameManager.instance.CharIndex;
    }

    // Update is called once per frame
    void Update()
    {
        if (!froze)
        {
            Movement();
            AnimationState();
            PlayerJump();

            if (canDoubleJump)
                MaxJumps();

            Attack();
            cooldownTimer += Time.deltaTime;

            if (!gotLevel)
            {
                GetLevel();
            }
            if (time < 0.5f) time += Time.deltaTime;

            UpdateImunity();
        }
    }

    public void GetLevel()
    {
        if (SceneManager.GetActiveScene().name == "Level1")
        {
            level = 1;
            gotLevel = true;
            SaveSystem.SavePlayer(this);
        }
        if (SceneManager.GetActiveScene().name == "Level2v1")
        {
            level = 2;
            gotLevel = true;
            SaveSystem.SavePlayer(this);
        }
        if (SceneManager.GetActiveScene().name == "Level2v2")
        {
            level = 3;
            gotLevel= true;
            SaveSystem.SavePlayer(this);
        }
        else if (SceneManager.GetActiveScene().name == "Level2v3")
        {
            level = 4;
            gotLevel = true;
            SaveSystem.SavePlayer(this);
        }
    }

    public void LoadPlayer()
    {
        PlayerData data = SaveSystem.LoadPlayer();

        level = data.level;

        if(level == 1)
        {
            SceneManager.LoadScene("Level1");
        }
        if(level == 2)
        {
            SceneManager.LoadScene("Level2v1");
        }
        if (level == 3)
        {
            SceneManager.LoadScene("Level2v2");
        }
        else if (level == 4)
        {
            SceneManager.LoadScene("Level2v3");
        }
    }

    private void Movement()
    {
        dirX = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(dirX * speed, rb.velocity.y);
        if(rb.velocity.x != 0 && rb.velocity.y < 0.1)
        {
            if(!walkSound.isPlaying)
                walkSound.Play();
        }
        else
        {
            walkSound.Stop();
        }
    }

    private void MaxJumps()
    {
        if (Mathf.Abs(rb.velocity.y) < 0.1f)
            jumps = 2;
        else return;
    }

    private void PlayerJump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if (!canDoubleJump)
            {
                if(Mathf.Abs(rb.velocity.y) < 0.1f) 
                {
                    rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                    jumpSound.Play();
                }
            }
            if (canDoubleJump)
            {
                if(jumps > 0)
                {
                    rb.velocity = new Vector2(rb.velocity.x, jumpForce + 1);
                    jumps--;
                    jumpSound.Play();
                }
                if(jumps == 0)
                {
                    return;
                }
            }
        }
    }

    private void Attack()
    {
        if (Input.GetKey(KeyCode.Z) && CanAttack() && cooldownTimer > attackCooldown)
        {
            GameObject projObj = Instantiate(projectile.gameObject, firePoint.transform.position, transform.rotation);
            projObj.GetComponent<Projectile>().dir = transform.localScale.x;
            cooldownTimer = 0;
            attackSound.Play();
        }
    }

    private void AnimationState()
    {
        if (dirX == 0f)
        {
            anim.SetBool("running", false);
        }
        else if (dirX > 0f)
        {
            anim.SetBool("running", true);
            //sprite.flipX = false;
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
        else
        {
            anim.SetBool("running", true);
            //sprite.flipX = true;
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(TRAP_TAG))
        {
         
            Hit();
            if (noLives == 0)
            {
                Die();
            }
        }
        if (collision.gameObject.CompareTag(MONSTER_TAG))
        {
            Hit();
            if (noLives == 0)
            {
                Die();
            }
        }
        if (collision.gameObject.CompareTag(GILTAZEL_TAG) && SceneManager.GetActiveScene().name == "Level2v2")
        {
            Die();
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(TRAP_TAG) || collision.gameObject.CompareTag(MONSTER_TAG))
            HitWithTimer();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(DOUBLEJUMP_TAG))
        {
            Destroy(collision.gameObject);
            canDoubleJump = true;
            collectSound.Play();
        }
        if (collision.gameObject.CompareTag(LIFE_TAG))
        {
            Destroy(collision.gameObject);
            AddHealth();
            collectSound.Play();
        }
        if (collision.gameObject.CompareTag(IMUNITY_TAG))
        {
            Destroy(collision.gameObject);
            StartImunity();
            collectSound.Play();
        }
        if (collision.gameObject.CompareTag(ENDGAME_TAG))
        {
            if (SceneManager.GetActiveScene().name == "Level2v1")
                SceneManager.LoadScene("BadEndingScreen");
            else if (SceneManager.GetActiveScene().name == "Level2v2")
                SceneManager.LoadScene("GoodEndingScreen");
            else if (SceneManager.GetActiveScene().name == "Level2v3")
            {
                myPages = GetComponent<ItemCollector>().pages;
                if (myPages >= 12)
                {
                     SceneManager.LoadScene("GoodEndingScreen");
                }
                else
                {
                    SceneManager.LoadScene("BadEndingScreen");
                }
            }
            /*SceneManager.LoadScene("EndScreen");
            string path = Application.persistentDataPath + "/player.save";
            if (File.Exists(path))
            {
                File.Delete(path);
            }*/
        }
        if (collision.gameObject.CompareTag("DialogueGiltazel"))
        {
            spoke = true;
            myPages = GetComponent<ItemCollector>().pages;
            if (myPages > (totalNoOfPages - myPages))
            {
                dialogue.SetActive(true);
            }
            else
            {
                dialogue2.SetActive(true);
                option = false;
            }
        }
        if (collision.gameObject.CompareTag(LEVEL2_TAG) && spoke)
        {
            if (option)
            {
                SceneManager.LoadScene("Level2v1");
            }
            else if (!option && myPages > (totalNoOfPages - myPages))
            {
                SceneManager.LoadScene("Level2v2");
            }
            else
            {
                SceneManager.LoadScene("Level2v3");
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("DialogueGiltazel"))
        {
            dialogue.SetActive(false);
            dialogue2.SetActive(false);
        }
    }

    private void Die()
    {
        rb.bodyType = RigidbodyType2D.Static;
        anim.SetTrigger("death");
        dieSound.Play();
    }

    public void Hit()
    {
        if (!imunity)
        {
            anim.SetTrigger("hit");
            lives[noLives - 1].SetActive(false);
            noLives--;
            time = 0;
            hitSound.Play();
            if (noLives == 0)
            {
                Die();
            }
        }
    }

    public void HitWithTimer()
    {
        if (!imunity)
        {
            time += Time.deltaTime;
            if (time >= 0.8f)
            {
                anim.SetTrigger("hit");
                lives[noLives - 1].SetActive(false);
                noLives--;
                time = 0f;
                hitSound.Play();
            }
            if (noLives == 0)
            {
                Die();
            }
        }
    }

    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public bool CanAttack()
    {
        return Mathf.Abs(rb.velocity.y) < 0.1f && SceneManager.GetActiveScene().name == "Level2v1";
    }

    public int getLives()
    {
        return noLives;
    }
    public void setLives(int no)
    {
        if(no <= -1)
        {
            noLives = 0;
            return;
        }
        noLives = no;
    }
    public void AddHealth()
    {
        lives[noLives].SetActive(true);
        if(noLives < 3)
            noLives++;
    }

    public void StartImunity()
    {
        imunity = true;
        imunityTime = 0;
        sprite.color = Color.green;
    }

    private void UpdateImunity()
    {
        if (imunity)
        {
            imunityTime += Time.deltaTime;
            if (imunityTime >= 15)
            {
                imunity = false;
                sprite.color = Color.white;
            }
        }
    }
}
