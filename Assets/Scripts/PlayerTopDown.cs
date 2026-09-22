using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerTopDown : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float speed = 6f; // units per second
    [SerializeField] private float acceleration = 40f; // how fast we reach full speed
    [SerializeField] private float deceleration = 50f; // how fast we stop
    [Header("Facing")]
    [SerializeField] private bool faceMouse = false; // true for twin-stick aiming
    private Rigidbody2D rb;
    private Vector2 moveInput;
    // Last non-zero direction, useful for attacks, dashes, and camera look-ahead.
    public Vector2 FacingDirection { get; private set; } = Vector2.down;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.interpolation = RigidbodyInterpolation2D.Interpolate;
        rb.gravityScale = 0f; // top-down, no gravity
    }
    private void Update()
    {
        // Normalize so diagonal movement isn't faster than straight movement.
        moveInput = new Vector2(InputHelper.Horizontal(), InputHelper.Vertical()).normalized;
        if (faceMouse)
        {
            Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(InputHelper.MouseScreenPosition());
            Vector2 toMouse = (mouseWorld - transform.position);
            if (toMouse.sqrMagnitude > 0.01f)
                FacingDirection = toMouse.normalized;
        }
        else if (moveInput != Vector2.zero)
        {
            FacingDirection = moveInput;
        }
    }
    private void FixedUpdate()
    {
        // Accelerate toward the target velocity, decelerate when there's no input.
        Vector2 targetVelocity = moveInput * speed;
        float rate = moveInput != Vector2.zero ? acceleration : deceleration;
        rb.linearVelocity = Vector2.MoveTowards(rb.linearVelocity, targetVelocity, rate * Time.fixedDeltaTime);
    }
}