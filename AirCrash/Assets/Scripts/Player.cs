using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float jumpForce;
    [SerializeField]
    private float speed;

    private Rigidbody2D rigidBody;


    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    public void OnJump(InputValue value)
    {
        rigidBody.velocity = Vector2.up * jumpForce;
    }
}
