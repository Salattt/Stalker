using UnityEngine;

public class PlayerController : Controller
{
    [SerializeField] private float _mouseXSensitivity = 1f;
    [SerializeField] private float _mouseYSensitivity = 1f;

    void Update()
    {
        Vector2 input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;

        MoveAxisInput = input;
        HorizontalRotate = Input.GetAxis("Mouse X") * _mouseXSensitivity;
        VerticalRotate = Input.GetAxis("Mouse Y") * _mouseYSensitivity;
    }
}
