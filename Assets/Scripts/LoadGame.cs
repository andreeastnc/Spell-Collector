using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class LoadGame : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        string path = Application.persistentDataPath + "/player.save";
        if (File.Exists(path))
        {
            SceneManager.LoadScene("StartMenu2");
        }
        else
        {
            SceneManager.LoadScene("StartMenu1");
        }
    }
}
