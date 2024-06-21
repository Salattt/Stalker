using System;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMover : MonoBehaviour 
{
    [SerializeField] private float _speed; 
    [SerializeField] private Controller _controller;

    private Transform _trasform;
    private CharacterController _characterController;

    private void Awake()
    {
        _trasform = transform;
        _characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if (_controller == null)
            throw new NullReferenceException(nameof(_controller));

        if (_characterController.isGrounded)
        {
            Vector3 nextMove = new Vector3(_controller.MoveAxisInput.x , 0, _controller.MoveAxisInput.y);

            nextMove = _trasform.TransformDirection(nextMove * _speed);
            nextMove += Physics.gravity;
            nextMove *= Time.deltaTime;

            _characterController.Move(nextMove);
        }
        else
        {
            _characterController.Move((_characterController.velocity + Physics.gravity* Time.deltaTime) * Time.deltaTime);
        }
    }
}
