using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [HideInInspector]
    public float height;
    [HideInInspector]
    public float xPosition;
    [HideInInspector]
    public bool isBottom;

    [SerializeField]
    private float speed;

    private bool isScored;

    private void Start()
    {
        Builder();
    }

    void Update()
    {
        if (LevelManager.Instance.IsPlayerValid())
        {
            transform.position += new Vector3(-1, 0, 0) * speed * Time.deltaTime;

            if (transform.position.x < -10)
            {
                Destroy(gameObject);
            }
            else if (!isScored && transform.position.x < -0.5f)
            {
                AddToScore();
            }
        }
    }

    private void AddToScore()
    {
        isScored = true;
        LevelManager.Instance.score++;
    }

    private void Builder()
    {
        if (isBottom)
        {
            transform.position = new Vector3(xPosition, -Camera.main.orthographicSize);
        }
        else
        {
            transform.position = new Vector3(xPosition, Camera.main.orthographicSize);
            transform.localScale = new Vector3(1, -1, 1);
        }

        GetComponent<SpriteRenderer>().size = new Vector2(1, height);
        GetComponent<BoxCollider2D>().size = new Vector2(1, height);
        GetComponent<BoxCollider2D>().offset = new Vector2(0, height * 0.5f);

    }
}
