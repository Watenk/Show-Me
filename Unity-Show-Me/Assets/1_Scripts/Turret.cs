using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour, IShootable, IMovable
{
    public Bullet Bullet { get { return bullet; } }
    public GameObject BulletSpawn { get { return bulletSpawn; } }
    public Rigidbody rb { get; private set; }

    [SerializeField]
    private Bullet bullet;
    [SerializeField]
    private GameObject bulletSpawn;
    private FiniteStateMachine fsm;
    private TurretAttackState attackState;
    private GameObject currentEnemy;

    public void Start()
    {
        rb = GetComponent<Rigidbody>();
        attackState = GetComponent<TurretAttackState>();
        attackState.OnAttackShoot += Shoot;
        attackState.OnAttackUpdate += LookTowardsEnemy;
        fsm = new FiniteStateMachine(GetComponents<BaseState>());
    }

    public void FixedUpdate()
    {
        fsm.OnUPS();
    }

    public void Shoot()
    {
        if (currentEnemy != null)
        {
            Bullet newBullet = Instantiate(Bullet, BulletSpawn.transform.position, BulletSpawn.transform.rotation);
            newBullet.Move(newBullet.transform.forward, newBullet.speedMultiplier, ForceMode.Impulse);
        }
    }

    public void LookTowardsEnemy()
    {
        if (currentEnemy != null)
        {
            transform.LookAt(currentEnemy.transform.position);
        }
    }

    public void Move(Vector3 direction, float forceMultiplier, ForceMode forceMode)
    {
        rb.AddForce(direction * forceMultiplier, forceMode);
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            fsm.SwitchState(typeof(TurretAttackState));
            currentEnemy = other.gameObject;
        }
    }
}
