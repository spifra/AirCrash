using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    private void Start()
    {
        AdsManager.Instance.RequestAndShowBanner();
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
