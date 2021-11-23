using UnityEngine;

[RequireComponent(typeof(Tracker), typeof(Animator))]
public class LocomotionScript : MonoBehaviour
{
    public bool m_Smooth;

    private Tracker m_Tracker;
    private Animator m_Animator;

    private float m_CurrentVelocityX;
    private float m_CurrentVelocityZ;
    private Vector3 m_CurrentAcceleration;

    private void Awake()
    {
        m_Tracker = GetComponent<Tracker>();
        m_Animator = GetComponent<Animator>();
    }

    private void Start()
    {
        m_Tracker.InitTracker();
    }

    private void LateUpdate()
    {
        m_Tracker.UpdateTracker();

        ComputeParameters();

        SetParameters();
    }

    private void ComputeParameters()
    {
        Vector3 targetVelocity = m_Tracker.GetCurrentVelocity();
        if (m_Smooth)
        {
            Vector3 currentVelocity = new Vector3(m_CurrentVelocityX, 0, m_CurrentVelocityZ);
            Vector3 smoothedVelocity = Vector3.SmoothDamp(currentVelocity, targetVelocity, ref m_CurrentAcceleration, 0.1f);

            m_CurrentVelocityX = smoothedVelocity.x;
            m_CurrentVelocityZ = smoothedVelocity.z;
        }
        else
        {
            m_CurrentVelocityX = targetVelocity.x;
            m_CurrentVelocityZ = targetVelocity.z;
        }
    }

    private void SetParameters()
    {
        m_Animator.SetFloat("Velocity X", m_CurrentVelocityX);
        m_Animator.SetFloat("Velocity Z", m_CurrentVelocityZ);
    }

}
