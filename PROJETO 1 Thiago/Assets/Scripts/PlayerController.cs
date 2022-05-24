using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Controle _controle;
    private PlayerInput _playerInput;
    private Camera _mainCamera;
    private Vector2 _moveInput;
    private Rigidbody _rigidbody;
    public float moveMultiplier;

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
        if (obj.action.name.CompareTo(_controle.Gameplay.Move.name) != 0)
        {
            _moveInput = obj.ReadValue<Vector2>();
        }
        
    }

    private void Move()
    {
        _rigidbody.AddForce((_mainCamera.transform.forward * _moveInput.y + 
                            _mainCamera.transform.right * _moveInput.x) *
                            moveMultiplier * Time.deltaTime);
    }

    private void FixedUpdate()
    {
        Move();
    }
}   

