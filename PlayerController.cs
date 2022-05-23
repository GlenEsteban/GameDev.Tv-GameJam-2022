using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float runSpeed = .2f;
    [SerializeField] float turnSmoothTime = 0.1f;
    [SerializeField] float jumpSpeed = 5f;
    CharacterManager characterManager;
    Vector2 moveInput;
    Vector3 playerVelocity;
    Rigidbody rb;
    float turnSmoothVelocity;
    [SerializeField] Boolean isCurrentCharacter = false;
    Boolean isFollowing = false;
    Boolean isGuarding = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        characterManager = FindObjectOfType<CharacterManager>();
    }

    // Update is called once per frame
    void Update()
    {
        Run();
    }

    private void Run()
    {
        if (isGuarding) {return;}

        // Moves player based on input
            playerVelocity = new Vector3(moveInput.x, 0f, moveInput.y).normalized;
            rb.velocity += playerVelocity * runSpeed;

        // Face direction of movement with smooth damping only if player is moving
        if (playerVelocity.magnitude >= Mathf.Epsilon)
        {
            float targetAngle = Mathf.Atan2(playerVelocity.x, playerVelocity.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
        }
    }

    void OnMove(InputValue value) 
    {
        // Updates my moveInput member variable
        moveInput = value.Get<Vector2>();
    }

    void OnJump(InputValue value)
    {
        if (value.isPressed)
        {
            rb.velocity += new Vector3(rb.velocity.x, jumpSpeed, rb.velocity.z);
        }
    }

    void OnGuard()
    {
        isFollowing = false;
        isGuarding = true;
    }

    void OnFollow()
    {
        isGuarding = false;
        isFollowing = true;
    }

    void OnSwitchRight(InputValue value)
    {
        if (value.isPressed)
        {
            characterManager.SwitchRight();
        }
    }

    void OnSwitchLeft(InputValue value)
    {
        if (value.isPressed)
        {
            characterManager.SwitchLeft();
        }
    }

    public void SetToCurrentCharacter(Boolean value)
    { 
        isCurrentCharacter = value;
    }

    public void DeactivateControls()
    {
        GetComponent<PlayerInput>().DeactivateInput();
    }
        public void ActivateControls()
    {
        GetComponent<PlayerInput>().ActivateInput();
    }
}
