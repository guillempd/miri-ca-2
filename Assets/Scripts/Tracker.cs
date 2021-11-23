using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tracker : MonoBehaviour
{
    private Vector3 m_PreviousVelocity;
    private Vector3 m_CurrentVelocity;
    private Vector3 m_PreviousPosition;

    public void InitTracker()
    {
        m_PreviousPosition = transform.position;
        m_PreviousVelocity = Vector3.zero;
    }

    public void UpdateTracker()
    {
        Vector3 currentPosition = transform.position;
        m_PreviousVelocity = m_CurrentVelocity;
        m_CurrentVelocity = (currentPosition - m_PreviousPosition) / Time.deltaTime;
        m_PreviousPosition = currentPosition;
    }

    // TODO: Use C# properties
    public Vector3 GetCurrentVelocity()
    {
        return m_CurrentVelocity;
    }

    public Vector3 GetPreviousVelocity()
    {
        return m_PreviousVelocity;
    }
}
