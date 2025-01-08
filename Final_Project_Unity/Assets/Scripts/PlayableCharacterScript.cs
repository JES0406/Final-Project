using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerScript_Marcos : MonoBehaviour
{

    [SerializeField] float movementSpeed = 4f;
    [SerializeField] float jumpForce = 5f;
    [SerializeField] bool isGrounded = false;
    [SerializeField] float verticalVelocity = 0.0f;
    [SerializeField] float gravity = 9.86f;
    [SerializeField] FlipperScript spriteFlipper;
    [SerializeField] float facingDirection = -1;
    [SerializeField] float floorHeight = 0.0f;
    [SerializeField] Vector3 initialPosition;

    [SerializeField] ParticleSystem dustParticles;

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

        bool newIsGrounded = transform.position.y <= floorHeight;

        if (isGrounded != newIsGrounded)
        {
            PlayDustParticles();
        }
        isGrounded = newIsGrounded;

        if (!isGrounded)
        {
            verticalVelocity -= gravity * Time.deltaTime;
        }
        else { verticalVelocity = 0.0f; }

        Vector3 inputDirection = new Vector3(inputHandler.moveInput.x, 0f, inputHandler.moveInput.y);
        if (isGrounded && inputHandler.jumpInput)
        {
            verticalVelocity += jumpForce;
            PlayDustParticles();
        }
        transform.position += (movementSpeed*inputDirection
            + Vector3.up*verticalVelocity) * Time.deltaTime;


        if (transform.position.y <= floorHeight)
        {
            transform.position = new Vector3(transform.position.x, floorHeight, transform.position.z);
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

    void PlayDustParticles()
    {
        Debug.Log("Particles!");
        dustParticles.Stop();
        dustParticles.Play();
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
