using GoogleMobileAds.Api;
using System;
using UnityEngine;

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

        GameObject go = Instantiate(playerPrefab, lastPlayerPosition, Quaternion.identity);
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
    }

    public bool IsPlayerValid()
    {
        if (currentPlayer.isPaused || !currentPlayer.isStarted)
        {
            return false;
        }

        return true;
    }
}
