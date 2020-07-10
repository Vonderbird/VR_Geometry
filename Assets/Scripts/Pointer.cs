using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pointer : MonoBehaviour
{
    public float m_DefaultLength = 5.0f;
    public GameObject m_Dot;
    public InputModule m_InputModule;
    private LineRenderer m_LineRenderer = null;
    private void Awake()
    {
        m_LineRenderer = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    private void Update()
    {
        UpdateLine();
    }

    private void UpdateLine()
    {
        //use default value from our input module
        float TargetLength = m_DefaultLength;
        //Raycast
        RaycastHit hit = createRaycast(TargetLength);
        //Default
        Vector3 EndPosition = transform.position + (transform.forward * TargetLength);
        //Update when hitting something (Check for collider)
        if (hit.collider != null)
        {
            EndPosition = hit.point;
        }

        //Set position of the Dot
        m_Dot.transform.position = EndPosition;
        //Set position of LineRenderer
        m_LineRenderer.SetPosition(0, transform.position);
        m_LineRenderer.SetPosition(1, EndPosition);
        

    }

    private RaycastHit createRaycast(float length)
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position,transform.forward);
        Physics.Raycast(ray, out hit, m_DefaultLength);
        return hit;
    }
}
