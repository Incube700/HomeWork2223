using UnityEngine;

public class Health : MonoBehaviour, IDamageble
{
    [SerializeField] private int _maxHealth = 100;
    
    private int _currentHealth;
    private bool _isDead;
    
    public int Maxhealth => _maxHealth;
    public int CurrentHealth => _currentHealth;
    public bool IsDead => _isDead;
    public float HealthPercent => (float)_currentHealth / _maxHealth;
    
    Awake
    
    
    
    
}
