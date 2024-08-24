using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using Unity.PlasticSCM.Editor.WebApi;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class PlayerHoldPoint : MonoBehaviour
{
    private enum Direction
    {
        Up,Down,Left,Right
    }

    private Vector2 moveInput;

    private Direction currentDirection = Direction.Right;

    [SerializeField] private Transform holdLeft;
    [SerializeField] private Transform holdRight;
    [SerializeField] private Transform holdUp;
    [SerializeField] private Transform holdDown;

    [SerializeField] private Transform toolHolder; //tool holder

    private void Update()
    {
        UpdateHoldPointPosition();
    }

    private void CalculateLastDirection()
    {
        Vector2 moveInput = PlayerGeneralInput.Instance.moveInput;
        
        if(moveInput == null)
        {
            Debug.LogError("moveInput not found - no direction for tool");
        }

        //update hold point
        if (moveInput != Vector2.zero)
        {
            if (moveInput.x > 0)
            {
                currentDirection = Direction.Right;
            }
            else if (moveInput.x < 0)
            {
                currentDirection = Direction.Left;
            }
            else if (moveInput.y > 0)
            {
                currentDirection = Direction.Up;
            }
            else
            {
                currentDirection = Direction.Down;
            }
        }
    }

    private void ChangeHoldPointPosition()
    {
        CalculateLastDirection();

        if (currentDirection == Direction.Left)
        {
            toolHolder.position = holdLeft.position;
            toolHolder.localScale = new Vector3(-1,1,1);
        }
        else if (currentDirection == Direction.Right)
        {
            toolHolder.position = holdRight.position;
            toolHolder.localScale = new Vector3(1, 1, 1);
        }
        else if (currentDirection == Direction.Up)
        {
            toolHolder.position = holdUp.position;
        }
        else
        {
            toolHolder.position = holdDown.position;
        }
    }
        

    private void UpdateHoldPointPosition()
    {
        ChangeHoldPointPosition();
    }

}
