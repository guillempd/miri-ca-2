using UnityEngine;

public class Tracker
{
    private Transform m_Transform;
    private Vector3 m_CurrentVelocity;
    private Vector3 m_WorldPreviousPosition;
    private float m_AngleOffset;

    public Vector3 velocity => m_CurrentVelocity;
    public float angleOffset => m_AngleOffset;

    public Tracker(Transform transform)
    {
        m_Transform = transform;
        m_WorldPreviousPosition = transform.position;
    }

    public void Update()
    {
        Vector3 worldCurrentPosition = m_Transform.position;
        Vector3 worldCurrentVelocity = ((worldCurrentPosition - m_WorldPreviousPosition)) / Time.deltaTime;
        m_CurrentVelocity = m_Transform.InverseTransformVector(worldCurrentVelocity);
        m_AngleOffset = Vector3.SignedAngle(m_CurrentVelocity, Vector3.forward, Vector3.up);
        m_WorldPreviousPosition = worldCurrentPosition;
    }

}
