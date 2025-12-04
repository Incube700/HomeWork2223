using UnityEngine;
using UnityEngine.AI;

public class AutoPatrolMoveTargetProvider : MonoBehaviour, IMoveTargetProvider
{
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private float _idleTimeBeforePatrol = 3f;
    [SerializeField] private float _patrolRadius = 5f;
    [SerializeField] private float _minVelocityToMove = 0.1f;

    private bool _hasNewPoint;
    private Vector3 _lastPoint;
    private float _idleTimer;

    private void Update()
    {
        float speed = _agent.velocity.magnitude;
        bool isMoving = speed > _minVelocityToMove;

        if (isMoving == true)
        {
            _idleTimer = 0f;
            return;
        }

        _idleTimer += Time.deltaTime;

        if (_idleTimer >= _idleTimeBeforePatrol)
        {
            TryGeneratePatrolPoint();
            _idleTimer = 0f;
        }
    }

    private void TryGeneratePatrolPoint()
    {
        Vector3 center = transform.position;
        Vector3 result;

        if (TryGetRandomPointOnNavMesh(center, _patrolRadius, out result) == true)
        {
            _lastPoint = result;
            _hasNewPoint = true;
        }
    }

    private bool TryGetRandomPointOnNavMesh(Vector3 center, float radius, out Vector3 result)
    {
        const int maxAttempts = 10;

        for (int i = 0; i < maxAttempts; i++)
        {
            Vector3 randomOffset = Random.insideUnitSphere * radius;
            randomOffset.y = 0f;

            Vector3 candidate = center + randomOffset;
            NavMeshHit hit;

            if (NavMesh.SamplePosition(candidate, out hit, 1.0f, NavMesh.AllAreas))
            {
                result = hit.position;
                return true;
            }
        }

        result = center;
        return false;
    }

    public bool TryGetMovePoint(out Vector3 point)
    {
        if (_hasNewPoint == true)
        {
            point = _lastPoint;
            _hasNewPoint = false;
            return true;
        }

        point = Vector3.zero;
        return false;
    }
}
