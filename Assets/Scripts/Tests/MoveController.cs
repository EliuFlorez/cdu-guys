using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveController : MonoBehaviour
{
    [Header("Horizontal Movement")]
    public float speedMovement = 15f;

    [Header("Physics")]
    public float jumpForce = 7f;
    public float fallMultiplier = 2.5f;
    public float lowMultiplier = 2f;

    [Header("Components")]
    public LayerMask groundLayer;
    public VariableJoystick joystick;

    [Header("Print Console")]
    public bool debug;

    // References
    private Rigidbody _rigidbody;
    //private Animator _animator;
    private CapsuleCollider _collider;

    // Movement
    private Vector3 _movement;
    private Vector3 _point;
    private bool _isGrounded;
    private bool _isJumping;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        //_animator = GetComponent<Animator>();
        _collider = GetComponent<CapsuleCollider>();
    }

    // Start is called before the first frame update
    void Start()
    {
        //
    }

    // Update is called once per frame
    void Update()
    {
        // JoyStick
        float horizontal = joystick.Horizontal;
        float vertical = joystick.Vertical;

        // Horizontal / Vertical
        if (horizontal > 0f || horizontal < 0f || vertical > 0f || vertical < 0f)
        {
            // Point Movement
            _point = new Vector3(horizontal + transform.position.x, -1.5f, vertical + transform.position.z);
        }

        // Movement
        _movement = Vector3.forward * vertical + Vector3.right * horizontal;

        // Is Grounded
        _isGrounded = Physics.CheckBox(_collider.bounds.center, _collider.bounds.extents, Quaternion.identity, groundLayer);

        // Is Jumping?
        if (Input.GetButtonDown("Jump"))
        {
            setDebug("_isJumping: " + _isJumping);
            setDebug("_isGrounded: " + _isGrounded);
            if (_isGrounded == true && _isJumping == false)
            {
                _isJumping = true;
            }
        }
    }

    private void FixedUpdate()
    {
        // Rotation
        PlayerRotation();

        // Movement Rigibody
        PlayerMove();

        // Jumping Rigibody
        if (_isJumping == true)
        {
            PlayerJumping();
        }

        // Gravity Rigibody
        PlayerGravity();
    }

    public void ActionJumping()
    {
        if (_isGrounded == true && _isJumping == false)
        {
            _isJumping = true;
        }
    }

    private void PlayerMove()
    {
        _rigidbody.AddForce(_movement * speedMovement * Time.fixedDeltaTime, ForceMode.VelocityChange);
    }

    private void PlayerRotation()
    {
        transform.LookAt(new Vector3(_point.x, 0f, _point.z));
        transform.eulerAngles = new Vector3(0f, transform.eulerAngles.y, 0f);
    }

    private void PlayerJumping()
    {
        _isJumping = false;
        _rigidbody.velocity = new Vector3(_rigidbody.velocity.x, 0f, _rigidbody.velocity.z);
        _rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    private void PlayerGravity()
    {
        if (_rigidbody.velocity.y < 0f)
        {
            _rigidbody.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (_rigidbody.velocity.y > 0f && _isJumping == false)
        {
            _rigidbody.velocity += Vector3.up * Physics.gravity.y * (lowMultiplier - 1) * Time.deltaTime;
        }
    }

    private void LateUpdate()
    {
        //_animator.SetBool("isIdle", _movement == Vector3.zero);
        //_animator.SetBool("isGrounded", _isGrounded);
    }

    private void setDebug(string message)
    {
        if (debug == true)
        {
            Debug.Log(message);
        }
    }
}
