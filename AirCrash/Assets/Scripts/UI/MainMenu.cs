using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void OnPlay()
    {
        SceneManager.LoadScene("Gameplay");
    }


    //TODO: Highscores table 1)New scene 2)UI
    public void OnHighScore()
    {
        Debug.Log("HighScore");
    }
}
