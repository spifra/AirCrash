using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBuilder : MonoBehaviour
{
    [SerializeField]
    private Obstacle obstaclePrefab;

    [SerializeField]
    private GameObject obstacleContainer;

    [SerializeField]
    private float speed;

    [SerializeField]
    private float gap1;
    [SerializeField]
    private float gap2;

    private float cameraSize;

    private List<Transform> obstacles = new List<Transform>();

    private void Awake()
    {
        cameraSize = Camera.main.orthographicSize;
    }
    private void Start()
    {
        CreateGap(5f, 3f, 0);
    }

    private void CreateGap(float gapPosition, float gapSize, float xPosition)
    {
        Obstacle botObs = Instantiate(obstaclePrefab, obstacleContainer.transform);
        botObs.height = gapPosition - gapSize * 0.5f;
        botObs.xPosition = xPosition;
        botObs.isBottom = true;

        Obstacle TopObs = Instantiate(obstaclePrefab, obstacleContainer.transform);
        TopObs.height = gapPosition - gapSize * 0.5f;
        TopObs.xPosition = xPosition;
        TopObs.isBottom = false;
    }

}
