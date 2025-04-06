using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using TMPro;

public class Playercontroller : MonoBehaviour
{
    private Rigidbody rb;
    private int count;
    
    private float movementX;
    private float movementY;
    private bool isGrounded = true;
    private int jumpCount = 0;
    
    public float speed = 0;
    public TextMeshProUGUI countText;
    public GameObject winTextObject;

    public float jumpForce = 5f;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent <Rigidbody>();
        count = 0; 
        SetCountText();
        winTextObject.SetActive(false);
    }
    
    private void FixedUpdate() 
    {
        Vector3 movement = new Vector3 (movementX, 0.0f, movementY);
        rb.AddForce(movement);
        rb.AddForce(movement * speed);
    }

     void OnTriggerEnter(Collider other) 
   {
        
        other.gameObject.SetActive(false);
        if (other.gameObject.CompareTag("Pick up")) 
       {
             other.gameObject.SetActive(false);
             count = count + 1;
             SetCountText();
            
       }
   }
   void OnMove (InputValue movementValue)
   {
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x; 
        movementY = movementVector.y; 

   }

    void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
{
    if (isGrounded && jumpCount == 0)
    {
        // First jump from ground
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        jumpCount = 1;
        isGrounded = false;
    }
    else if (!isGrounded && jumpCount == 1)
    {
        // Double jump
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        jumpCount = 2;
    }
}
    }

    void OnCollisionEnter(Collision collision)
{
    if (collision.gameObject.CompareTag("Ground"))
    {
        isGrounded = true;
        jumpCount = 0;
    }
}

void OnCollisionExit(Collision collision)
{
    if (collision.gameObject.CompareTag("Ground"))
    {
        isGrounded = false;
    }
}

   void SetCountText() 
   {
    countText.text =  "Count: " + count.ToString();  
    if (count >= 9)
       {
           winTextObject.SetActive(true);
       }  
   }

   

}
