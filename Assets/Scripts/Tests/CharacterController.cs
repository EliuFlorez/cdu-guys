using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [Header("Username")]
    public GameObject username;

    [Header("Physics")]
    public float speed = 6.0f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;
    public float slopeVelocity = 7f;
    public float slopeForceDown = -10f;

    [Header("Components")]
    public AudioClip jumpAudio;
    public LayerMask groundLayer;
    public VariableJoystick joystick;

    [Header("Print Console")]
    public bool debug;

    // References
    private CharacterController _controller;
    //private Animator _animator;
    private AudioSource _jumpAudio;
    private GameObject _username;

    // Vector
    private Vector3 _moveDirection = Vector3.zero;
    private Vector3 _moveRotation = Vector3.zero;
    private Vector3 _hitNormal;
    //private float _moveAngle;

    private Vector3 _groundPositionNow;
    private Vector3 _groundPositionLast;

    private string _groundNameNow;
    private string _groundNameLast;

    private Quaternion _rotationNow;
    private Quaternion _rotationLast;

    // Boolean
    //private bool _isGrounded;
    //private bool _isSlope;
    //private bool _actionButton;

    private void Awake()
    {
        _username = Instantiate(username);
        _controller = GetComponent<CharacterController>();
        //_animator = GetComponent<Animator>();
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
        /*float horizontal = joystick.Horizontal;
        float vertical = joystick.Vertical;

        // Is Grounded
        if (_controller.isGrounded)
        {
            _isGrounded = true;

            // We are grounded, so recalculate
            // move direction directly from axes
            _moveDirection = new Vector3(horizontal, 0.0f, vertical).normalized;
            _moveDirection = Vector3.ClampMagnitude(_moveDirection, 1f);
            _moveDirection *= speed;

            if (Input.GetButtonDown("Jump") || _actionButton == true)
            {
                _moveDirection.y = jumpSpeed;
            }
        }
        else
        {
            _isGrounded = false;
        }*/

        // Movement Rotation
        _moveRotation = Vector3.forward * _moveDirection.z + Vector3.right * _moveDirection.x;
        /*if (_moveDirection.magnitude > 0)
        {
            _moveAngle = Mathf.Atan2(_moveDirection.x, _moveDirection.z) * Mathf.Rad2Deg;
        }*/
    }

    private void FixedUpdate()
    {
        // Apply gravity. Gravity is multiplied by deltaTime twice (once here, and once below
        // when the moveDirection is multiplied by deltaTime). This is because gravity should be applied
        // as an acceleration (ms^-2)
        /*_moveDirection.y -= gravity * Time.deltaTime;

        // Movement Direccion / Rotation
        _controller.transform.LookAt(_controller.transform.position + _moveRotation);
        //transform.localEulerAngles = new Vector3(0f, _moveAngle, 0f);
        _controller.Move(_moveDirection * Time.deltaTime);

        // Username
        _username.transform.position = new Vector3(_controller.transform.position.x, _controller.transform.position.y, _controller.transform.position.z);

        // Platforms Slope
        SlopeDown();

        // Platforms Move
        MoveGround();*/
    }

    private void LateUpdate()
    {
        //_animator.SetBool("isIdle", _movement == Vector3.zero);
        //_animator.SetBool("isGrounded", _isGrounded);
        //_actionButton = false;
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        _hitNormal = hit.normal;
    }

    private void MoveGround()
    {
        /*
        if (_isGrounded == true)
        {
            RaycastHit _hit;

            if (Physics.SphereCast(transform.position, _controller.height / 4.2f, -transform.up, out _hit)) 
            {
                GameObject groundIn = _hit.collider.gameObject;
                if (groundIn.CompareTag("Rotation")) 
                {
                    _groundNameNow = groundIn.name;
                    _groundPositionNow = groundIn.transform.position;

                    _rotationNow = groundIn.transform.rotation;

                    if (_groundNameNow == _groundNameLast && _groundPositionNow != _groundPositionLast)
                    {
                        //transform.position += _groundPositionNow - _groundPositionLast;
                    }

                    if (_groundNameNow == _groundNameLast && _rotationNow != _rotationLast)
                    {
                        //var _rotationNew = transform.rotation * (_rotationNow.eulerAngles - _rotationLast.eulerAngles);
                        //transform.RotateAround(groundIn.transform.position, Vector3.up, _rotationNew.y);
                    }

                    _groundNameLast = _groundNameNow;
                    _groundPositionNow = _groundPositionLast;
                    _rotationLast = _rotationNow;
                }
            }
        } 
        else if (_isGrounded == false) 
        {
            _groundNameNow = null;
            _groundPositionNow = Vector3.zero;
            _rotationNow = Quaternion.Euler(0f, 0f, 0f);
        }
        */
    }

    public void SlopeDown()
    {
        /*_isSlope = Vector3.Angle(Vector3.up, _hitNormal) >= _controller.slopeLimit;

        if (_isSlope)
        {
            float xDownVelocity = _hitNormal.x * slopeVelocity;
            float zDownVelocity = _hitNormal.z * slopeVelocity;

            //float xDownVelocity = ((1f - _hitNormal.y) * _hitNormal.x) * slopeVelocity;
            //float zDownVelocity = ((1f - _hitNormal.y) * _hitNormal.z) * slopeVelocity;

            _moveDirection.x += xDownVelocity;
            _moveDirection.z += zDownVelocity;
            _moveDirection.y += slopeForceDown;
        }*/
    }

    public void ActionButtonA()
    {
        /*if (_isGrounded == true)
        {
            _actionButton = true;
        }*/
    }

    private void setDebug(string message)
    {
        if (debug == true)
        {
            Debug.Log(message);
        }
    }
}
