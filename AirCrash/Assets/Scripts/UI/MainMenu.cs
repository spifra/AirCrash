using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    public void Start()
    {
        AdsManager.Instance.RequestAndShowFirstInterstitialAndBanner();
    }

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
