using TreeEditor;
using Unity.VisualScripting;
using UnityEngine;

public class MarioMovement : MonoBehaviour
{
    public float        m_moveSpeed     = 8.0f;
    public float        m_maxJumpHeight = 5.0f;
    public float        m_maxJumpTime   = 1.0f;

    private Camera      m_camera;
    private Rigidbody2D m_rb;
    private float       m_hMove;
    private Vector2     m_velocity;

    public bool isGrounded { get; private set; }
    public bool isJumping { get; private set; }

    public float JumpForce => (2.0f * m_maxJumpHeight) / (m_maxJumpTime / 2.0f);
    public float Gravity => (-2.0f * m_maxJumpHeight) / Mathf.Pow(m_maxJumpTime / 2.0f, 2.0f);



    private void Awake()
    {
        m_rb     = GetComponent<Rigidbody2D>();
        m_camera = Camera.main;
    }

    private void Update()
    {
        HorizontalMovement();

        isGrounded = m_rb.Raycast(Vector2.down);

        if (isGrounded)
            GroundedMovement();

        ApplyGravity();
    }

    private void FixedUpdate()
    {
        Vector2 position = m_rb.position;
        position += m_velocity * Time.deltaTime;

        // Keep mario in screen bounds
        Vector2 leftEdge  = m_camera.ScreenToWorldPoint(Vector2.zero);
        Vector2 rightEdge = m_camera.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        position.x        = Mathf.Clamp(position.x, leftEdge.x + 0.5f, rightEdge.x - 0.5f);

        m_rb.MovePosition(position);
    }



    void HorizontalMovement()
    {
        m_hMove      = Input.GetAxis("Horizontal");
        m_velocity.x = Mathf.MoveTowards(m_velocity.x, m_hMove * m_moveSpeed, m_moveSpeed * Time.deltaTime);

        if (m_rb.Raycast(Vector2.right * m_velocity.x))
            m_velocity.x = 0.0f;

        if (m_velocity.x > 0.0f)
            transform.eulerAngles = Vector3.zero;
        else if (m_velocity.x < 0.0f)
            transform.eulerAngles = new Vector3(0.0f, 180.0f, 0.0f);
    }

    void ApplyGravity()
    {
        bool isFalling  = m_velocity.y < 0.0f || !Input.GetButton("Jump");
        float multiplier = isFalling ? 2.0f : 1.0f;

        m_velocity.y += Gravity * multiplier * Time.deltaTime;

        // Set terminal velocity
        m_velocity.y = Mathf.Max(m_velocity.y, Gravity / 2.0f);
    }

    private void GroundedMovement()
    {
        m_velocity.y = Mathf.Max(m_velocity.y, 0.0f);

        // Only check for jumping when grounded
        isJumping = m_velocity.y > 0.0f;

        if (Input.GetButtonDown("Jump"))
        {
            m_velocity.y = JumpForce;
            isJumping = true;
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check for head collisions
        if (collision.gameObject.layer != LayerMask.NameToLayer("PowerUp"))
        {
            if (transform.DotTest(collision.transform, Vector2.up))
                m_velocity.y = 0.0f;
        }
    }
}
