using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class PlayerInput : MonoBehaviour
{
    public static PlayerInput Instance;

    [Header("Input Action Asset")]
    private GameInputActionAsset myInputActionAsset;

    private Animator animator;

    [field: SerializeField] public Vector2 moveInput { get; private set; }
    [field: SerializeField] public bool fireInput { get; private set; }
    [field: SerializeField] public bool runInput { get; private set; }
    [field: SerializeField] public bool leftClickInput { get; private set; }
    [field: SerializeField] public bool rightClickInput { get; private set; }
    [field: SerializeField] public bool saveInput { get; private set; }
    [field: SerializeField] public bool loadInput { get; private set; }

    [field: SerializeField] public bool isInventoryClosed { get; private set; }

    private InputAction fireAction;
    private InputAction moveAction;
    private InputAction runAction;
    private InputAction leftClickAction;
    private InputAction rightClickAction;
    private InputAction saveAction;
    private InputAction loadAction;

    //inventory
    private InputAction inventoryOpenAndClose;
    public static Action<bool> onOpenAndCloseInventory;

    [field: SerializeField] public Vector3 lastMouseScreenPosition { get; private set; } = new Vector3();
    [field: SerializeField] public Vector3 lastMouseWorldPosition { get; private set; } = new Vector3();

    private Vector2 lastDirection = new Vector2();

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

        animator = GetComponent<Animator>();

        myInputActionAsset = new GameInputActionAsset();

        fireAction = myInputActionAsset.Player.Fire;
        moveAction = myInputActionAsset.Player.Move;
        runAction = myInputActionAsset.Player.Run;
        leftClickAction = myInputActionAsset.Player.LeftClick;
        rightClickAction = myInputActionAsset.Player.RightClick;
        saveAction = myInputActionAsset.Player.Save;
        loadAction = myInputActionAsset.Player.Load;

        inventoryOpenAndClose = myInputActionAsset.UI.OpenAndCloseInventory;

        RegisterInputActions();
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
        inventoryOpenAndClose.Enable();
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
        inventoryOpenAndClose.Disable();
    }

    void Update()
    {
        AnimationStuff();

        if (fireAction.WasPerformedThisFrame())
        {
            fireInput = true;
        }
        else
        {
            fireInput = false;
        }

        //other script
        if (fireInput)
        {
            Debug.Log("Firing");
        }

        if (leftClickAction.WasPerformedThisFrame())
        {
            CalculateScreenAndWorldMousePosition();
            leftClickInput = true;
        }
        else
        {
            leftClickInput = false;
        }

        if (rightClickAction.WasPerformedThisFrame())
        {
            CalculateScreenAndWorldMousePosition();
            rightClickInput = true;
        }
        else
        {
            rightClickInput = false;
        }

        if (saveAction.WasPerformedThisFrame())
        {
            saveInput = true;
        }
        else
        {
            saveInput = false;
        }

        if (loadAction.WasPerformedThisFrame())
        {
            loadInput = true;
        }
        else
        {
            loadInput = false;
        }
        
        if(loadInput)
        {
            Debug.Log("Load action button pressed");
        }
        
        if (inventoryOpenAndClose.WasPerformedThisFrame())
        {
            isInventoryClosed = !isInventoryClosed;
            onOpenAndCloseInventory?.Invoke(isInventoryClosed);
        }
    }

    /// <summary>
    /// Adding event lambdas to handlers
    /// </summary>
    void RegisterInputActions()
    {
        //call back context uses:
        //When the move action is performed
        //we want to read value form the context given from the callback
        //we want to set moveInput using the read value of the context

        //move
        moveAction.canceled += context => moveInput = Vector2.zero;

        moveAction.canceled += context => animator.SetFloat("xInput", 0f);
        moveAction.canceled += context => animator.SetFloat("yInput", 0f);

        moveAction.performed += context => lastDirection = moveAction.ReadValue<Vector2>();

        moveAction.performed += context => animator.SetBool("isMoving", true);
        moveAction.canceled += context => animator.SetBool("isMoving", false);
        

        //run
        runAction.performed += context => runInput = true;
        runAction.canceled += context => runInput = false;
    }

    private void CalculateScreenAndWorldMousePosition()
    {
        lastMouseScreenPosition = Mouse.current.position.ReadValue();
        lastMouseWorldPosition = Camera.main.ScreenToWorldPoint(lastMouseScreenPosition);
    }

    private void AnimationStuff()
    {
        //Debug.Log(lastDirection);
        animator.SetFloat("lastXInput", lastDirection.x);
        animator.SetFloat("lastYInput", lastDirection.y);

        moveInput = moveAction.ReadValue<Vector2>();
        animator.SetFloat("xInput", moveInput.x);
        animator.SetFloat("yInput", moveInput.y);

    }

}
