using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float m_MovementSpeed = 5.0f;
    
    private float m_MovementDirection;

    private Rigidbody2D m_Rigidbody2D;

    private GameObject m_Planet;

#if UNITY_EDITOR
    [SerializeField] private bool m_DebugMode = false;
#endif

    private void Start()
    {
        m_Rigidbody2D = this.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        m_MovementDirection = Input.GetAxis("Horizontal");
    }

    private void FixedUpdate()
    {
#if UNITY_EDITOR
        if (m_DebugMode)
        {
            Debug.DrawRay(transform.position, Vector2.zero - (Vector2)transform.position);
        }
#endif
        Vector2 planetNormal = Vector2.zero;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.zero - (Vector2)transform.position);
        if (hit.collider)
        {
            planetNormal = hit.normal;
            float desiredAngle = Vector2.SignedAngle(Vector2.up, hit.normal);
            transform.eulerAngles = new Vector3(0, 0, desiredAngle);
#if UNITY_EDITOR
            if (m_DebugMode)
            {
                Debug.DrawRay(hit.point, hit.normal, Color.yellow);
                Debug.DrawRay(hit.point, Vector2.Perpendicular(hit.normal), Color.blue);
            }
#endif
            m_Rigidbody2D.AddForce(-hit.normal * 9.81f);
            
        }

        m_Rigidbody2D.velocity += -Vector2.Perpendicular(planetNormal) * m_MovementDirection * m_MovementSpeed;
    }
}
