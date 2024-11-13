using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float sprintMultiplier = 2f;
    public float sprintCooldown = 3f;
    private float nextSprint = 0f;
    private Rigidbody rb;

    
    void Start()
    {
        rb = GetComponent<Rigidbody>();

     
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
    }

   
    void FixedUpdate()
    {
        
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        
        rb.AddForce(movement * speed, ForceMode.Acceleration);

        
        if (Input.GetKey(KeyCode.LeftShift) && Time.time > nextSprint)
        {
            rb.AddForce(movement * speed * sprintMultiplier, ForceMode.Acceleration);
            nextSprint = Time.time + sprintCooldown;
        }
    }
}

