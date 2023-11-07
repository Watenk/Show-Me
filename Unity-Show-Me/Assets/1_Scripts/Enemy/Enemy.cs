using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IShootable, IMovable, IDamagable
{
    public GameObject Player;
    public Bullet Bullet { get { return bullet; } }
    public GameObject BulletSpawn { get { return bulletSpawn; } }
    public Rigidbody rb {  get; private set; }
    public int MaxHealth { get; private set; }
    public int Health { get; set; }
    public GameObject DropItem;

    [SerializeField]
    private Bullet bullet;
    [SerializeField]
    private GameObject bulletSpawn;
    private FiniteStateMachine fsm;
    private EnemyAttackState attackState;

    public void Start()
    {
        MaxHealth = GameSettings.Instance.EnemyHealth;
        Health = MaxHealth;
        rb = GetComponent<Rigidbody>();
        attackState = GetComponent<EnemyAttackState>();
        attackState.OnAttackShoot += Shoot;
        attackState.OnAttackUpdate += LookTowardsPlayer;
        fsm = new FiniteStateMachine(GetComponents<BaseState>()); 
    }

    public void FixedUpdate()
    {
        fsm.OnUPS();

        if (Vector3.Distance(Player.transform.position, this.transform.position) < GameSettings.Instance.EnemytargetRange)
        {
            fsm.SwitchState(typeof(EnemyAttackState));
        }
    }

    public void Shoot()
    {
        Bullet newBullet = Instantiate(Bullet, BulletSpawn.transform.position, BulletSpawn.transform.rotation);
        newBullet.Move(newBullet.transform.forward, newBullet.speedMultiplier, ForceMode.Impulse);
    }

    public void LookTowardsPlayer()
    {
        transform.LookAt(Player.transform.position);
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

    public void Die()
    {
        Instantiate(DropItem, this.transform.position, Quaternion.identity);
        attackState.OnAttackShoot -= Shoot;
        attackState.OnAttackUpdate -= LookTowardsPlayer;
        Destroy(this.gameObject);
    }
}
