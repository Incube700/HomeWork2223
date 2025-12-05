using UnityEngine;

public class Mine : MonoBehaviour
{
    [SerializeField] private int _damage = 25;
    [SerializeField] private float _delayBeforeExplosion = 1.5f;
    [SerializeField] private float _explodeRadius = 2.5f;
	
	[SerializeField] private Animator _animator;
	[SerializeField] private ParticleSystem _explodeFxPrefab;

    private bool _isTriggered;
    private float _timer;

    private void Update()
    {
        if (_isTriggered == false)
        {
            return;
        }

        _timer -= Time.deltaTime;

        if (_timer <= 0f)
        {
            Explode();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_isTriggered == true)
        {
            return;
        }

        IDamageable damageable = other.GetComponentInParent<IDamageable>();

        if (damageable != null && damageable.IsDead == false)
        {
            _isTriggered = true;
            _timer = _delayBeforeExplosion;

		if (_animator != null)
			{	
			_animator.SetTrigger("Open");
			}
        }
    }

    private void Explode()
    {
        if (_explodeFxPrefab != null)
        {
            ParticleSystem fx = Instantiate(_explodeFxPrefab, transform.position, Quaternion.identity);
            
            fx.Play();

            Destroy(fx.gameObject, fx.main.duration);
        }
        
        Collider[] hits = Physics.OverlapSphere(transform.position, _explodeRadius);

        for (int i = 0; i < hits.Length; i++)
        {
            IDamageable damageable = hits[i].GetComponentInParent<IDamageable>();

            if (damageable != null && damageable.IsDead == false)
            {
                damageable.TakeDamage(_damage);
            }	
        }

        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _explodeRadius);
    }
}