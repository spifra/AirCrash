using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class LevelBuilder : MonoBehaviour
{
    [SerializeField]
    private Obstacle obstaclePrefab;

    [SerializeField]
    private GameObject obstacleContainer;

    [SerializeField]
    private int startingObstaclesNumber;

    [SerializeField]
    private float obstaclesGap;

    [SerializeField]
    private float startingObstaclesPosition;

    private float latestXPosition;

    private void Awake()
    {
        latestXPosition = startingObstaclesPosition;

        BuildingObstacles(startingObstaclesNumber);

    }

    /// <summary>
    /// In the start we build the starting obstacles and we start the coroutine to continue to build them
    /// </summary>
    private void Start()
    {
        StartCoroutine(IBuilder());
    }

    //Create 2 obstacles and the gap in between them
    private void CreateGapAndObstacles(float gapPosition, float gapSize, float xPosition)
    {
        Obstacle botObs = Instantiate(obstaclePrefab, obstacleContainer.transform);
        botObs.height = gapPosition - gapSize * 0.5f;
        botObs.xPosition = xPosition;
        botObs.isBottom = true;

        Obstacle TopObs = Instantiate(obstaclePrefab, obstacleContainer.transform);
        TopObs.height = Camera.main.orthographicSize * 2f - gapPosition - gapSize * 0.5f;
        TopObs.xPosition = xPosition;
        TopObs.isBottom = false;

        latestXPosition = xPosition;
    }


    private IEnumerator IBuilder()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            BuildingObstacles(2);
        }
    }


    private void BuildingObstacles(int obstaclesNumber)
    {
        for (int i = 0; i < obstaclesNumber; i++)
        {
            float gap = Random.Range(3.5f, 7.5f);
            CreateGapAndObstacles(gap, 3f, latestXPosition + obstaclesGap);
        }
    }

    public void DestroyObstaclesForRespawn()
    {
        List<Obstacle> obstaclesToDestroy = FindObjectsOfType<Obstacle>().Where(x => x.transform.position.x <= LevelManager.Instance.lastPlayerPosition.x + 2).ToList();

        foreach (Obstacle o in obstaclesToDestroy)
        {
            Destroy(o.gameObject);
        }
    }
}
