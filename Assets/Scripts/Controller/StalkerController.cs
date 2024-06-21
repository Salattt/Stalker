using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StalkerController : Controller
{
    [SerializeField] Transform _target;
    [SerializeField] double _distanceBetweenTarget;

    private Transform _transform;

    private void Awake()
    {
        _transform = transform;
    }

    void Update()
    {
        if (_target == null)
            throw new NullReferenceException(nameof(_target));

        Vector3 targetDirection = (_target.position - _transform.position);
        Vector3 transformForward = _transform.forward;

        transformForward.y = 0;

        SetMoveAxis(targetDirection, transformForward);

        targetDirection.y = 0;

        SetHorizontalAngle(targetDirection, transformForward);
    }

    private void SetMoveAxis(Vector3 targetDirection, Vector3 transformForward)
    {
        if (Vector3.Distance(targetDirection, transformForward) > _distanceBetweenTarget)
            MoveAxisInput = new Vector2(targetDirection.x, targetDirection.z).normalized;
        else
            MoveAxisInput = Vector2.zero;
    }

    private void SetHorizontalAngle(Vector3 targetDirection, Vector3 transformForward)
    {
        float newHorizontalRotate;

        newHorizontalRotate = SignedAngleBetween(transformForward, targetDirection, new Vector3(0, 1, 0));

        if (newHorizontalRotate > 5 || newHorizontalRotate < -5)
            HorizontalRotate = newHorizontalRotate;
        else
            HorizontalRotate = 0;
    }

    private float SignedAngleBetween(Vector3 a, Vector3 b, Vector3 n)
    {
        float angle = Vector3.Angle(a, b);
        float sign = Mathf.Sign(Vector3.Dot(n, Vector3.Cross(a, b)));

        float signed_angle = angle * sign;

        return signed_angle;
    }
}
