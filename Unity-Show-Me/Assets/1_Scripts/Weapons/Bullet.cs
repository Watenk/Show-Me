using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Bullet : MonoBehaviour, IDamagable, IMovable
{
    public int MaxHealth { get; protected set; } 
    public int Health { get; set; }
    public Rigidbody rb { get; protected set; }
    public float speedMultiplier { get; protected set; }    

    public virtual void Awake()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null) { Debug.LogError(this.name + " Couldn't find Rigidbody"); }

        MaxHealth = GameSettings.Instance.BulletHealth;
        Health = MaxHealth;
    }

    public void Move(Vector3 direction, float forceMultiplier, ForceMode forceMode)
    {
        rb.AddForce(direction * forceMultiplier, forceMode);
    }

    public void TakeDamage(int _amount)
    {
        Health -= _amount;
        if (Health <= 0)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        Destroy(this.gameObject);
    }
}
