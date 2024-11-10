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

    private InputAction moveAction;
    private InputAction jumpAction;

    public Vector2 moveInput { get; private set; }
    public bool jumpInput { get; private set; }

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

        moveAction = inputActions.FindActionMap(inputMapName).FindAction(move);
        jumpAction = inputActions.FindActionMap(inputMapName).FindAction(jump);
        RegisterInputActions();
    }

        void RegisterInputActions()
    {
        moveAction.performed += context => moveInput = context.ReadValue<Vector2>(); // => is a lambda expression, it's a shorthand way of writing a function
        moveAction.canceled += context => moveInput = Vector2.zero;

        jumpAction.performed += context => jumpInput = true;
        jumpAction.canceled += context => jumpInput = false;
    }

    private void OnEnable()
    {
        moveAction.Enable();
        jumpAction.Enable();
    }

    private void OnDisable()
    {
        moveAction.Disable();
        jumpAction.Disable();
    }

}
