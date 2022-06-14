using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void NewGame()
    {
        SceneManager.LoadScene("CharacterSelection");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void MainMenu1()
    {
        SceneManager.LoadScene("StartMenu1");
    }

    public void MainMenu2()
    {
        SceneManager.LoadScene("StartMenu2");
    }

    public void StartGame()
    {
        int selectedCharacter = int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name);
        GameManager.instance.CharIndex = selectedCharacter;
        SceneManager.LoadScene("Level1");
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
