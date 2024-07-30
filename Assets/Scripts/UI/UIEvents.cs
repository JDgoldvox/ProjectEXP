using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UIEvents : MonoBehaviour
{
    [SerializeField] private TabSwitch S_TabSwitch;

    public static Action<bool> E_OpenAndCloseInventory;

    private void Awake()
    {
        E_OpenAndCloseInventory += OpenAndCloseInventory;
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
