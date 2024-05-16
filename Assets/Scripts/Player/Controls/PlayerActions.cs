using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    PlayerInput S_PlayerInput;
    Rigidbody2D rb;

    private float walkSpeed = 1f;
    private float runSpeed = 10f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        S_PlayerInput = PlayerInput.Instance;
    }


    void FixedUpdate()
    {
        Movement();
    }

    void Movement()
    {
        if (S_PlayerInput.runInput) //is running/holding shift
        {
            rb.AddForce(S_PlayerInput.moveInput * runSpeed, ForceMode2D.Impulse);
        }
        else //is walking
        {
            rb.AddForce(S_PlayerInput.moveInput * walkSpeed, ForceMode2D.Impulse);
        }
    }
}
