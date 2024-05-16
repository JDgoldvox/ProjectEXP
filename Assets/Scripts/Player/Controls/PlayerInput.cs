using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    public static PlayerInput Instance;

    [Header("Input Action Asset")]
    private GameInputActionAsset myInputActionAsset;

    [field: SerializeField] public Vector2 moveInput { get; private set; }
    [field: SerializeField] public bool fireInput { get; private set; }
    [field: SerializeField] public bool runInput { get; private set; }

    private InputAction fireAction;
    private InputAction moveAction;
    private InputAction runAction;

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

        RegisterInputActions();
    }

    private void OnEnable()
    {
        fireAction.Enable();
        moveAction.Enable();
        runAction.Enable();
        
    }

    private void OnDisable()
    {
        fireAction.Disable();
        moveAction.Disable();
        runAction.Disable();
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
}
