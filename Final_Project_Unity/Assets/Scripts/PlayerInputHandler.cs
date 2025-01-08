using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    [SerializeField] private InputActionAsset inputActions;
    [SerializeField] private string inputMapName = "game";
    [SerializeField] private string move = "move";
    [SerializeField] private string jump = "jump";
    [SerializeField] private string swapTarget = "swapTarget";  
    [SerializeField] private string shoot = "attack";            

    private InputAction moveAction;
    private InputAction jumpAction;
    private InputAction swapTargetAction;
    private InputAction shootAction;

    public Vector2 moveInput { get; private set; }
    public bool jumpInput { get; private set; }
    public bool swapTargetInput { get; private set; }  
    public bool shootInput { get; private set; }       

    // Singleton
    public static PlayerInputHandler instance { get; private set; }

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found more than one Input Manager in the scene.");
            Destroy(gameObject);
        }
        instance = this;
        DontDestroyOnLoad(gameObject);

        if (inputActions == null)
        {
            Debug.LogError("InputActions is not assigned.");
            return;
        }

        var actionMap = inputActions.FindActionMap(inputMapName);
        if (actionMap == null)
        {
            Debug.LogError($"Action map '{inputMapName}' not found.");
            return;
        }

        moveAction = actionMap.FindAction(move);
        jumpAction = actionMap.FindAction(jump);
        swapTargetAction = actionMap.FindAction(swapTarget);
        shootAction = actionMap.FindAction(shoot);

        if (shootAction == null)
        {
            Debug.LogError("One or more actions not found in the input map.");
            return;
        }

        RegisterInputActions();
        Debug.Log("PlayerInputHandler Awake: " + instance);
    }


    void RegisterInputActions()
    {
        moveAction.performed += context => moveInput = context.ReadValue<Vector2>();
        moveAction.canceled += context => moveInput = Vector2.zero;

        jumpAction.performed += context => jumpInput = true;
        jumpAction.canceled += context => jumpInput = false;

        swapTargetAction.performed += context => swapTargetInput = true;  
        swapTargetAction.canceled += context => swapTargetInput = false;  

        shootAction.performed += context => shootInput = true;            
        shootAction.canceled += context => shootInput = false;            
    }

    private void OnEnable()
    {
        moveAction.Enable();
        jumpAction.Enable();
        swapTargetAction.Enable();  
        shootAction.Enable();       
    }

    private void OnDisable()
    {
        moveAction.Disable();
        jumpAction.Disable();
        swapTargetAction.Disable(); 
        shootAction.Disable();     
    }
}
