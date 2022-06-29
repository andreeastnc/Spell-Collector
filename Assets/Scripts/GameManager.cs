using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField]
    private GameObject[] characters;

    private int _charIndex;

    public int CharIndex
    {
        get { return _charIndex; }
        set { _charIndex = value; }
    }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        Time.timeScale = 1f;
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    }

    void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        string path = Application.persistentDataPath + "/player.save";
        if (File.Exists(path))
        {
            PlayerData data = SaveSystem.LoadPlayer();
            CharIndex = data.charIndex;
        }
        if (scene.name == "Level1" || scene.name == "Level2v1" || scene.name == "Level2v2" || scene.name == "Level2v3")
        {
            Instantiate(characters[CharIndex]);
        }
    }
}
