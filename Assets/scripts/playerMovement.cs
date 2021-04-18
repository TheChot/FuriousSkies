using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    // For movement
    [Header("Player Movement")]
    public float speed;
    public Joystick joystick;
    Rigidbody2D rb;
    Vector2 moveVelocity;
    public float maxY = 4.0f;
    public float minY = -4.0f;
    public float maxX = 10.0f;
    public float minX = -10.0f;

    [Header("GFX")]
    public Animator anim;

    



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if(levelManager.instance.isPaused)
            return;
            
        Vector2 moveInput = new Vector2(joystick.Horizontal, joystick.Vertical);

        moveVelocity = moveInput.normalized * speed;

        anim.SetFloat("vertical", joystick.Vertical);

        if(transform.position.x > maxX)
        {
            transform.position = new Vector3(maxX, transform.position.y, transform.position.z);
        }

        if(transform.position.x < minX)
        {
            transform.position = new Vector3(minX, transform.position.y, transform.position.z);
        }

        if(transform.position.y > maxY)
        {
            transform.position = new Vector3(transform.position.x, maxY, transform.position.z);
        }

        if(transform.position.y < minY)
        {
            transform.position = new Vector3(transform.position.x, minY, transform.position.z);
        }
       

    }

    private void FixedUpdate() {
        
        rb.MovePosition(rb.position + moveVelocity * Time.fixedDeltaTime);   
        // Vector3 clampedPos = rb.position;
        // float xPos = Mathf.Clamp(clampedPos.x, maxX, minX);
        // float yPos = Mathf.Clamp(clampedPos.y, maxY, minY);

        // transform.position = clampedPos;
            

    }

    

    
}
