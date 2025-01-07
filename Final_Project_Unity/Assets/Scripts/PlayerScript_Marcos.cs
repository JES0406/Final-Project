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
    public float floorHeight = 0.0f;
    public Vector3 initialPosition;

    private PlayerInputHandler inputHandler;

    // Start is called before the first frame update
    void Start()
    {
        inputHandler = PlayerInputHandler.instance;
        initialPosition = transform.position;
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


        if (transform.position.y <= floorHeight)
        {
<<<<<<< HEAD
            transform.position = new Vector3(transform.position.x, floorHeight, transform.position.z);
=======
            transform.position = new Vector3(transform.position.x, 0f, transform.position.z);
<<<<<<< HEAD
<<<<<<< HEAD
>>>>>>> parent of 95e2849 (dust particles)
=======
>>>>>>> parent of 95e2849 (dust particles)
=======
>>>>>>> parent of 95e2849 (dust particles)
            isGrounded = true;
        }
        else { isGrounded = false; }

<<<<<<< HEAD
<<<<<<< HEAD
=======
        // flipping the player (hope it works)
>>>>>>> parent of 95e2849 (dust particles)
=======
        // flipping the player (hope it works)
>>>>>>> parent of 95e2849 (dust particles)
        if (inputDirection.x != 0)
        {

            if (inputDirection.x * facingDirection < 0)
            {
                spriteFlipper.Flip();
            }
            facingDirection = inputDirection.x < 0 ? -1 : 1;
        }

        // tilting the sprite when the creature walks
<<<<<<< HEAD
<<<<<<< HEAD
=======
        if (Mathf.Abs(inputDirection.x) + Mathf.Abs(inputDirection.z) > 0)
>>>>>>> parent of 95e2849 (dust particles)
=======
        if (Mathf.Abs(inputDirection.x) + Mathf.Abs(inputDirection.z) > 0)
>>>>>>> parent of 95e2849 (dust particles)
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

    public void resetPosition()
    {
        transform.position = initialPosition;
    }
    public void SetInitialPosition(float x, float y, float z)
    {
        initialPosition = new Vector3(x, y, z);
    }
}
