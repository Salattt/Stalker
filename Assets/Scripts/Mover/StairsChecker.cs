using System;
using UnityEngine;

[RequireComponent (typeof(Collider))]
public class StairsChecker : MonoBehaviour
{
    [SerializeField] private float _colliderHeight;
    [SerializeField] private float _maxStepHeight;
    [SerializeField] private float _minStepHeight;

    private Collider _collider;
    private Transform _transform;

    public Action<Vector3> StepDetected;

    private void Awake()
    {
        _collider = GetComponent<Collider>();
        _transform = transform;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.bounds.ClosestPoint(_transform.position).y - (_transform.position.y - _colliderHeight / 2) < _maxStepHeight && other.bounds.ClosestPoint(_transform.position).y - (_transform.position.y - _colliderHeight / 2) > _minStepHeight)
            StepDetected.Invoke(other.bounds.ClosestPoint(_transform.position));
    }
}
