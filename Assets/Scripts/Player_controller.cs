using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerController : MonoBehaviour
{
    [Header("The three numbers")]
    public float moveSpeed = 8f; // ground speed, units/second
    public float jumpSpeed = 28f; // initial upward speed
    public float airControl = 0.5f; // steering kept mid-air, 0..1
    
    [Header("Ground check")]
    public Transform groundCheck;
    public float groundRadius = 0.8f;
    public LayerMask groundLayer;

    public float Tolerance =0.3f;

    Rigidbody2D rb;
    Vector2 moveInput;
    bool grounded;
    void Awake() { rb = GetComponent<Rigidbody2D>(); }
    void OnMove(InputValue value) { 
        moveInput = value.Get<Vector2>();
    }
    void FixedUpdate()
    {
        // grounded  = Physics2D.Raycast(transform.position, Vector2.down, 2.2f, groundLayer);

        grounded = rb.linearVelocity.y <= Tolerance;

        Debug.Log("The object is grounded : " + grounded);
        Debug.Log("The input has the next vector: " + moveInput);

        float targetX = moveInput.x * moveSpeed;
        if (grounded)
            rb.linearVelocity = new Vector2(targetX, rb.linearVelocity.y);
        else
            rb.linearVelocity = new Vector2(
            Mathf.Lerp(rb.linearVelocity.x, targetX, airControl),
            rb.linearVelocity.y);

    }
    void OnJump()
    {
        Debug.Log("OnJump executed");
        if (grounded)
            rb.linearVelocity =
            new Vector2(rb.linearVelocity.x, jumpSpeed);
    }
}