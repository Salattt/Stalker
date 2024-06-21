using UnityEngine;

public class CameraRotator : MonoBehaviour , IRotator
{
    [SerializeField] private Controller _controller;
    [SerializeField] float _minAngle = -89f;
    [SerializeField] float _maxAngle = 89f;


    private Transform _transform;
    private float _angle = 0;

    private void Awake()
    {
        _transform = transform;
    }

    private void Update()
    {
        if (_controller != null)
            Rotate(_controller.VerticalRotate);
    }

    public void Rotate(float angle)
    {
        _angle -= angle;
        _angle = Mathf.Clamp(_angle, _minAngle, _maxAngle);

        _transform.localEulerAngles =Vector3.right * _angle;
    }
}
