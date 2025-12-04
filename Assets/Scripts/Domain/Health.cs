using UnityEngine;

public class Health : MonoBehaviour, IDamageable
{
    [SerializeField] private int _maxHealth = 100;
    
    private int _currentHealth;
    private bool _isDead;
    
    public int Maxhealth => _maxHealth;
    public int CurrentHealth => _currentHealth;
    public bool IsDead => _isDead;
    public float HealthPercent => (float)_currentHealth / _maxHealth;

    private void Awake()
    {
        _currentHealth = _maxHealth;
        _isDead = false;
    }

    public void TakeDamage(int amount)
    {
        if (_isDead == true)
        {
            return;
        }

        if (amount < 0)
        {
            amount = 0;
        }
        
        _currentHealth -= amount;

        if (_currentHealth < 0)
        {
            _currentHealth = 0;
        }

        if (_currentHealth == 0)
        {
            Die();
        }
    }

    private void Die()
    {
        if (_isDead == true)
        {
            return;
        }
        
        _isDead = true;
    }
}
