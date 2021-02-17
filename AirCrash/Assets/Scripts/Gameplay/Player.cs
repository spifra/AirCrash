using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float jumpForce;

    [HideInInspector]
    public bool isStarted;
    [HideInInspector]
    public bool isPaused;

    private Rigidbody2D rigidBody;
    private Animator anim;

    private UnityEvent OnDeath = new UnityEvent();

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
        OnDeath.AddListener(LevelManager.Instance.OnPlayerDeath);
        isStarted = false;
        isPaused = false;

    }

    private void FixedUpdate()
    {
        if (transform.position.y <= -5f || transform.position.y >= 5f)
        {
            DeathAnimation();
        }
    }

    /// <summary>
    /// If the touch is not over UI and the game is not paused
    /// </summary>
    public void OnJump(InputValue value)
    {

        if (!EventSystem.current.IsPointerOverGameObject() && !isPaused && isStarted)
        {
            rigidBody.velocity = Vector2.up * jumpForce;
        }

        if (!isStarted)
        {
            isStarted = true;
            rigidBody.constraints = RigidbodyConstraints2D.None;
            rigidBody.freezeRotation = true;
            rigidBody.velocity = Vector2.up;
        }
        if (isPaused)
        {
            isPaused = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Obstacle")
        {
            DeathAnimation();
        }
    }

    //Start death animation
    private void DeathAnimation()
    {
        isPaused = true;
        anim.SetTrigger("Death");
    }

    //Called by death animation
    private void Death()
    {
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        OnDeath.Invoke();
    }

}
