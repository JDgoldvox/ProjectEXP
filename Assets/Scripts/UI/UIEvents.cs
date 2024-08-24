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
            Debug.Log("Closing");
            S_TabSwitch.CloseCurrentTab();
        }
        else
        {
            Debug.Log("Opening");
            S_TabSwitch.SwitchToInventory();
        }
    }
}
