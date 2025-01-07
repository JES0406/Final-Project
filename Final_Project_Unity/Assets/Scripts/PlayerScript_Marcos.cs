using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerScript_Marcos : MonoBehaviour
{

    public float movementSpeed = 4f;
    public float jumpForce = 5f;
    public bool isGrounded = false;
    public float verticalVelocity = 0.0f;
    public float gravity = 9.86f;
    public FlipperScript spriteFlipper;
    public float facingDirection = -1;

    private PlayerInputHandler inputHandler;

    // Start is called before the first frame update
    void Start()
    {
        inputHandler = PlayerInputHandler.instance;
    }

    private void Awake()
    {

    }

    void HandleMovement()
    {
        isGrounded = transform.position.y <= 0;
        if (!isGrounded)
        {
            verticalVelocity -= gravity * Time.deltaTime;
        }
        else { verticalVelocity = 0.0f; }

        Vector3 inputDirection = new Vector3(inputHandler.moveInput.x, 0f, inputHandler.moveInput.y);
        if (isGrounded && inputHandler.jumpInput)
        {
            verticalVelocity += jumpForce;
        }
        transform.position += (movementSpeed*inputDirection
            + Vector3.up*verticalVelocity) * Time.deltaTime;


        if (transform.position.y <= 0)
        {
            transform.position = new Vector3(transform.position.x, 0f, transform.position.z);
            isGrounded = true;
        }
        else { isGrounded = false; }

        // flipping the player (hope it works)
        if (inputDirection.x != 0)
        {

            if (inputDirection.x * facingDirection < 0)
            {
                spriteFlipper.Flip();
            }
            facingDirection = inputDirection.x < 0 ? -1 : 1;
        }

        // tilting the sprite when the creature walks
        if (Mathf.Abs(inputDirection.x) + Mathf.Abs(inputDirection.z) > 0)
        {
            spriteFlipper.Tilt();
        }
        else { 
            spriteFlipper.ResetTilt();
        }

    }
    // Update is called once per frame
    void Update()
    {
        HandleMovement();
    }
}
