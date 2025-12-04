using UnityEngine;

public interface IDamagable
{
   void TakeDamage(int amount);
   bool IsDead { get; }
}
