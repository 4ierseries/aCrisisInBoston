// this script is for the main character animations.
// this is applied on the character prefab so won't need further updates (I think)
using System;
using Cinemachine.Utility;
using UnityEngine;
using System.Collections; // For IEnumerator and coroutines
using UnityEngine.SceneManagement;


public class PoweredUpMovement : MonoBehaviour 
{
    // MOVEMENT VARIABLES
    private float _horizontalInput;
    private float _moveSpeed = 5f;
    private bool _isFacingRight = true;
    private float _jumpPower = 6f;
    private bool _isGrounded = false;
    
    public GameObject spikes;
    private PolygonCollider2D touch_spikes;
    
    
    public GameObject key1;
    private BoxCollider2D key11;
    
    public GameObject key2;
    private BoxCollider2D key22;
    
    public GameObject key3;
    private BoxCollider2D key33;
    
    public GameObject key4;
    private BoxCollider2D key44;
    
    public GameObject key5;
    private BoxCollider2D key55;
    
    // REFERENCING COMPONENTS OF THE CHARACTER
    private Rigidbody2D _rB;
    private Animator _animator;
    private BoxCollider2D _bC;
    
    // REFERENCING THE POWERUP / BLOXY COLA & ITS COMPONENTS
    public GameObject PowerUp; // this will hold the bloxy cola prefab
    private BoxCollider2D CollideCola;
    
    private bool _isUpsideDown = false; // if the player is currently upside down
    private bool hasPowerUp = false; // if the player has the power up

    private bool canPickup = false; // indicates if the user is near the power up
    
    void Start()
    {
        CollideCola = PowerUp.GetComponent<BoxCollider2D>(); // this the powerup
        _bC = GetComponent<BoxCollider2D>(); // this is the character
        _rB = GetComponent<Rigidbody2D>(); // also the character but for movement
        _animator = GetComponent<Animator>(); // animations handler
        touch_spikes = spikes.GetComponent<PolygonCollider2D>();
        
        key11 = key1.GetComponent<BoxCollider2D>();
        key22 = key2.GetComponent<BoxCollider2D>();
        key33 = key3.GetComponent<BoxCollider2D>();
        key44 = key4.GetComponent<BoxCollider2D>();
        key55 = key5.GetComponent<BoxCollider2D>();
        
    }

    void Update()
    {
        // walking animations setup
        _animator.SetBool("isWalking", Math.Abs(_horizontalInput) > 0.1f);
        
        // this checks if the user can pick up the power up
        if (CollideCola.bounds.Intersects(_bC.bounds)) { canPickup = true; }

        if (touch_spikes.bounds.Intersects(_bC.bounds))
        {
            StartCoroutine(WaitOneSecond()); SceneManager.LoadScene("GameOverScene");
        }

        if ((key11.bounds.Intersects(_bC.bounds)) || (key22.bounds.Intersects(_bC.bounds)))
        {
            key1.SetActive(false);
        }
        
        if ((key22.bounds.Intersects(_bC.bounds)) || (key22.bounds.Intersects(_bC.bounds)))
        {
            key2.SetActive(false);
        }
        
        if ((key33.bounds.Intersects(_bC.bounds)) || (key22.bounds.Intersects(_bC.bounds)))
        {
            key3.SetActive(false);
        }
        
        if ((key44.bounds.Intersects(_bC.bounds)) || (key22.bounds.Intersects(_bC.bounds)))
        {
            key4.SetActive(false);
        }
        
        if ((key55.bounds.Intersects(_bC.bounds)) || (key22.bounds.Intersects(_bC.bounds)))
        {
            key5.SetActive(false);
        }
        
        // movement sprite flipping setup
        _horizontalInput = Input.GetAxis("Horizontal");
        FlipSprite();
        
        // Jumping 
        if (Input.GetButtonDown("Jump") && _isGrounded) { Jump(); }
        
        // allow the user to pick the powerup & indicate they did
        if (canPickup && Input.GetKeyDown(KeyCode.E)) { PoweredUp(); } 
        
        // flips the character if they took their powerup
        if (hasPowerUp && Input.GetKeyDown(KeyCode.F)) { Flip(); } 
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

    void PoweredUp()
    { // indicate that the user has picked up the power up & set it off
        hasPowerUp = true;
        Debug.Log("Picked up! Can now flip");
        PowerUp.SetActive(false);
    }

    void Flip()
    { // flips the user upside down
        if (hasPowerUp == true)
        {
            _isUpsideDown = !_isUpsideDown; // upside-down state
            transform.Rotate(0f, 180f, 0f); // Rotate 180 degrees on the Y-axis
            Vector3 ls = transform.localScale;
            ls.y *= -1f; // invert the y-scale
            transform.localScale = ls;
        
            // reverse the gravity direction
            _rB.gravityScale *= -1;
        }
        else
        {
            Debug.Log("get cola first");
        }
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
    
    protected IEnumerator WaitOneSecond()
    {
        yield return new WaitForSeconds(1);
    }

}

