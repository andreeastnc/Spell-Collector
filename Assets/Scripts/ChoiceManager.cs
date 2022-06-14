using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoiceManager : MonoBehaviour
{

    public bool option;
    private GameObject dialogue;

    // Start is called before the first frame update
    void Start()
    {
        dialogue = GameObject.FindWithTag("Dialogue");
    }

    public void OptionTrue()
    {
        Player.option = true;
        dialogue.SetActive(false);
    }

    public void OptionFalse()
    {
        Player.option = false;
        dialogue.SetActive(false);
    }
}
