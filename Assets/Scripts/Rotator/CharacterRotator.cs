using UnityEngine;

public class CharacterRotator : MonoBehaviour , IRotator
{
    [SerializeField] private Controller _controller;

    private Transform _transorm;

    private void Awake()
    {
        _transorm = transform;
    }

    private void Update()
    {
        if (_controller != null)
            Rotate(_controller.HorizontalRotate);
    }

    public void Rotate(float angle)
    {
        _transorm.Rotate(Vector3.up  * angle);
        _transorm.eulerAngles = new Vector3(0, _transorm.eulerAngles.y, 0);
    }
}
