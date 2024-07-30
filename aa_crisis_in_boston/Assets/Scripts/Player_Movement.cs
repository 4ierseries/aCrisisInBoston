using System;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    private float _horizontalInput;
    private float _moveSpeed = 5f;
    private bool _isFacingRight = true;
    private float _jumpPower = 5f;
    private bool _isGrounded = false;
    private Rigidbody2D _rB;
    private Animator _animator;

    void Start()
    {
        _rB = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        _horizontalInput = Input.GetAxis("Horizontal");

        FlipSprite();

        if (Input.GetButtonDown("Jump") && _isGrounded)
        {
            _rB.velocity = new Vector2(_rB.velocity.x, _jumpPower);
            _isGrounded = false;
            _animator.SetBool("isJumping", !_isGrounded);
        }
        
        _animator.SetBool("isWalking", Math.Abs(_horizontalInput) > 0.1f);
    }

    private void FixedUpdate()
    {
        _rB.velocity = new Vector2(_horizontalInput * _moveSpeed, _rB.velocity.y);
        _animator.SetFloat("xVelocity", Math.Abs(_rB.velocity.x));
        _animator.SetFloat("yVelocity", _rB.velocity.y);
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _isGrounded = true;
        _animator.SetBool("isJumping", false);
    }
    
    
    
    
    
}


