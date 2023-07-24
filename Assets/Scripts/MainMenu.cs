using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    void Start()
    {
        Application.targetFrameRate = 60;
    }
    public void GoToGame()
    {
        SceneManager.LoadScene("Game");
    }
}
