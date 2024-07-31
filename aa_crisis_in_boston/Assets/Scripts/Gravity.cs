// this script is for the main character animations.
// this is applied on the character prefab so won't need further updates (I think)
/* using System;
using UnityEngine;

public class Gravity : MonoBehaviour
{
    private bool _isGrounded = false;

    void Start()
    {
       
    }

    void Update()
    {
        if (Input.GetKeyDown("s"))
        {
            
        }
    }
    
    
}


*/

using System;
using UnityEngine;

public class Gravity : MonoBehaviour
{
    private float _horizontalInput;
    private float _moveSpeed = 5f;
    private bool _isFacingRight = true;
    private float _jumpPower = 5f;
    private bool _isGrounded = false;
    private Rigidbody2D _rB;
    private Animator _animator;

    // Fields to handle flipping
    private bool _isUpsideDown = false; // Indicates if the player is currently upside down
    public KeyCode flipKey = KeyCode.F; // The key to press for flipping

    void Start()
    {
        _rB = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        _horizontalInput = Input.GetAxis("Horizontal");
        
        FlipSprite();
        

        // Jumping logic
        if (Input.GetButtonDown("Jump") && _isGrounded)
        {
            _rB.velocity = new Vector2(_rB.velocity.x, _jumpPower * (_isUpsideDown ? -1 : 1));
            _isGrounded = false;
            _animator.SetBool("isJumping", !_isGrounded);
        }

        // Check if the flip key is pressed and the player is grounded
        if (Input.GetKeyDown(flipKey) && _isGrounded)
        {
            Flip();
        }

        _animator.SetBool("isWalking", Math.Abs(_horizontalInput) > 0.1f);
    }

    private void FixedUpdate()
    {
        if (!_isUpsideDown)
        {
            // Normal movement
            _rB.velocity = new Vector2(_horizontalInput * _moveSpeed, _rB.velocity.y);
            _animator.SetFloat("xVelocity", Math.Abs(_rB.velocity.x));
            _animator.SetFloat("yVelocity", _rB.velocity.y);
        }
        else
        {
            // Movement when upside down
            Debug.Log("This is for moving upside down");

            // Invert the horizontal input for upside-down movement
            _rB.velocity = new Vector2((_horizontalInput * -1f) * _moveSpeed, _rB.velocity.y);

            // Set the xVelocity to be negative to reflect the direction change
            _animator.SetFloat("xVelocity", Math.Abs(_rB.velocity.x) * -1f);
            _animator.SetFloat("yVelocity", _rB.velocity.y);
        }
    }

    void FlipSprite()
    {
        if (_isFacingRight && _horizontalInput < 0f || !_isFacingRight && _horizontalInput > 0f)
        {
            _isFacingRight = !_isFacingRight;
            Vector3 ls = transform.localScale;
            ls.x *= -1f;
            transform.localScale = ls;
        }
    }
    

    // Flip method to flip the player upside down
    void Flip()
    {
        _isUpsideDown = !_isUpsideDown; // Toggle the upside-down state
        transform.Rotate(0f, 180f, 0f); // Rotate the player 180 degrees on the Y-axis
        Vector3 ls = transform.localScale;
        ls.y *= -1f; // Invert the Y-scale to flip the player
        transform.localScale = ls;
        
        // Reverse the gravity direction
        _rB.gravityScale *= -1;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _isGrounded = true; // Set isGrounded to true when colliding with the ground
        _animator.SetBool("isJumping", false); // Update animator state
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _isGrounded = false; // Set isGrounded to false when leaving the ground
    }
}

