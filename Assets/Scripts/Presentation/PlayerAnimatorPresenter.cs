using UnityEngine;
using UnityEngine.AI;

public class PlayerAnimatorPresenter : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private Health _health;
    [SerializeField] private float _woundedPercentThreshold = 0.3f;
    [SerializeField] private float _minVelocityToMove = 0.1f;

    private readonly int _isMovingHash = Animator.StringToHash("IsMoving");
    private readonly int _isWoundedHash = Animator.StringToHash("IsWounded");
    private readonly int _dieHash = Animator.StringToHash("Die");
    private readonly int _hitHash = Animator.StringToHash("Hit");

    private bool _dieTriggered;
    private int _lastHealth;

    private void Awake()
    {
        if (_health != null)
        {
            _lastHealth = _health.CurrentHealth;
        }
    }

    private void Update()
    {
        if (_animator == null || _agent == null || _health == null)
        {
            return;
        }

        if (_health.IsDead == true)
        {
            if (_dieTriggered == false)
            {
                _animator.SetBool(_isMovingHash, false);
                _animator.SetBool(_isWoundedHash, true);
                _animator.SetTrigger(_dieHash);
                _dieTriggered = true;
            }

            return;
        }

        int currentHealth = _health.CurrentHealth;

        if (currentHealth < _lastHealth)
        {
            _animator.SetTrigger(_hitHash);
            _lastHealth = currentHealth;
        }
        else if (currentHealth > _lastHealth)
        {
            _lastHealth = currentHealth;
        }
    
        float speed = _agent.velocity.magnitude;
        bool isMoving = speed > _minVelocityToMove;
        _animator.SetBool(_isMovingHash, isMoving);

        bool isWounded = _health.HealthPercent < _woundedPercentThreshold;
        _animator.SetBool(_isWoundedHash, isWounded);
    }
}
