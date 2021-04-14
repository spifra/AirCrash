using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{

    private void Awake()
    {
        
    }

    private void Start()
    {
        AdsManager.instance.RequestAndShowBanner();
    }
 
    public void OnPlay()
    {
        SceneManager.LoadScene("Gameplay");
    }

    public void OnHighscores()
    {
        Leaderboard.instance.ShowLeaderboard();
    }
}
