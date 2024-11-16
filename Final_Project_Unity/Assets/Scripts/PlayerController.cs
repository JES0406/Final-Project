using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{

    public Rigidbody RB;
    public float moveSpeed = 5.0f;
    public float jumpForce = 5.0f;

    public LayerMask whatIsGround;
    public Transform groundPoint;
    private bool isGrounded;

    private Vector2 moveDirection;
    private bool jumpPressed;

    private PlayerInputHandler inputHandler;

    // Start is called before the first frame update
    void Awake()
    {
        Debug.Log("Awake");
        inputHandler = PlayerInputHandler.instance;
        if (inputHandler == null)
        {
            Debug.LogError("inputHandler is not assigned.");
            return;
        }
        else
        {
            Debug.Log("RB is assigned.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
    }

    private void GroundLogic()
    {
        RaycastHit hit;
        if (Physics.Raycast(groundPoint.position, Vector3.down, out hit, 0.1f, whatIsGround))
        {
            transform.position = new Vector3(transform.position.x, 0.36f, transform.position.z);
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }

    void HandleMovement()
    {
        float speed = moveSpeed;

        Vector3 inputDirection = new Vector3(inputHandler.moveInput.x, 0, inputHandler.moveInput.y);
        inputDirection.Normalize();


        RB.velocity = new Vector3(inputDirection.x * speed, RB.velocity.y, inputDirection.z * speed);
        if (inputDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(inputDirection);
            RB.MoveRotation(targetRotation);
        }


        HandleJump();
    }

    private void HandleJump()
    {
        GroundLogic();

        if (isGrounded && inputHandler.jumpInput)
        {
            Debug.Log("Jumping");
            RB.velocity += new Vector3(0, jumpForce, 0);
        }
    }

}
