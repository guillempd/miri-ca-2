using UnityEngine;

[RequireComponent(typeof(Animator))]
public class LocomotionScript : MonoBehaviour
{
    [SerializeField] private bool m_SmoothVelocity = true;
    private float m_LinearVelocity;
    private float m_VelocityX;
    private float m_VelocityZ;
    private Vector3 m_CurrentAcceleration = Vector3.zero;

    [SerializeField] private bool m_FixOrientation = false;
    private float m_AngularVelocity;

    private Tracker m_Tracker;
    private Animator m_Animator;

    private void Awake()
    {
        m_Animator = GetComponent<Animator>();
    }

    private void Start()
    {
        m_Tracker = new Tracker(transform);
    }

    private void LateUpdate()
    {
        m_Tracker.Update();
        ComputeAnimatorParameters();
        SetAnimatorParameters();
    }

    private void ComputeAnimatorParameters()
    {
        Vector3 targetVelocity = m_Tracker.velocity;

        if (m_SmoothVelocity)
        {
            Vector3 currentVelocity = new Vector3(m_VelocityX, 0, m_VelocityZ);
            targetVelocity = Vector3.SmoothDamp(currentVelocity, targetVelocity, ref m_CurrentAcceleration, 0.1f);
        }
        m_VelocityX = targetVelocity.x;
        m_VelocityZ = targetVelocity.z;
        m_LinearVelocity = targetVelocity.magnitude;

        if (!m_FixOrientation && targetVelocity.magnitude > 0.1f)
        {
            float angleOffset = m_Tracker.angleOffset;
            float smoothedAngleOffset = Mathf.SmoothDampAngle(angleOffset, 0, ref m_AngularVelocity, 0.1f);
            transform.Rotate(0, smoothedAngleOffset - angleOffset, 0);
        }
    }

    private void SetAnimatorParameters()
    {
        m_Animator.SetFloat("Velocity X", m_VelocityX);
        m_Animator.SetFloat("Velocity Z", m_VelocityZ);
    }

}
