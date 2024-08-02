using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAnimations : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rb;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        WalkAnimation();
    }

    private void WalkAnimation()
    {
        Vector2 moveInput = PlayerGeneralInput.Instance.moveInput;
        bool isMoving = PlayerGeneralInput.Instance.isMoving;

        animator.SetBool("isMoving", isMoving);

        // moveInput != Vector2.zero // to save the last input for idle direction
        if (moveInput != Vector2.zero)
        {
            animator.SetFloat("xInput", moveInput.x);
            animator.SetFloat("yInput", moveInput.y);
        }
    }
}
