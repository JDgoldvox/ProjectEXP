using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerUI : MonoBehaviour
{
    public static PlayerUI Instance;

    [Header("Input Action Asset")]
    [SerializeField] private InputActionAsset inputActionAsset;

    [Header("Action Map Names")]
    private string uiMap = "UI";

    [Header("Input Actions")]
    private InputAction openAndCloseInventoryAction;

    [Header("Action Name References")]
    private string openAndCloseInventory = "OpenAndCloseInventory";

    //variables
    [field: SerializeField] public bool isInventoryClosed { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
        }

        openAndCloseInventoryAction = inputActionAsset.FindActionMap(uiMap).FindAction(openAndCloseInventory);

        RegisterInputActions();
    }

    void RegisterInputActions()
    {
        //Opening and closing inventory
        openAndCloseInventoryAction.performed += InventoryOpenAndCloseFunction;
    }

    private void OnEnable()
    {
        openAndCloseInventoryAction.Enable();
    }

    private void OnDisable()
    {
        openAndCloseInventoryAction.Disable();
    }
    private void InventoryOpenAndCloseFunction(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            isInventoryClosed = !isInventoryClosed;
            UIEvents.E_OpenAndCloseInventory?.Invoke(isInventoryClosed);
        }
    }
}