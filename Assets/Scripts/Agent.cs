using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Agent : MonoBehaviour
{

    private float m_VelocityMagnitude;

    private Rigidbody m_Rigidbody;

    private Vector3 m_GoalPosition;

    private void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Vector3 velocity = (m_GoalPosition - transform.position).normalized * m_VelocityMagnitude;
        Vector3 newPosition = m_Rigidbody.position + velocity * Time.fixedDeltaTime;
        m_Rigidbody.MovePosition(newPosition);
    }

    public void SetGoalPosition(Vector3 goalPosition)
    {
        m_GoalPosition = goalPosition;
        m_VelocityMagnitude = Random.Range(0.5f, 1.5f);
    }

    public bool GoalReached()
    {
        return Vector3.Distance(transform.position, m_GoalPosition) < 0.5f;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(transform.position, m_GoalPosition - transform.position);
        Gizmos.DrawSphere(m_GoalPosition, 0.5f);
    }

}
