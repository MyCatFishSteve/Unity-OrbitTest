using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    private bool m_ZoomActive;
    private GameObject m_Player;
    private Camera m_Camera;

    private float velocity;
    
    [SerializeField] private float m_DefaultZoom = 5f;
    [SerializeField] private float m_ActiveZoom = 7f;

    // Start is called before the first frame update
    private void Start()
    {
        m_Player = GameObject.FindGameObjectWithTag("Player");
        m_Camera = Camera.main;
    }

    // Update is called once per frame
    private void Update()
    {
        float selectedZoom = Input.GetKey(KeyCode.Space) ? m_ActiveZoom : m_DefaultZoom;
        m_Camera.orthographicSize = Mathf.SmoothDamp(m_Camera.orthographicSize, selectedZoom, ref velocity, 0.1f);
    }
}
