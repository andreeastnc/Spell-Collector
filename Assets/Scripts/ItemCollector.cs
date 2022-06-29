using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ItemCollector : MonoBehaviour
{
    public int pages = 0;
    [SerializeField] private Text pagesText;
    private GameObject userInterface;
    private Player player;
    [SerializeField] AudioSource collectSound;

    private void Start()
    {
        if(SceneManager.GetActiveScene().name == "Level1" || SceneManager.GetActiveScene().name == "Level2v3")
        {
            userInterface = GameObject.FindWithTag("UI");
            pagesText = userInterface.GetComponentInChildren<Text>();
            player = GetComponent<Player>();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Page"))
        {
            Destroy(collision.gameObject);
            pages++;
            pagesText.text = "" + pages;
            collectSound.Play();
            if (SceneManager.GetActiveScene().name == "Level1")
            {
                if (pages > (player.totalNoOfPages - pages))
                {
                    pagesText.color = Color.green;
                }
            }
            else if (SceneManager.GetActiveScene().name == "Level2v3"){
                if (pages > 11)
                {
                    pagesText.color = Color.green;
                }
            }
        }
    }
}
