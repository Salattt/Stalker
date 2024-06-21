using UnityEngine;

public class Controller : MonoBehaviour
{
    public Vector2 MoveAxisInput { get; protected set; }
    public float VerticalRotate { get; protected set; }
    public float HorizontalRotate { get; protected set; }

    private void Awake()
    {
        MoveAxisInput = Vector2.zero;
        VerticalRotate = 0f;
        HorizontalRotate = 0f;
    }
}
