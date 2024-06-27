using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UIEvents : MonoBehaviour
{
    [SerializeField] private TabSwitch S_TabSwitch;

    private void OnEnable()
    {
        PlayerInput.onOpenAndCloseInventory += OpenAndCloseInventory;
    }

    private void OnDisable()
    {
        PlayerInput.onOpenAndCloseInventory -= OpenAndCloseInventory;
    }

    private void OpenAndCloseInventory(bool isClosed)
    {
        if (!isClosed)
        {
            S_TabSwitch.CloseCurrentTab();
        }
        else
        {
            S_TabSwitch.SwitchToInventory();
        }

    }
}
