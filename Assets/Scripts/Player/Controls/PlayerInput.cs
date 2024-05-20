using UnityEngine;
using UnityEngine.InputSystem;
using static System.Collections.Specialized.BitVector32;

public class PlayerInput : MonoBehaviour
{
    public static PlayerInput Instance;

    [Header("Input Action Asset")]
    private GameInputActionAsset myInputActionAsset;

    [field: SerializeField] public Vector2 moveInput { get; private set; }
    [field: SerializeField] public bool fireInput { get; private set; }
    [field: SerializeField] public bool runInput { get; private set; }
    [field: SerializeField] public bool leftClickInput { get; private set; }
    [field: SerializeField] public bool rightClickInput { get; private set; }

    private InputAction fireAction;
    private InputAction moveAction;
    private InputAction runAction;
    private InputAction leftClickAction;
    private InputAction rightClickAction;

    [field: SerializeField] public Vector3 lastMouseScreenPosition { get; private set; } = new Vector3();
    [field: SerializeField] public Vector3 lastMouseWorldPosition { get; private set; } = new Vector3();

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        myInputActionAsset = new GameInputActionAsset();

        fireAction = myInputActionAsset.Player.Fire;
        moveAction = myInputActionAsset.Player.Move;
        runAction = myInputActionAsset.Player.Run;
        leftClickAction = myInputActionAsset.Player.LeftClick;
        rightClickAction = myInputActionAsset.Player.RightClick;

        RegisterInputActions();
    }

    private void OnEnable()
    {
        fireAction.Enable();
        moveAction.Enable();
        runAction.Enable();
        leftClickAction.Enable();
        rightClickAction.Enable();
    }

    private void OnDisable()
    {
        fireAction.Disable();
        moveAction.Disable();
        runAction.Disable();
        leftClickAction.Disable();
        rightClickAction.Disable();
    }

    void Update()
    {

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


        //other script
        if (leftClickInput)
        {
            Debug.Log("left clicking");
        }

        //other script
        if (rightClickInput)
        {
            Debug.Log("right clicking");
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

        moveAction.performed += context => moveInput = context.ReadValue<Vector2>();
        moveAction.canceled += context => moveInput = Vector2.zero;

        runAction.performed += context => runInput = true;
        runAction.canceled += context => runInput = false;
    }

    private void CalculateScreenAndWorldMousePosition()
    {
        lastMouseScreenPosition = Mouse.current.position.ReadValue();
        lastMouseWorldPosition = Camera.main.ScreenToWorldPoint(lastMouseScreenPosition);
    }
}
