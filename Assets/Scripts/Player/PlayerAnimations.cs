using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rb;
    [SerializeField] private AnimationClip walkForwardAnim;
    [SerializeField] private AnimationClip walkLeftAnim;
    [SerializeField] private AnimationClip walkRightAnim;
    [SerializeField] private AnimationClip walkBackwardsAnim;

    [SerializeField] private AnimationClip idleForwardAnim;
    [SerializeField] private AnimationClip idleLeftAnim;
    [SerializeField] private AnimationClip idleRightAnim;
    [SerializeField] private AnimationClip idleBackwardsAnim;

    private const string PLAYER_IDLE_FORWARD = "Player_IdleForward";
    private const string PLAYER_IDLE_BACKWARDS = "Player_IdleBackwards";
    private const string PLAYER_IDLE_LEFT = "Player_IdleLeft";
    private const string PLAYER_IDLE_RIGHT = "Player_IdleRight";

    private string PLAYER_WALK_FORWARD = "Player_IdleForward";
    private const string PLAYER_WALK_BACKWARDS = "Player_IdleBackwards";
    private const string PLAYER_WALK_LEFT = "Player_IdleLeft";
    private const string PLAYER_WALK_RIGHT = "Player_IdleRight";

    private void Start()
    {
        PLAYER_WALK_FORWARD = walkForwardAnim.name;
        rb = GetComponent<Rigidbody2D>();
    }
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (rb.velocity.x > 0) //walk right
        {
            animator.Play(PLAYER_WALK_RIGHT);
        }
        else if (rb.velocity.x < 0) // walk left
        {
            animator.Play(PLAYER_WALK_RIGHT);
        }

        if(rb.velocity.y > 0) //walk down
        {
            animator.Play(PLAYER_WALK_BACKWARDS);
        }
        else if(rb.velocity.y < 0) //walk up
        {
            animator.Play(PLAYER_WALK_FORWARD);
        }
    }
}
