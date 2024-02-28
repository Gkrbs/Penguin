using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// Player Component
// Player GameObject have children playerObj and orientation

public class PlayerMovement : MonoBehaviour
{
    float playerRadius = 0.3f;
    [Header("Movement")]
    [SerializeField] float moveForce        = 10f;
    [SerializeField] float moveSpeed        = 3f;
    [SerializeField] float parachuteSpeed   = 5f;
    [SerializeField] float walkSpeed        = 3f;
    [SerializeField] float fallingSpeed     = 8f;
    [SerializeField] float slidingSpeed     = 8f;
    [SerializeField] float groundDrag       = 5;
    Vector3 moveDirection;

    [Header("Jump")]
    [SerializeField] float jumpForce = 5;
    [SerializeField] float jumpCooldown = 1.0f;
    [SerializeField] float airMultiplier = 0.3f;
    [SerializeField] bool readyToJump = true;

    [Header("Sliding")]
    [SerializeField] bool sliding = false;

    [Header("Parachute")]
    [SerializeField] bool parachute = false;
    [SerializeField] float parachuteDrag = 0.1f;
    [SerializeField] float parachuteGravityResist = 7.5f;
    [SerializeField] float paraCooldown = 2.0f;
    [SerializeField] bool readyToPara = false;

    [Header("Ground Check")]
    [SerializeField] float playerHeight = 0.7f;
    [SerializeField] float checkOffset = 0.1f;
    [SerializeField] LayerMask Ground;
    [SerializeField] LayerMask IceGround;
    [SerializeField] bool iceGrounded = false;
    [SerializeField] bool grounded = true;

    [Header("Components")]
    public Transform orientation;
    InputSystem input;
    Rigidbody rb;
    Animator anim;

    private void Start()
    {
        input = GetComponent<InputSystem>();
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        Ground = LayerMask.GetMask("Ground");
        IceGround = LayerMask.GetMask("IceGround");
    }

    private void Update()
    {
        GroundCheck();
        ParachuteCheck();
        MyInput();
        ControlHandle();
        SpeedControl();
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MyInput()
    {
        // when to jump
        if(input.jump && readyToJump && grounded)
        {
            anim.SetTrigger("Jump");
            readyToJump = false;

            Jump();

            Invoke(nameof(ResetJump), jumpCooldown);
        }
        else
        {
            input.jump = false;
        }
    }
    private void ControlHandle()
    {
        // handle drag
        if (grounded)
        {
            rb.drag = groundDrag;
            moveSpeed = walkSpeed;
            parachute = false;
            readyToPara = false;
        }
        else if (parachute)
        {
            rb.drag = parachuteDrag;
            moveSpeed = parachuteSpeed;
        }
        else
        {
            rb.drag = 0;
            moveSpeed = fallingSpeed;
        }
    }
    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        // limit velocity if needed
        if(flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
        if(parachute && rb.velocity.magnitude > moveSpeed)
        {
            rb.velocity = rb.velocity.normalized * moveSpeed;
        }
    }
    private void MovePlayer()
    {
        // calculate movement direction
        moveDirection = orientation.forward * input.move.y + orientation.right * input.move.x;
        if (moveDirection != Vector3.zero)
            anim.SetBool("Run", true);
        else
            anim.SetBool("Run", false);

        // on ground
        if (grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * moveForce, ForceMode.Force);
        // in air
        else if (parachute)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * moveForce * airMultiplier, ForceMode.Force);
            rb.AddForce(Vector3.up * parachuteGravityResist, ForceMode.Force);
        }
        else if(!grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * airMultiplier, ForceMode.Force);

    }

    private void Jump()
    {
        // reset y velocity
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        
    }
    private void ResetJump()
    {
        readyToJump = true;
        input.jump = false;
        Invoke(nameof(CanParachute), paraCooldown);
    }
    private void CanParachute()
    {
        if (!grounded)
            readyToPara = true;
    }
    private void GroundCheck()
    {
        // ground check
        RaycastHit hit;
        grounded = Physics.SphereCast(transform.position, playerRadius, Vector3.down, out hit, playerHeight * 0.5f + checkOffset - playerRadius * 0.5f, Ground);
        //if (hit.collider.CompareTag("IceGround"))
        //    iceGrounded = true;
        
        anim.SetBool("Ground", grounded);
    }
    void ParachuteCheck()
    {
        // parachute check
        if (!parachute && readyToPara && !grounded && input.jump)
        {
            rb.velocity = Vector3.zero;
            parachute = true;
            readyToPara = false;
            input.jump = false;
        }
        else if (parachute && input.jump)
        {
            parachute = false;
            input.jump = false;
            Invoke(nameof(CanParachute), paraCooldown);
        }
        anim.SetBool("Fly", parachute);
    }
}