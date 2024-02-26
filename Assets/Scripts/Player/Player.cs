using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour, IDamageable
{
    //variable for amount of diamonds
    public int diamonds;

    //get handle to rigidbody
    //use this for initialization
    private Rigidbody2D _rigid;

    //variable for jumpForce
    [SerializeField]
    private float _jumpForce = 7f;
    private bool _resetJump = false;
    [SerializeField]
    private float _speed = 5.0f;
    private bool _grounded = false;

    //handle to playerAnimation
    private PlayerAnimation _playerAnim;
    private SpriteRenderer _playerSprite;
    private SpriteRenderer _swordArcSprite;

    public int Health { get; set; }

    //Use this for initialization
    // Start is called before the first frame update
    void Start()
    {
        //assign handle of rigidbody
        _rigid = GetComponent<Rigidbody2D>();
        //assign hadle to playerAnimation
        _playerAnim = GetComponent<PlayerAnimation>();
        _playerSprite = GetComponentInChildren<SpriteRenderer>();
        _swordArcSprite = transform.GetChild(1).GetComponent<SpriteRenderer>();
        Health = 4;
    }

    // Update is called once per frame
    void Update()
    {  
        Movement();

        //if left click && IsGrounded
        //Attack
        //Input.GetMouseButtonDown(0) 
        if (Input.GetMouseButtonDown(0) && IsGrounded() == true)
        {
            _playerAnim.Attack();
        }
    }

    void Movement()
    {
        
        //horizantal input for left/right
        float move = Input.GetAxisRaw("Horizontal");
        _grounded = IsGrounded();

        //if move is greater than 0
        //facing right
        //else if move < 0 
        //facing left
        if (move > 0)
        {
            Flip(true);
        }
        else if (move < 0)
        {
            Flip(false);
        }
        //Input.GetKeyDown(KeyCode.Space) 
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded() == true)
        {
            Debug.Log("Jump!");
            _rigid.velocity = new Vector2(_rigid.velocity.x, _jumpForce);
            StartCoroutine(ResetJumpRoutine());
            //tell Animator to Jump
            _playerAnim.Jump(true);
        }

        _rigid.velocity = new Vector2(move * _speed, _rigid.velocity.y);

        _playerAnim.Move(move);
    }
    bool IsGrounded()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.down, 1f, 1 << 8);
        Debug.DrawRay(transform.position, Vector2.down, Color.green);

        if(hitInfo.collider != null)
        {   
            if(_resetJump == false)
            {
                // set animator bool to false
                Debug.Log("Grounded");
                _playerAnim.Jump(false);
                return true;
            }
        }

        return false;
    }

    void Flip(bool faceRight)
    {  
        if(faceRight == true)
        {
            _playerSprite.flipX = false;
            _swordArcSprite.flipX = false;
            _swordArcSprite.flipY = false;

            Vector3 newPos = _swordArcSprite.transform.localPosition;
            newPos.x = 1.01f;
            _swordArcSprite.transform.localPosition = newPos;
        }
        else if (faceRight == false)
        {
            _playerSprite.flipX = true;
            _swordArcSprite.flipX = true;
            _swordArcSprite.flipY = true;

            Vector3 newPos = _swordArcSprite.transform.localPosition;
            newPos.x = -1.01f;
            _swordArcSprite.transform.localPosition = newPos;
        }
        
    }

    IEnumerator ResetJumpRoutine()
    {
        _resetJump = true;
        yield return new WaitForSeconds(0.1f);
        _resetJump = false;
    }

    public void Damage()
    {
        if(Health < 1)
        {
            return;
        }

        Debug.Log("Player::Damage()");
        Health--;
        //update UI display
        UIManager.Instance.UpdateLives(Health);

        //check for dead
        //play death animation
        if(Health < 1)
        {
            _playerAnim.Death();
        }

    }

    public void AddGems(int amount)
    {
        diamonds += amount;
        UIManager.Instance.UpdateGemCount(diamonds); 
    }
}
