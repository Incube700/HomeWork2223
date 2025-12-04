using UnityEngine;
using UnityEngine.AI;

public class PlayerMover : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private Health _health;

    [SerializeField] private ClickMoveTargetProvider _clickProviderSource;
    [SerializeField] private AutoPatrolMoveTargetProvider _autoPatrolProviderSource;
    [SerializeField] private Transform _moveTargetMarker;

    private IMoveTargetProvider _clickProvider;
    private IMoveTargetProvider _autoPatrolProvider;
   
	 private void Update()
    {
        if (_health != null && _health.IsDead == true)
        {
            _agent.isStopped = true;
            return;
        }

        Vector3 targetPoint = transform.position;
        bool hasPoint = false;

        if (_clickProvider != null && _clickProvider.TryGetMovePoint(out targetPoint) == true)
        {
            hasPoint = true;
        }
        else if (_autoPatrolProvider != null && _autoPatrolProvider.TryGetMovePoint(out targetPoint) == true)
        {
            hasPoint = true;
        }

		if (_clickProviderSource != null)
   		 {
        _clickProvider = _clickProviderSource;
  	     }

    if (_autoPatrolProviderSource != null)
    {
        _autoPatrolProvider = _autoPatrolProviderSource;
    }

        if (hasPoint == true)
        {
            _agent.isStopped = false;
            _agent.SetDestination(targetPoint);

            if (_moveTargetMarker != null)
            {
                _moveTargetMarker.gameObject.SetActive(true);
                _moveTargetMarker.position = targetPoint;
            }
        }
    }
}