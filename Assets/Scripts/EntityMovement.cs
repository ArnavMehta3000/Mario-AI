using UnityEngine;

public class EntityMovement : MonoBehaviour
{
    public float m_moveSpedd   = 1.0f;
    public Vector2 m_direction = Vector2.left;
    
    private Rigidbody2D m_rb;
    private Vector2 m_velocity;

    private void Awake()
    {
        m_rb = GetComponent<Rigidbody2D>();
    }

    private void OnBecameVisible()
    {
        enabled = true;
    }

    private void OnBecameInvisible()
    {
        enabled = false;
    }

    private void OnEnable()
    {
        m_rb.WakeUp();
    }

    private void OnDisable()
    {
        m_rb.Sleep();
    }
}
