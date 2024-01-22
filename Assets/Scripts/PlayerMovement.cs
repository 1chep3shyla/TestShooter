using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    private Rigidbody rb;
    private PlayerAnimatorController animatorController;
    private PlayerAttack playerAttack;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animatorController = GetComponent<PlayerAnimatorController>();
        playerAttack = GetComponent<PlayerAttack>();
    }

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        if (horizontalInput != 0f || verticalInput != 0f )
        {
            if(!playerAttack.isShooting)
            {
                Moving(horizontalInput, verticalInput);
                animatorController.SetWalkingAnimation(true);
            }
        }
        else
        {
            animatorController.SetWalkingAnimation(false);
        }
    }

    void Moving(float horizontalInput, float verticalInput) 
    {
        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput);
        movement.Normalize(); 
        RotatePlayer(movement);
        
        rb.velocity = movement * speed;
    }

    void RotatePlayer(Vector3 movement)
    {
        if (movement != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(movement, Vector3.up);
            transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, Time.deltaTime * 10f);
        }
    }
}