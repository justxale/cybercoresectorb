using System;
using System.Collections;
using System.Collections.Generic;
//using Unity.Framework.Constraints;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class OctoBot : MonoBehaviour
{
    public float speed;
    public bool isFlipped = false;
    public bool isRolling = false;
    public float rollCooldown = 2f;
    public float rollVelocity = 3f;

    public ScreenPanel rightPanel;
    public ScreenPanel leftPanel;

    private bool rightRoll = false;
    private bool leftRoll = false;

    private Rigidbody2D rb;
    private Animator playerAnimator;
    private Vector2 moveInput;
    private Vector2 moveVelocity;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();

        StartCoroutine(DoRoll());
    }

    private void Update()
    {
        isRolling = playerAnimator.GetCurrentAnimatorClipInfo(0)[0].clip.name == "OctoBox_roll";
        
        if (rightPanel.isPointed && !isRolling)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            transform.GetChild(0).rotation = Quaternion.Euler(0,0,0);
            transform.GetChild(0).position = new Vector3(transform.position.x + 0,transform.position.y,0);
            foreach (var components in transform.GetComponentsInChildren<SpriteRenderer>())
            {
                components.flipX = false;
                components.flipY = false;
            }
            
            isFlipped = false;
        }
        else if (leftPanel.isPointed && !isRolling)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
            transform.GetChild(0).rotation = Quaternion.Euler(0,0,0);
            transform.GetChild(0).position = new Vector3(transform.position.x + -0.825f,transform.position.y,0);
            foreach (var components in transform.GetComponentsInChildren<SpriteRenderer>())
            {
                components.flipX = true;
                components.flipY = false;
            }

            isFlipped = true;
        }

        GetComponent<SpriteRenderer>().flipX = false;
        GetComponent<SpriteRenderer>().flipY = false;

        if (!isRolling)
        {
            moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            moveVelocity = moveInput.normalized * speed;
        }

        if (isRolling)
        {
            
        }
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveVelocity * Time.fixedDeltaTime);
    }

    private IEnumerator DoRoll()
    {
        while (true)
        {
            yield return new WaitWhile((() => !Input.GetButtonDown("Fire2") || Input.GetAxisRaw("Horizontal") == 0));
            if (Input.GetAxisRaw("Horizontal") == 1)
            {
                rightRoll = true;
                leftRoll = false;
                playerAnimator.SetTrigger("DoRoll");
                var rollMove = new Vector2(rollVelocity, 0);
                moveVelocity = rollMove.normalized * speed;
            }
            else if (Input.GetAxisRaw("Horizontal") == -1)
            {
                rightRoll = false;
                leftRoll = true;
                playerAnimator.SetTrigger("DoRoll");
                var rollMove = new Vector2(-rollVelocity, 0);
                moveVelocity = rollMove.normalized * speed;
                rightRoll = false;
                leftRoll = true;
            }
            yield return new WaitForSeconds(rollCooldown);
        }
            
    }
}
