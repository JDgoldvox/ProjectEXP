using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum OBJECT_TYPE
{
    SEED, TILE, WEAPON
}

public class PlayerActions : MonoBehaviour
{
    public static PlayerActions Instance;
    public static Action E_LeftClick;

    public static Action E_MeleeAttack;

    PlayerGeneralInput S_PlayerInput;
    Rigidbody2D rb;

    private float walkSpeed = 1f;
    private float runSpeed = 2f;

    OBJECT_TYPE CurrentSelectedObjectType = OBJECT_TYPE.WEAPON; //OBJECT_TYPE.WEAPON;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        E_LeftClick += LeftClickAction;
    }

    private void Start()
    {
        S_PlayerInput = PlayerGeneralInput.Instance;
    }

    private void Update()
    {
        
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

    public void LeftClickAction()
    {
        switch(CurrentSelectedObjectType) {
                case OBJECT_TYPE.WEAPON:
                MeleeAttack();
                    break;
                case OBJECT_TYPE.TILE:
                    Plant();
                    break;
        }
    }

    void Plant()
    {
        LevelEditor.Instance.PlaceTile();
    }

    void MeleeAttack()
    {
        E_MeleeAttack.Invoke();
    }
}
