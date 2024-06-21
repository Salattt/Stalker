using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent (typeof(Collider))]
public class StalkerMover : MonoBehaviour
{
    [SerializeField] private StairsChecker _stairsChecker;
    [SerializeField] private Controller _controller;
    [SerializeField] private float _colliderHeight;
    [SerializeField] private float _sphereCastDistance;
    [SerializeField] private float _sphereRadius;
    [SerializeField] private float _speed;
    [SerializeField] private float _gravityFactor;

    private Rigidbody _rigidbody;
    private Transform _transform;
    private Collider _collider;
    private Vector3 _input;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _transform = GetComponent<Transform>();
        _collider = GetComponent<Collider>();
    }

    private void OnEnable()
    {
        if(_stairsChecker == null)
            throw new NullReferenceException(nameof(StairsChecker));

        _stairsChecker.StepDetected += OnStepDetected;
    }

    private void FixedUpdate()
    {
        if (_controller != null)
        {
            _input = new Vector3(_controller.MoveAxisInput.x,0,_controller.MoveAxisInput.y);

            AddVelocity(_input);
        }
    }

    private void OnDisable()
    {
        if (_stairsChecker == null)
            throw new NullReferenceException(nameof(StairsChecker));

        _stairsChecker.StepDetected -= OnStepDetected;
    }

    private void AddVelocity(Vector3 velocityDirection)
    {
        if (_rigidbody == null)
            throw new NullReferenceException(nameof(_rigidbody));

        if (Physics.SphereCast(_rigidbody.position,_sphereRadius, Vector3.down, out RaycastHit hitInfo,_sphereCastDistance))
        {
            Vector3 inputVelocity = new Vector3(velocityDirection.x, 0, velocityDirection.z) * _speed;
            _rigidbody.velocity = Vector3.ProjectOnPlane(inputVelocity, hitInfo.normal).normalized * inputVelocity.magnitude;
        }
        else
        {
            _rigidbody.velocity += Vector3.down * _gravityFactor * Time.fixedDeltaTime;
        }
    }

    private void OnStepDetected(Vector3 stepPosition)
    {


        if (Vector2.Dot(new Vector3(stepPosition.x - _transform.position.x,0, stepPosition.z - _transform.position.y), _input) > 0)
        {
            Vector3 distanceToStep = new Vector3((stepPosition - _collider.ClosestPoint(stepPosition)).x,stepPosition.y -  (_transform.position.y - _colliderHeight / 2), (stepPosition - _collider.ClosestPoint(stepPosition)).z);

            _transform.position += distanceToStep;
        }
    }
}
