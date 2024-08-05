// this script is for the main character animations.
// this is applied on the character prefab so won't need further updates (I think)
using System;
using UnityEngine;

public class nopowermove : MonoBehaviour 
{
    // MOVEMENT VARIABLES
    private float _horizontalInput;
    private float _moveSpeed = 5f;
    private bool _isFacingRight = true;
    private float _jumpPower = 5f;
    private bool _isGrounded = false;
    
    // REFERENCING COMPONENTS OF THE CHARACTER
    private Rigidbody2D _rB;
    private Animator _animator;
    private BoxCollider2D _bC;

    private bool _isUpsideDown = false; // if the player is currently upside down

    
    void Start()
    {
        _bC = GetComponent<BoxCollider2D>(); // this is the character
        _rB = GetComponent<Rigidbody2D>(); // also the character but for movement
        _animator = GetComponent<Animator>(); // animations handler
    }

    void Update()
    {
        // walking animations setup
        _animator.SetBool("isWalking", Math.Abs(_horizontalInput) > 0.1f);
        
        // this checks if the user can pick up the power up
        
        // movement sprite flipping setup
        _horizontalInput = Input.GetAxis("Horizontal");
        FlipSprite();
        
        // Jumping 
        if (Input.GetButtonDown("Jump") && _isGrounded) { Jump(); }
        
        // allow the user to pick the powerup & indicate they did
        
        // flips the character if they took their powerup
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
            // Invert the horizontal input for upside-down movement
            _rB.velocity = new Vector2((_horizontalInput * -1f) * _moveSpeed, _rB.velocity.y);

            // Set the xVelocity to be negative to reflect the direction change
            _animator.SetFloat("xVelocity", Math.Abs(_rB.velocity.x) * -1f);
            _animator.SetFloat("yVelocity", _rB.velocity.y);
        }
    }
    
    void FlipSprite()
    { // this will flip the sprite when the user is walking left/right
        if (_isFacingRight && _horizontalInput < 0f || !_isFacingRight && _horizontalInput > 0f)
        {
            _isFacingRight = !_isFacingRight;
            Vector3 ls = transform.localScale;
            ls.x *= -1f;
            transform.localScale = ls;
        }
    }

    void Jump()
    { // jumping logic & animations
        _rB.velocity = new Vector2(_rB.velocity.x, _jumpPower * (_isUpsideDown ? -1 : 1));
        _isGrounded = false;
        _animator.SetBool("isJumping", !_isGrounded);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _isGrounded = true; 
        _animator.SetBool("isJumping", false); 
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _isGrounded = false; 
    }
}

