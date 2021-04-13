using GoogleMobileAds.Api;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    #region Singleton
    private static LevelManager instance;
    public static LevelManager Instance
    {
        get
        {
            if (instance != null)
            {
                return instance;
            }
            else
            {
                return null;
            }
        }
    }
    #endregion

    [SerializeField]
    private GameObject playerPrefab;

    [SerializeField]
    private inGameMenu UI;

    [SerializeField]
    private LevelBuilder levelBuilder;

    [HideInInspector]
    public Player currentPlayer;

    [HideInInspector]
    public Vector2 lastPlayerPosition;

    [SerializeField]
    private GameObject resurrectionPrefab;

    private GameObject resurrection;

    [HideInInspector]
    public int score;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        SpawnPlayer();
    }

    private void SpawnPlayer()
    {
        GameObject go = Instantiate(playerPrefab, new Vector2(0, 0), Quaternion.identity);
        currentPlayer = go.GetComponent<Player>();
        score = 0;
    }

    /// <summary>
    /// Call the method to destroy the obstacle and instantiate the player
    /// </summary>
    internal void RespawnPlayer(object sender, Reward e)
    {
        levelBuilder.DestroyObstaclesForRespawn();

        ResumeGame();

        StartCoroutine(IRespawnPlayer());
    }

    private IEnumerator IRespawnPlayer()
    {
        resurrection = Instantiate(resurrectionPrefab);
        SoundEffectManager.instance.OnRevival();
        yield return new WaitForSeconds(2f);

        Destroy(resurrection);

        GameObject go = Instantiate(playerPrefab, new Vector2(0, 0), Quaternion.identity);
        currentPlayer = null;
        currentPlayer = go.GetComponent<Player>();
    }

    public void PauseGame()
    {
        if (currentPlayer != null)
            currentPlayer.isPaused = true;
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        if (currentPlayer != null)
            currentPlayer.isPaused = false;
        Time.timeScale = 1;
    }

    public void OnPlayerDeath()
    {
        lastPlayerPosition = currentPlayer.transform.position;
        UI.OnGameOver();
    }

    public bool IsPlayerValid()
    {
        if (currentPlayer != null)
        {
            if (!currentPlayer.isPaused && currentPlayer.isStarted)
            {
                return true;
            }
        }
        return false;
    }

    public void LoadNewPlayer()
    {
        levelBuilder.DestroyObstaclesForRespawn();

        GameObject go = Instantiate(playerPrefab, new Vector2(0, 0), Quaternion.identity);
        currentPlayer = null;
        currentPlayer = go.GetComponent<Player>();
    }

    internal void GoToHome(object sender, EventArgs e)
    {
        ResumeGame();
        if (score != 0)
        {
            Leaderboard.instance.AddScoreToLeaderboard(score);
        }
        SceneManager.LoadScene("Menu");
    }

    internal void Restart(object sender, EventArgs e)
    {
        ResumeGame();
        if (score != 0)
        {
            Leaderboard.instance.AddScoreToLeaderboard(score);
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
