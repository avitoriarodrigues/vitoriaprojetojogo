using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
public class PlayerController : MonoBehaviour
{
   
    private Controle _controle;
    private PlayerInput _playerInput;
    private Camera _mainCamera;
    private Vector2 _moveInput;
    private Rigidbody _rigidbody;
    private bool _isGrounded;
    public float moveMultiplier;
    public float maxVelocity;
    public float rayDistance;
    public LayerMask layerMask;
    public float jumpForce;
    public int coin = 0;
    public int diamante = 0;
    private void OnEnable()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _controle = new Controle();
        _playerInput = GetComponent < PlayerInput>();
        _mainCamera = Camera.main;

        _playerInput.onActionTriggered += OnActionTriggered;

    }

    private void OnDisable()
    {
        _playerInput.onActionTriggered -= OnActionTriggered;
    }

    private void OnActionTriggered(InputAction.CallbackContext obj)
    {
        if (obj.action.name.CompareTo(_controle.Gameplay.Move.name) == 0)
        {
            _moveInput = obj.ReadValue<Vector2>();
        }

        if (obj.action.name.CompareTo(_controle.Gameplay.jump.name) == 0)
        {
            if (obj.performed)jump();
        }
    }

    private void Move()

    {
        Vector3 camForward = _mainCamera.transform.forward;
        Vector3 camRight = _mainCamera.transform.right;

        camForward.y = 0;
        camRight.y = 0;
        _rigidbody.AddForce((_mainCamera.transform.forward * _moveInput.y + 
                            _mainCamera.transform.right * _moveInput.x) *
                            moveMultiplier * Time.deltaTime);
    }

    private void FixedUpdate()
    {
        Move();
        LimitVelocity();
    }

    private void LimitVelocity()
    {
        Vector3 velocity = _rigidbody.velocity;
        if (Mathf.Abs(velocity.x) > maxVelocity) velocity.x = Mathf.Sign(velocity.x) * maxVelocity;
        if (Mathf.Abs(velocity.z) > maxVelocity) velocity.z = Mathf.Sign(velocity.z) * maxVelocity;

        _rigidbody.velocity = velocity;
        
    }

    private void CheckGround()
    {
        RaycastHit collision;

        if (Physics.Raycast(origin: transform.position, direction: Vector3.down, out collision, rayDistance, layerMask))
        {
            _isGrounded = true;
        }
        else
        {
            _isGrounded = false;
        }
    }

    private void jump()
    {
        if (_isGrounded)
        {
            _rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    private void Update()
    {
        CheckGround();
    }

    private void OnDrawGizmos()
    {
        Debug.DrawRay(transform.position, dir: Vector3.down * rayDistance, Color.yellow);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("coin"))
        {
            coin++;
            PlayerObserverManager.CoinsChanged(coin);
            Destroy(other.gameObject);
        }
        if (other.CompareTag("diamante"))
        {
            diamante++;
            PlayerObserverManager.DiamanteChanged(diamante);
            Destroy(other.gameObject);
        }
    }
    
    
    
}

