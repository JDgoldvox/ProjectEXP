using UnityEngine;
using UnityEngine.InputSystem;
using System;
using UnityEditor.Rendering;

public class PlayerGeneralInput : MonoBehaviour
{
    public static PlayerGeneralInput Instance;

    [Header("Input Action Asset")]
    [SerializeField] private InputActionAsset inputActionAsset;

    private Animator animator;

    [Header("Action Map Name References")]
    private string playerMap = "Player";

    [Header("Input Actions")]
    private InputAction fireAction;
    private InputAction moveAction;
    private InputAction runAction;
    private InputAction leftClickAction;
    private InputAction rightClickAction;
    private InputAction saveAction;
    private InputAction loadAction;
    private InputAction openAndCloseInventoryAction;

    [Header("Action Name References")]
    private string fire = "Fire";
    private string move = "Move";
    private string run = "Run";
    private string leftClick = "LeftClick";
    private string rightClick = "RightClick";
    private string save = "Save";
    private string load = "Load";
    
    //variables
    [field: SerializeField] public Vector2 moveInput { get; private set; }
    [field: SerializeField] public bool fireInput { get; private set; }
    [field: SerializeField] public bool runInput { get; private set; }
    [field: SerializeField] public bool leftClickInput { get; private set; }
    [field: SerializeField] public bool rightClickInput { get; private set; }
    [field: SerializeField] public bool saveInput { get; private set; }
    [field: SerializeField] public bool loadInput { get; private set; }

    [field: SerializeField] public bool isMoving { get; private set; }

    [field: SerializeField] public Vector3 lastMouseScreenPosition { get; private set; } = new Vector3();
    [field: SerializeField] public Vector3 lastMouseWorldPosition { get; private set; } = new Vector3();

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
        }

        isMoving = false;

        fireAction = inputActionAsset.FindActionMap(playerMap).FindAction(fire);
        moveAction = inputActionAsset.FindActionMap(playerMap).FindAction(move);
        runAction = inputActionAsset.FindActionMap(playerMap).FindAction(run);
        leftClickAction = inputActionAsset.FindActionMap(playerMap).FindAction(leftClick);
        rightClickAction = inputActionAsset.FindActionMap(playerMap).FindAction(rightClick);
        saveAction = inputActionAsset.FindActionMap(playerMap).FindAction(save);
        loadAction = inputActionAsset.FindActionMap(playerMap).FindAction(load);

        RegisterInputActions();
    }

    void RegisterInputActions()
    {
        //fire
        fireAction.performed += FireFunction;

        //move
        moveAction.performed += MoveFunction;
        moveAction.canceled += MoveFunction;

        //run
        runAction.performed += context => runInput = true;
        runAction.canceled += context => runInput = false;

        //clicks
        leftClickAction.performed += LeftClickFunction;
        rightClickAction.performed += RightClickFunction;

        //save & load
        saveAction.performed += SaveFunction;
        loadAction.performed += LoadFunction;
    }

    private void OnEnable()
    {
        fireAction.Enable();
        moveAction.Enable();
        runAction.Enable();
        leftClickAction.Enable();
        rightClickAction.Enable();
        saveAction.Enable();
        loadAction.Enable();
    }

    private void OnDisable()
    {
        fireAction.Disable();
        moveAction.Disable();
        runAction.Disable();
        leftClickAction.Disable();
        rightClickAction.Disable();
        saveAction.Disable();
        loadAction.Disable();
    }

    void Update()
    {
        if (loadInput)
        {
            Debug.Log("Load action button pressed");
        }

        if (saveInput)
        {
            Debug.Log("Im saving...");
        }
    }

    private void LeftClickFunction(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            CalculateScreenAndWorldMousePosition();
            PlayerActions.E_LeftClick.Invoke();
        }
        
    }

    private void RightClickFunction(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            LevelEditor.Instance.SwitchTiles();
        }
    }

    private void SaveFunction(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            LevelManager.E_SaveLevel.Invoke();
            Debug.Log("SAVING");
        }
    }

    private void LoadFunction(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            LevelManager.E_LoadLevel.Invoke();
            Debug.Log("LOADING");
        }
    }

    private void FireFunction(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("firing");
        }
    }

    private void MoveFunction(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            moveInput = context.ReadValue<Vector2>();
            isMoving = true;
        }
        else if (context.canceled)
        {
            moveInput = Vector2.zero;
            isMoving = false;
        }
    }

    private void CalculateScreenAndWorldMousePosition()
    {
        lastMouseScreenPosition = Mouse.current.position.ReadValue();
        lastMouseWorldPosition = Camera.main.ScreenToWorldPoint(lastMouseScreenPosition);
    }
}