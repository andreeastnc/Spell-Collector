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

    private void Start()
    {
        if(SceneManager.GetActiveScene().name == "Level1")
        {
            userInterface = GameObject.FindWithTag("UI");
            pagesText = userInterface.GetComponentInChildren<Text>();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Page"))
        {
            Destroy(collision.gameObject);
            pages++;
            pagesText.text = "" + pages;
        }
    }
}
