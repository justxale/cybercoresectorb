using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PersonController : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    public float movementSpeed = 10f;
    public float runningMultiplier = 1.25f;
    public float jumpHeight = 1f;

    private float _horizMovement;
    private bool _isRunning;
    private bool _isJumping;
    
    // Start is called before the first frame update
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        _horizMovement = Input.GetAxisRaw("Horizontal") * movementSpeed * Time.deltaTime;
        _isRunning = Input.GetKey(KeyCode.LeftShift);
    }

    private void FixedUpdate()
    {
        MovePlayer();
        Jump();
        CheckIsOnGround();
    }

    private void MovePlayer()
    {
        if (_isRunning)
        {
            _horizMovement *= runningMultiplier;
        }

        var dist = new Vector2(_horizMovement, _rigidbody.velocity.y);
        _rigidbody.velocity = dist;
    }

    private void Jump()
    {
        
    }

    private void CheckIsOnGround()
    {
        
    }
}
