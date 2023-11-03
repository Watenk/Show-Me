using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalBullet : Bullet
{
    public GameObject DiesParticle;

    public override void Awake()
    {
        base.Awake();
        speedMultiplier = GameSettings.Instance.NormalBulletSpeed;
    }

    public void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.GetComponent<IDamagable>() != null)
        {
            IDamagable damagable = other.gameObject.GetComponent<IDamagable>();
            damagable.TakeDamage(GameSettings.Instance.NormalBulletDamage);
        }
        TakeDamage(1);
    }

    public override void Die()
    {
        Instantiate(DiesParticle, transform.position, Quaternion.identity);
        base.Die();
    }
}
