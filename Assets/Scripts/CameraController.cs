using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Transform m_target;

    private void Awake()
    {
        m_target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void LateUpdate()
    {
        Vector3 camPos     = transform.position;
        camPos.x           = Mathf.Max(m_target.position.x, camPos.x);
        transform.position = camPos;
    }
}
