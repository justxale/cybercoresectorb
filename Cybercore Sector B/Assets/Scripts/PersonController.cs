using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer))]
public class PersonController : MonoBehaviour
{
    public float movementSpeed = 10f;
    public float runningMultiplier = 1.25f;
    public float jumpHeight = 1f;
    public Transform groundCheckPoint;
    public float groundCheckRadius = 0.5f;
    public LayerMask groundLayer;
    
    private Rigidbody2D _rigidbody;
    private SpriteRenderer _spriteRenderer;
    private float _horizVel;
    private bool _isRunning;
    private bool _isJumping;
    private bool _isGrounded;

    // Start is called before the first frame update
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    private void Update()
    {
        _isRunning = Input.GetKey(KeyCode.LeftShift);
        MovePlayer();
        Jump();
        CheckIsOnGround();
    }

    private void FixedUpdate()
    {
        _horizVel = Input.GetAxis("Horizontal");
    }

    private void MovePlayer()
    {
        if (_isRunning)
        {
            _horizVel *= runningMultiplier;
        }

        _spriteRenderer.flipX = _horizVel switch
        {
            > 0 => false,
            < 0 => true,
            _ => _spriteRenderer.flipX
        };

        _rigidbody.velocity = new Vector2(_horizVel * movementSpeed, _rigidbody.velocity.y);
    }

    private void Jump()
    {
        if (!Input.GetButtonDown("Jump")) return;
        if (_isGrounded)
        {
            _rigidbody.velocity = Vector2.up * jumpHeight;
        }
    }

    private void CheckIsOnGround()
    {
        _isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, groundCheckRadius, groundLayer);
    }
}
