using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    GameInputActionAsset myInputActionAsset;

    InputAction fireAction;
    InputAction moveAction;

    private void Awake()
    {
        myInputActionAsset = new GameInputActionAsset();
    }

    void Start()
    {
        fireAction = myInputActionAsset.FindAction("Fire");
        moveAction = myInputActionAsset.FindAction("Move");

        fireAction.Enable();
        moveAction.Enable();
    }

    void Update()
    {
        if (fireAction.WasPressedThisFrame())
        {
            Debug.Log("Fired");
        }

        if (moveAction.IsPressed())
        {
            Vector2 moveDir = moveAction.ReadValue<Vector2>();

            if (moveDir.x > 0)
            {
                Debug.Log("RIGHT");
            }
        }
    }
}
