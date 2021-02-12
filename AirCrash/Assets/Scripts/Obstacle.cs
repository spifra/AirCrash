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

    /// <summary>
    /// If doesn't work try with coroutine
    /// </summary>
    private void Start()
    {
        Builder();
    }

    void Update()
    {
        transform.position += new Vector3(-1, 0, 0) * speed * Time.deltaTime;

        if (transform.position.x < -10)
        {
            Destroy(gameObject);
        }
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
