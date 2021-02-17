using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Core
using Core;

public class PlayerController : MonoBehaviour
{
    [Header("Username")]
    public GameObject username;

    [Header("Physics")]
    public float moveSpeed = 6.0f;
    public float jumpForce = 5f;
    public float gravity = 20.0f;
    public float fallMultiplier = 2.5f;
    public float lowMultiplier = 2f;

    [Header("Components")]
    public AudioClip jumpAudio;
    public LayerMask groundLayer;
    public VariableJoystick joystick;

    [Header("Print Console")]
    public bool debug;

    // Components
    private Rigidbody _rigidbody;
    //private Animator _animator;
    private CapsuleCollider _collider;
    private AudioSource _jumpAudio;
    private GameObject _username;

    // Vector
    private Vector3 _moveDirection = Vector3.zero;
    private Vector3 _moveRotation = Vector3.zero;

    // Boolean
    private bool _isGrounded;
    private bool _isJumping;

    // Float
    private float width = .33f;
    public float height = 1f;

    private void Awake()
    {
        _username = Instantiate(username);
        _rigidbody = GetComponent<Rigidbody>();
        //_animator = GetComponent<Animator>();
        _collider = GetComponent<CapsuleCollider>();
        _jumpAudio = GetComponent<AudioSource>();
        _jumpAudio.clip = jumpAudio;
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

        // Is Grounded
        _isGrounded = Physics.CheckCapsule(transform.position - Vector3.up * height, 
            transform.position + Vector3.up * height, width, groundLayer);

        // We are grounded, so recalculate
        // move direction directly from axes
        _moveDirection = new Vector3(horizontal, 0.0f, vertical).normalized;

        // Jumping
        if (Input.GetButtonDown("Jump") && _isGrounded == true)
        {
            _isJumping = true;
        }

        // Movement Rotation
        _moveRotation = Vector3.forward * _moveDirection.z + Vector3.right * _moveDirection.x;
    }

    private void FixedUpdate()
    {
        // Rigibody - Direction
        if (_moveDirection != Vector3.zero && Game.state == Game.State.play)
        {
            // Rigibody - Movement
            _rigidbody.MovePosition(_rigidbody.position + _moveDirection * moveSpeed * Time.fixedDeltaTime);

            // Rigibody - Rotation
            transform.LookAt(transform.position + _moveRotation);
            transform.eulerAngles = new Vector3(0f, transform.eulerAngles.y, 0f);
        }

        // Rigibody - Jumping
        if (_isGrounded == true && _isJumping == true)
        {
            _isJumping = false;
            _rigidbody.AddForce(Vector3.up * Mathf.Sqrt(jumpForce * -2f * Physics.gravity.y), ForceMode.VelocityChange);
        }

        // Rigibody - Gravety
        if (_rigidbody.velocity.y < 0f)
        {
            _rigidbody.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (_rigidbody.velocity.y > 0f && _isJumping == false)
        {
            _rigidbody.velocity += Vector3.up * Physics.gravity.y * (lowMultiplier - 1) * Time.deltaTime;
        }

        // Player - Username
        _username.transform.position = new Vector3(_rigidbody.position.x, _rigidbody.position.y, _rigidbody.position.z);
    }

    public void ActionButtonA()
    {
        if (_isGrounded == true)
        {
            _isJumping = true;
        }
    }

    private void setDebug(string message)
    {
        if (debug == true)
        {
            Debug.Log(message);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position - Vector3.up * height, width);
        Gizmos.DrawWireSphere(transform.position + Vector3.up * height, width);
        Gizmos.DrawLine((transform.position + Vector3.up * height) + Vector3.right * width,
                        (transform.position - Vector3.up * height) + Vector3.right * width);
        Gizmos.DrawLine((transform.position + Vector3.up * height) - Vector3.right * width,
                        (transform.position - Vector3.up * height) - Vector3.right * width);
        Gizmos.DrawLine((transform.position + Vector3.up * height) + Vector3.forward * width,
                        (transform.position - Vector3.up * height) + Vector3.forward * width);
        Gizmos.DrawLine((transform.position + Vector3.up * height) - Vector3.forward * width,
                        (transform.position - Vector3.up * height) - Vector3.forward * width);
        Gizmos.color = Color.white;
    }
}
